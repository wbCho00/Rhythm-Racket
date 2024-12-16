using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RacketManager : MonoBehaviour
{
    public GameObject[] dynamicRackets; 
    public Button[] equipButtons;
	public GameObject[] dynamicBalls;
	public Button[] ballButtons;
	public TextMeshProUGUI[] equipButtonTexts;
	public TextMeshProUGUI[] ballButtonTexts;
	public Transform leftHandRacketHolder; 
    public Transform rightHandRacketHolder; 

    public Button startButton; 
    public Button startButton2;

	public GameObject ray;
	public GameObject demostart;
	public GameObject demostart2;

	private GameObject selectedRacketPrefab;
	private GameObject selectedBallPrefab;
    private GameObject currentLeftRacket;
    private GameObject currentRightRacket; 

    public Vector3 positionOffset = Vector3.zero; 
    public Vector3 rotationOffset = new Vector3(0, 0, 0); 

    void Start()
    {
        for (int i = 0; i < equipButtons.Length; i++)
        {
            int index = i;
            equipButtons[i].onClick.AddListener(() => OnEquipButtonClicked(index));
        }
		for (int i = 0; i < ballButtons.Length; i++)
		{
			int index = i;
			ballButtons[i].onClick.AddListener(() => OnBallButtonClicked(index));
		}

		startButton.onClick.AddListener(OnStartButtonClicked);
		startButton2.onClick.AddListener(OnStartButtonClicked);
	}

	void OnEquipButtonClicked(int index)
    {
        foreach (var text in equipButtonTexts)
        {
            text.text = "Equip";
        }
        equipButtonTexts[index].text = "Selected";

        selectedRacketPrefab = dynamicRackets[index];
        Debug.Log($"Racket {dynamicRackets[index].name} selected but not equipped.");
    }
	void OnBallButtonClicked(int index)
	{
		foreach (var text in ballButtonTexts)
		{
			text.text = "Equip";
		}
		ballButtonTexts[index].text = "Selected";
		DemoStart demoStartget = demostart.GetComponent<DemoStart>();
		DemoStartStellar demoStartget2 = demostart2.GetComponent<DemoStartStellar>();
		demoStartget.ballPrefab = dynamicBalls[index];
		demoStartget2.ballPrefab = dynamicBalls[index];
	}
	void OnStartButtonClicked()
    {
        if (selectedRacketPrefab == null)
        {
            Debug.LogWarning("No racket selected. Please select a racket before starting.");
            return;
        }

        EquipRackets();

        if (ray != null) ray.SetActive(false);

        foreach (var button in equipButtons)
        {
            button.interactable = false;
        }
		startButton.gameObject.SetActive(false);
		startButton2.gameObject.SetActive(false);

		Debug.Log("Game Started! Rackets equipped.");
    }

    public void OnSongEnded()
    {
        if (ray != null) ray.SetActive(true);

        UnequipRackets();

        foreach (var button in equipButtons)
        {
            button.interactable = true;
        }
        startButton.gameObject.SetActive(true);

        Debug.Log("Song Ended. Rackets removed and ray re-enabled.");
    }

    void EquipRackets()
    {
        if (selectedRacketPrefab == null)
        {
            Debug.LogError("No racket prefab to equip.");
            return;
        }

        currentLeftRacket = Instantiate(selectedRacketPrefab);
        currentRightRacket = Instantiate(selectedRacketPrefab);

        AttachRacketToHand(currentLeftRacket, leftHandRacketHolder);
        AttachRacketToHand(currentRightRacket, rightHandRacketHolder);
    }

    void UnequipRackets()
    {
        if (currentLeftRacket != null) Destroy(currentLeftRacket);
        if (currentRightRacket != null) Destroy(currentRightRacket);

        currentLeftRacket = null;
        currentRightRacket = null;
    }

    void AttachRacketToHand(GameObject racket, Transform handHolder)
    {
        if (racket == null || handHolder == null)
        {
            Debug.LogWarning("Racket hoặc Hand Holder bị null.");
            return;
        }

        racket.transform.position = handHolder.position + handHolder.TransformDirection(new Vector3(0, 0, 0));

        racket.transform.rotation = handHolder.rotation * Quaternion.Euler(new Vector3(45, 0, 90));

        racket.transform.SetParent(handHolder);

        var rigidbody = racket.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }

        Debug.Log($"Racket {racket.name} đã được gắn vào {handHolder.name} với offset đúng.");
    }
}
