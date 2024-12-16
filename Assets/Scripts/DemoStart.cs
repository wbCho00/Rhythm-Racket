using UnityEngine;
using System.Collections;
using TMPro;

public class DemoStart : MonoBehaviour
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

	private float bpm = 95f;
	private float height = 0.5f;

    public GameObject ray; // Đối tượng ray
    public RacketManager racketManager; // Script RacketManager


    void Start()
	{
	}

	public void OnStartButtonClick()
	{
		StartCoroutine(PlayMusic());
		StartCoroutine(LaunchBallsSequenceReminiscence());
	}
	IEnumerator PlayMusic()
	{
		yield return new WaitForSeconds(1.2f);
		if (musicPlayer != null && mainMusic != null)
		{
			musicPlayer.PlayOneShot(mainMusic);
			yield return new WaitForSeconds(46f);
			musicPlayer.Stop();
		}

	}
	IEnumerator LaunchBallsSequenceReminiscence()
	{
		float bpm2ms = 60f / bpm;
		float bit4 = bpm2ms;				//1aaa 4ºÐÀ½Ç¥ 1¹Ú
		float bit8 = bpm2ms / 2f;			//1a1a 8ºÐÀ½Ç¥
		float bit16 = bpm2ms / 4f;			//1111 16ºÐÀ½Ç¥
		float bitswing = bpm2ms / 4f * 3f;	//1aa1 ½ºÀ®¹ÚÀÚ
		float bit2 = bpm2ms * 2f;			//1aaa aaaa 2ºÐÀ½Ç¥ 2¹Ú
		float bit1 = bpm2ms * 4f;           //1aaa aaaa aaaa aaaa ¿ÂÀ½Ç¥ 1¸¶µð
											//yield return new WaitForSeconds(bit4);
											//yield return new WaitForSeconds(bit8);
											//yield return new WaitForSeconds(bit16);
											//yield return new WaitForSeconds(bitswing);
											//yield return new WaitForSeconds(bit2);
											//yield return new WaitForSeconds(bit1);


		//StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		//StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		//StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		//StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));

		scoreText.text = "Score: " + score;

		// Guide_Detection ¿ÀºêÁ§Æ® À§Ä¡¸¦ (0, 0, 0)À¸·Î ÀÌµ¿
		guideDetection.transform.position = new Vector3(0, height, 0);
		// mainCanvas ¿ÀºêÁ§Æ®¸¦ yÃà ¹æÇâÀ¸·Î 500 ÀÌµ¿½ÃÄÑ¼­ º¸ÀÌÁö ¾Ê°Ô ÇÔ
		mainCanvas.transform.position += new Vector3(0, 500, 0);

		yield return new WaitForSeconds(0.577f); //À½¿ø ½ÃÀÛ ÈÄ ´ë±â½Ã°£

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit2);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bit2);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bit2);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bit2);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bit2);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bit2);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bit1);

		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bit2);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit2);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bit1);

		yield return new WaitForSeconds(bit1);

		//ÀüÁÖ³¡³²

		//A 323 323 332 4
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bitswing);


		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bitswing);


		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bitswing);



		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bitswing);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bitswing);


		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bitswing);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bit2);


		//B 323 422 341 8
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bitswing);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bitswing);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit4);

		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bitswing);
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bit4);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit16);
		yield return new WaitForSeconds(bit2);


		//C 323 323 332 422
		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bitswing);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bitswing);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bitswing);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bitswing);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bitswing);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bitswing);

		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bit8);

		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit4);

		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit8);


		//D 31 4 4 121 4 31 4 4
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bitswing);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bit16);

		yield return new WaitForSeconds(bit4);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bit4);

		yield return new WaitForSeconds(bit16);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bit8);
		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bit16);
		//
		StartCoroutine(LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform, 2));
		yield return new WaitForSeconds(bit4);

		StartCoroutine(LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform, 4));
		yield return new WaitForSeconds(bitswing);
		StartCoroutine(LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform, 1));
		yield return new WaitForSeconds(bit16);

		StartCoroutine(LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform, 3));
		yield return new WaitForSeconds(bit4);

		yield return new WaitForSeconds(bit4);

		//ÀÚ¸£±â Å×½ºÆ®



		//Time.deltatime ÀÌ¿ëÇÏ±â?






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
public class BallCollisionHandler : MonoBehaviour
{
	public GameObject guideDetection; // Guide_Detection ¿ÀºêÁ§Æ®
	public System.Action<int> onScoreUpdate;
	public AudioClip collisionSound;   // Ãæµ¹ ½Ã Àç»ýÇÒ ¼Ò¸®
	public AudioSource audioSource;    // ¿Àµð¿À ¼Ò½º ÄÄÆ÷³ÍÆ®
	private bool hasCollided = false;  // Ã¹ ¹øÂ° Ãæµ¹¸¸ Ã³¸®ÇÏ±â À§ÇÑ ÇÃ·¡±×

	void OnCollisionEnter(Collision collision)
	{
		if (hasCollided) return; // ÀÌ¹Ì Ãæµ¹ÇßÀ¸¸é ÀÌÈÄ Ãæµ¹ ¹«½Ã
		
		// ¶óÄÏ°úÀÇ Ãæµ¹À» °¨Áö
		if (collision.gameObject.CompareTag("Rackets"))
		{
			hasCollided = true; // Ã¹ ¹øÂ° Ãæµ¹ ¹ß»ý
			Debug.Log("°øÀÌ ¶óÄÏ°ú Ãæµ¹Çß½À´Ï´Ù.");
			Vector3 collisionPoint = collision.contacts[0].point; // Ãæµ¹ ÁöÁ¡ ÁÂÇ¥ ±â·Ï
			Debug.Log("Ãæµ¹ À§Ä¡: " + collisionPoint);
			
			// Ãæµ¹ ¼Ò¸® Àç»ý
			if (audioSource != null && collisionSound != null)
			{
				audioSource.PlayOneShot(collisionSound);
			}

			// Guide_Detection ³»ÀÇ Å¥ºê¿Í Ãæµ¹ À§Ä¡ ÆÇÁ¤
			// Guide_Detection ³»ÀÇ DetectionBox ÀÚ½Ä ¿ÀºêÁ§Æ®¸¦ Ã£°í, ±× ÀÚ½Ä ¿ÀºêÁ§Æ®µé Áß Detection Å¥ºê¿Í Ãæµ¹ À§Ä¡ ÆÇÁ¤
			Transform detectionBox = guideDetection.transform.Find("DetectionBox");
			if (detectionBox != null)
			{
				foreach (Transform child in detectionBox)
				{
					Collider cubeCollider = child.GetComponent<Collider>();
					if (cubeCollider != null && cubeCollider.bounds.Contains(collisionPoint))
					{
						int scoreToAdd = 0;
						if (child.CompareTag("DetectionBad"))
						{
							scoreToAdd = 50;
						}
						else if (child.CompareTag("DetectionPerfect"))
						{
							scoreToAdd = 100;
						}

						if (scoreToAdd > 0 && onScoreUpdate != null)
						{
							onScoreUpdate(scoreToAdd);
						}
						Debug.Log(child.name + " Å¥ºê¿Í Ãæµ¹Çß½À´Ï´Ù! Á¡¼ö¸¦ ¾ò½À´Ï´Ù: " + scoreToAdd);
					}
				}
			}
		}
	}
}
