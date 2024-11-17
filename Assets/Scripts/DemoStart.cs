using UnityEngine;
using System.Collections;
using TMPro;

public class DemoStart : MonoBehaviour
{
	public GameObject guideDetection;        // Guide_Detection 오브젝트 (Hierarchy에서 연결)
	public GameObject mainCanvas;            // 메인 캔버스 오브젝트 Hierarchy
	public TextMeshProUGUI scoreText;
	public GameObject ballPrefab;            // 공 prefab
	public Transform cannon1Transform;       // 1번 대포 Hierarchy
	public Transform cannon2Transform;       // 2번 대포 Hierarchy
	public Transform cannon3Transform;       // 3번 대포 Hierarchy
	public Transform cannon4Transform;       // 4번 대포 Hierarchy
	public Transform ballGuideLUTransform;   // 좌상단 공 도착지점 Hierarchy
	public Transform ballGuideLDTransform;   // 좌하단 공 도착지점 Hierarchy
	public Transform ballGuideRUTransform;   // 우상단 공 도착지점 Hierarchy
	public Transform ballGuideRDTransform;   // 우하단 공 도착지점 Hierarchy
	public float launchInterval = 1.0f;      // 공을 발사할 시간 간격
	public float ballLifetime = 5.0f;        // 공의 최대 생존 시간
	public int score = 0;
	public AudioClip collisionSound;         // 충돌 시 재생할 소리
	public AudioSource audioSource;          // 오디오 소스 컴포넌트
	void Start()
	{
	}

	public void OnStartButtonClick()
	{
		////Guide_Detection 오브젝트 위치를(0, 0, 0)으로 이동
		//guideDetection.transform.position = Vector3.zero;
		////mainCanvas 오브젝트를 y축 방향으로 500 이동시켜서 보이지 않게 함
		//mainCanvas.transform.position += new Vector3(0, 500, 0);

		// 공 던지는 함수 호출
		StartCoroutine(LaunchBallsSequence());
	}

	IEnumerator LaunchBallsSequence()
	{
		scoreText.text = "Score: " + score;

		// Guide_Detection 오브젝트 위치를 (0, 0, 0)으로 이동
		guideDetection.transform.position = Vector3.zero;
		// mainCanvas 오브젝트를 y축 방향으로 500 이동시켜서 보이지 않게 함
		mainCanvas.transform.position += new Vector3(0, 500, 0);

		// 4개의 대포에서 공을 발사하는 루틴 시작
		yield return LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform);
		yield return new WaitForSeconds(launchInterval);
		yield return LaunchBall(ballPrefab, cannon2Transform, ballGuideLDTransform);
		yield return new WaitForSeconds(launchInterval);
		yield return LaunchBall(ballPrefab, cannon3Transform, ballGuideRUTransform);
		yield return new WaitForSeconds(launchInterval);
		yield return LaunchBall(ballPrefab, cannon4Transform, ballGuideRDTransform);

		// 추가로 공 발사하는 타이밍을 여기에 추가할 수 있습니다
		yield return new WaitForSeconds(launchInterval);
		yield return LaunchBall(ballPrefab, cannon1Transform, ballGuideLUTransform);


		// 곡 끝남
		yield return new WaitForSeconds(3.0f);
		// mainCanvas 오브젝트를 y축 방향으로 -500 이동시켜서 다시 보이게 함
		mainCanvas.transform.position += new Vector3(0, -500, 0);
		// Guide_Detection 오브젝트 위치를 (0, 500, 0)으로 이동시켜서 보이지 않게 함
		guideDetection.transform.position += new Vector3(0, 500, 0);
		scoreText.text = "";
		score = 0;
	}

	IEnumerator LaunchBall(GameObject ballPrefab, Transform cannonTransform, Transform targetTransform)
	{
		// 공의 인스턴스 생성
		GameObject ball = Instantiate(ballPrefab, cannonTransform.position, cannonTransform.rotation);

		// ProjectileMotion 스크립트에 시작 위치와 목표 위치 설정
		ProjectileMotion projectileMotion = ball.GetComponent<ProjectileMotion>();
		if (projectileMotion != null)
		{
			projectileMotion.startPoint = cannonTransform;
			projectileMotion.targetPoint = targetTransform;
		}
		// BallCollisionHandler 스크립트를 공에 추가
		BallCollisionHandler ballCollisionHandler = ball.AddComponent<BallCollisionHandler>();
		ballCollisionHandler.guideDetection = guideDetection;
		ballCollisionHandler.onScoreUpdate = UpdateScore;
		ballCollisionHandler.collisionSound = collisionSound;
		ballCollisionHandler.audioSource = ball.GetComponent<AudioSource>();

		// 일정 시간 후 공 제거 (최적화를 위해)
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
	public GameObject guideDetection; // Guide_Detection 오브젝트
	public System.Action<int> onScoreUpdate;
	public AudioClip collisionSound;   // 충돌 시 재생할 소리
	public AudioSource audioSource;    // 오디오 소스 컴포넌트
	private bool hasCollided = false;  // 첫 번째 충돌만 처리하기 위한 플래그

	void OnCollisionEnter(Collision collision)
	{
		if (hasCollided) return; // 이미 충돌했으면 이후 충돌 무시
		
		// 라켓과의 충돌을 감지
		if (collision.gameObject.CompareTag("Rackets"))
		{
			hasCollided = true; // 첫 번째 충돌 발생
			Debug.Log("공이 라켓과 충돌했습니다.");
			Vector3 collisionPoint = collision.contacts[0].point; // 충돌 지점 좌표 기록
			Debug.Log("충돌 위치: " + collisionPoint);
			
			// 충돌 소리 재생
			if (audioSource != null && collisionSound != null)
			{
				audioSource.PlayOneShot(collisionSound);
			}

			// Guide_Detection 내의 큐브와 충돌 위치 판정
			// Guide_Detection 내의 DetectionBox 자식 오브젝트를 찾고, 그 자식 오브젝트들 중 Detection 큐브와 충돌 위치 판정
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
						Debug.Log(child.name + " 큐브와 충돌했습니다! 점수를 얻습니다: " + scoreToAdd);
					}
				}
			}
		}
	}
}
