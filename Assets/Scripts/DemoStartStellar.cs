using UnityEngine;
using System.Collections;
using TMPro;

public class DemoStartStellar : MonoBehaviour
{
	public GameObject guideDetection;        
	public GameObject mainCanvas;            
	public GameObject savedObject; 
	public TextMeshProUGUI scoreText;
	public GameObject ballPrefab;            
	public Transform cannon1Transform;       
	public Transform cannon2Transform;       
	public Transform cannon3Transform;       
	public Transform cannon4Transform;       
	public Transform ballGuideLUTransform;   
	public Transform ballGuideLDTransform;   
	public Transform ballGuideRUTransform;   
	public Transform ballGuideRDTransform;   
	public float launchInterval = 1.0f;      
	public float ballLifetime = 5.0f;        
	public int score = 0;
	public AudioSource musicPlayer;
	public AudioClip mainMusic;
	public AudioClip collisionSound;         
	public AudioSource ballAudioSource;          

	private float bpm = 200f;
	private float height = 0.5f;

    public GameObject ray; // Đối tượng ray
    public RacketManager racketManager; // Script RacketManager


    void Start()
	{
	}

	public void OnStartButtonClick()
	{
		StartCoroutine(PlayMusic());
		StartCoroutine(LaunchBallsSequenceStellar());
	}
	IEnumerator PlayMusic()
	{
		yield return new WaitForSeconds(1.2f);
		if (musicPlayer != null && mainMusic != null)
		{
			musicPlayer.PlayOneShot(mainMusic);
			yield return new WaitForSeconds(37.8f);
			musicPlayer.Stop();
		}

	}
	IEnumerator LaunchBallsSequenceStellar()
	{
		float bpm2ms = 60f / bpm;
		float bit4 = bpm2ms;				//1aaa 4ºÐÀ½Ç¥ 1¹Ú
		float bit8 = bpm2ms / 2f;			//1a1a 8ºÐÀ½Ç¥
		float bit16 = bpm2ms / 4f;          //1111 16ºÐÀ½Ç¥
		float bit4d3 = bpm2ms / 3f;
		float bitswing = bpm2ms / 4f * 3f;	//1aa1 ½ºÀ®¹ÚÀÚ
		float bit2 = bpm2ms * 2f;			//1aaa aaaa 2ºÐÀ½Ç¥ 2¹Ú
		float bit1 = bpm2ms * 4f;           //1aaa aaaa aaaa aaaa ¿ÂÀ½Ç¥ 1¸¶µð
											//yield return new WaitForSeconds(bit4);
											//yield return new WaitForSeconds(bit8);
											//yield return new WaitForSeconds(bit16);
											//yield return new WaitForSeconds(bitswing);
											//yield return new WaitForSeconds(bit2);
											//yield return new WaitForSeconds(bit1);


		//StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		//StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		//StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		//StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		scoreText.text = "Score: " + score;

		// Guide_Detection ¿ÀºêÁ§Æ® À§Ä¡¸¦ (0, 0, 0)À¸·Î ÀÌµ¿
		guideDetection.transform.position = new Vector3(0, height, 0);
		// mainCanvas ¿ÀºêÁ§Æ®¸¦ yÃà ¹æÇâÀ¸·Î 500 ÀÌµ¿½ÃÄÑ¼­ º¸ÀÌÁö ¾Ê°Ô ÇÔ
		mainCanvas.transform.position += new Vector3(0, 500, 0);

		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);

		//yield return new WaitForSeconds(0.577f); //À½¿ø ½ÃÀÛ ÈÄ ´ë±â½Ã°£
		//도입부
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit2);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);

		//A
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit4);

		//trill
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);

		//high trill
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3)); //우상
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2)); //좌하 
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4)); //우하
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1)); //좌상
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);
		yield return new WaitForSeconds(bit4);




		// °î ³¡³²
		yield return new WaitForSeconds(3.0f);
        if (ray != null)
        {
            ray.SetActive(true);
            Debug.Log("Ray has been re-enabled.");
        }

        if (racketManager != null)
        {
            racketManager.OnSongEnded();
            Debug.Log("Rackets removed via RacketManager.");
        }
        // mainCanvas ¿ÀºêÁ§Æ®¸¦ yÃà ¹æÇâÀ¸·Î -500 ÀÌµ¿½ÃÄÑ¼­ ´Ù½Ã º¸ÀÌ°Ô ÇÔ
        mainCanvas.transform.position += new Vector3(0, -500, 0);
		// Guide_Detection ¿ÀºêÁ§Æ® À§Ä¡¸¦ (0, 500, 0)À¸·Î ÀÌµ¿½ÃÄÑ¼­ º¸ÀÌÁö ¾Ê°Ô ÇÔ
		guideDetection.transform.position += new Vector3(0, 500, 0);
		scoreText.text = "";
		score = 0;
	}

	IEnumerator LaunchBall(GameObject ballPrefab, Transform cannonTransform, Transform targetTransform, int Type)
	{
		// °øÀÇ ÀÎ½ºÅÏ½º »ý¼º
		GameObject ball = Instantiate(ballPrefab, cannonTransform.position, cannonTransform.rotation);
		SavedValues savedValues = savedObject.GetComponent<SavedValues>();

		// ProjectileMotion ½ºÅ©¸³Æ®¿¡ ½ÃÀÛ À§Ä¡¿Í ¸ñÇ¥ À§Ä¡ ¼³Á¤
		ProjectileMotion projectileMotion = ball.GetComponent<ProjectileMotion>();
		if (projectileMotion != null)
		{
			projectileMotion.startPoint = cannonTransform;
			Debug.Log("cannon position: " + cannonTransform.position);
			projectileMotion.targetPoint = targetTransform;
			Debug.Log("target position: " + targetTransform.position);
			switch (Type)
			{
				case 1:
					projectileMotion.velocity = savedValues.velocity1;
					projectileMotion.angleDeg = savedValues.angle1;
					break;
				case 2:
					projectileMotion.velocity = savedValues.velocity2;
					projectileMotion.angleDeg = savedValues.angle2;
					break;

				case 3:
					projectileMotion.velocity = savedValues.velocity3;
					projectileMotion.angleDeg = savedValues.angle3;
					break;

				case 4:
					projectileMotion.velocity = savedValues.velocity4;
					projectileMotion.angleDeg = savedValues.angle4;
					break;

				default:
					Debug.LogError("Å¸ÀÔ ¿¡·¯");
					break;
			}
		}
		// BallCollisionHandler ½ºÅ©¸³Æ®¸¦ °ø¿¡ Ãß°¡
		BallCollisionHandler ballCollisionHandler = ball.AddComponent<BallCollisionHandler>();
		ballCollisionHandler.guideDetection = guideDetection;
		ballCollisionHandler.onScoreUpdate = UpdateScore;
		ballCollisionHandler.collisionSound = collisionSound;
		ballCollisionHandler.audioSource = ball.GetComponent<AudioSource>();
	ballCollisionHandler.laneIndex = Type;
	ballCollisionHandler.targetPoint = targetTransform;

		// ÀÏÁ¤ ½Ã°£ ÈÄ °ø Á¦°Å (ÃÖÀûÈ­¸¦ À§ÇØ)
		Destroy(ball, ballLifetime);

		yield return null;
	}
	void UpdateScore(int addScore)
	{
		score += addScore;
		scoreText.text = "Score: " + score;
	}
}
