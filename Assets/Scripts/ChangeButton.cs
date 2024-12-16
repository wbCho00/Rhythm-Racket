using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeButton : MonoBehaviour
{
	public TextMeshProUGUI mainText;
	public GameObject startButtonRemini;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Onclicked()
	{
		if (startButtonRemini.activeSelf)//true
		{
			mainText.text = "Current Song: Reminiscence";
		}
		else
		{
			mainText.text = "Current Song: Stellar";
		}
	}
}
