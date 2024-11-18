using UnityEngine;
using System.Collections;
using TMPro;

public class ProjectileOptimizer : MonoBehaviour
{
	public TextMeshProUGUI LoadingText;
	public GameObject ballPrefab;            // 공 prefab
	public GameObject savedObject;
	public Transform cannon1Transform;       // 1번 대포 Hierarchy
	public Transform cannon2Transform;       // 2번 대포 Hierarchy
	public Transform cannon3Transform;       // 3번 대포 Hierarchy
	public Transform cannon4Transform;       // 4번 대포 Hierarchy
	public Transform ballGuideLUTransform;   // 좌상단 공 도착지점 Hierarchy
	public Transform ballGuideLDTransform;   // 좌하단 공 도착지점 Hierarchy
	public Transform ballGuideRUTransform;   // 우상단 공 도착지점 Hierarchy
	public Transform ballGuideRDTransform;   // 우하단 공 도착지점 Hierarchy
	public float desiredTime = 1.2f;           // 목표 도착 시간
	private float maxSimulationTime = 2.0f;     // 시뮬레이션 시간
	private float angleStep = 0.3f;             // 각도 증가 단계 (도 단위)
	private float maxAngle = 20.0f;             // 최대 각도
	private float minAngle = 12.0f;             // 최소 각도 deg

	private float bestVelocity1 = 0f;
	private float bestVelocity2 = 0f;
	private float bestVelocity3 = 0f;
	private float bestVelocity4 = 0f;
	private float bestAngle1 = 0f;//deg
	private float bestAngle2 = 0f;
	private float bestAngle3 = 0f;
	private float bestAngle4 = 0f;
	private float minError1 = float.MaxValue;
	private float minError2 = float.MaxValue;
	private float minError3 = float.MaxValue;
	private float minError4 = float.MaxValue;

	private bool coroutine1Done = false;
	private bool coroutine2Done = false;
	private bool coroutine3Done = false;
	private bool coroutine4Done = false;


	void Start()
	{
		// 시작 시 시뮬레이션을 실행하여 최적의 발사 각도와 속도를 찾음
		StartCoroutine(FindOptimalLaunchParameters1());
		StartCoroutine(FindOptimalLaunchParameters2());
		StartCoroutine(FindOptimalLaunchParameters3());
		StartCoroutine(FindOptimalLaunchParameters4());

	}

	IEnumerator FindOptimalLaunchParameters1()
	{
		// LU 시뮬레이션
		for (float angleDeg = minAngle; angleDeg <= maxAngle; angleDeg += angleStep)
		{
			float angle = angleDeg * Mathf.Deg2Rad; //rad
			float distance = Vector3.Distance(cannon1Transform.position, ballGuideLUTransform.position);
			float velocity = distance / (desiredTime * Mathf.Cos(angle));

			GameObject ball = Instantiate(ballPrefab, cannon1Transform.position, cannon1Transform.rotation);
			Rigidbody rb = ball.GetComponent<Rigidbody>();
			ProjectileMotion projectileMotion = ball.GetComponent<ProjectileMotion>();
			if (projectileMotion != null)
			{
				projectileMotion.startPoint = cannon1Transform;
				projectileMotion.targetPoint = ballGuideLUTransform;
			}

			if (rb != null)
			{
				// 초기 속도 설정
				Vector3 direction = ballGuideLUTransform.position - cannon1Transform.position;
				Vector3 velocityVector = direction.normalized * Mathf.Cos(angle) * velocity + Vector3.up * Mathf.Sin(angle) * velocity;
				projectileMotion.angleDeg = angleDeg;
				projectileMotion.velocity = velocity;
				//Debug.Log("angledeg: " + angleDeg);
				//Debug.Log("velocity: " + velocity);
				rb.velocity = velocityVector;

				//Debug.Log("테스트중인속도"+rb.velocity);

				// 시뮬레이션 대기
				yield return new WaitForSeconds(desiredTime);

				// 공의 위치와 목표 지점 비교
				float error = Vector3.Distance(ball.transform.position, ballGuideLUTransform.position);
				if (error < minError1)
				{
					minError1 = error;
					bestAngle1 = angleDeg;
					bestVelocity1 = velocity;
				}
				//Debug.Log("차이1: " + minError1);
				//Debug.Log("Best Angle1: " + bestAngle1);
				//Debug.Log("Best Velocity1: " + bestVelocity1);

			}

			// 시뮬레이션 후 공 제거
			Destroy(ball);
			if (minError1 < 0.1f) break;
		}

		// 최적의 각도와 속도를 기록
		Debug.Log("Final Best Angle1: " + bestAngle1);
		Debug.Log("Final Best Velocity1: " + bestVelocity1);
		coroutine1Done = true;

		// 모든 코루틴 완료 여부 확인
		CheckAllCoroutinesCompleted();

	}
	IEnumerator FindOptimalLaunchParameters2()
	{
		// LD 시뮬레이션
		for (float angleDeg = minAngle; angleDeg <= maxAngle; angleDeg += angleStep)
		{
			float angle = angleDeg * Mathf.Deg2Rad; //rad
			float distance = Vector3.Distance(cannon2Transform.position, ballGuideLDTransform.position);
			float velocity = distance / (desiredTime * Mathf.Cos(angle));

			GameObject ball = Instantiate(ballPrefab, cannon2Transform.position, cannon2Transform.rotation);
			Rigidbody rb = ball.GetComponent<Rigidbody>();
			ProjectileMotion projectileMotion = ball.GetComponent<ProjectileMotion>();
			if (projectileMotion != null)
			{
				projectileMotion.startPoint = cannon2Transform;
				projectileMotion.targetPoint = ballGuideLDTransform;
			}

			if (rb != null)
			{
				// 초기 속도 설정
				Vector3 direction = ballGuideLDTransform.position - cannon2Transform.position;
				Vector3 velocityVector = direction.normalized * Mathf.Cos(angle) * velocity + Vector3.up * Mathf.Sin(angle) * velocity;
				projectileMotion.angleDeg = angleDeg;
				projectileMotion.velocity = velocity;
				//Debug.Log("angledeg: " + angleDeg);
				//Debug.Log("velocity: " + velocity);
				rb.velocity = velocityVector;

				//Debug.Log("테스트중인속도"+rb.velocity);

				// 시뮬레이션 대기
				yield return new WaitForSeconds(desiredTime);

				// 공의 위치와 목표 지점 비교
				float error = Vector3.Distance(ball.transform.position, ballGuideLDTransform.position);
				if (error < minError2)
				{
					minError2 = error;
					bestAngle2 = angleDeg;
					bestVelocity2 = velocity;
				}
				//Debug.Log("차이2: " + minError2);
				//Debug.Log("Best Angle1: " + bestAngle1);
				//Debug.Log("Best Velocity1: " + bestVelocity1);

			}

			// 시뮬레이션 후 공 제거
			Destroy(ball);
			if (minError2 < 0.1f) break;
		}

		// 최적의 각도와 속도를 기록
		Debug.Log("Final Best Angle2: " + bestAngle2);
		Debug.Log("Final Best Velocity2: " + bestVelocity2);
		coroutine2Done = true;

		// 모든 코루틴 완료 여부 확인
		CheckAllCoroutinesCompleted();

	}
	IEnumerator FindOptimalLaunchParameters3()
	{
		// RU 시뮬레이션
		for (float angleDeg = minAngle; angleDeg <= maxAngle; angleDeg += angleStep)
		{
			float angle = angleDeg * Mathf.Deg2Rad; //rad
			float distance = Vector3.Distance(cannon3Transform.position, ballGuideRUTransform.position);
			float velocity = distance / (desiredTime * Mathf.Cos(angle));

			GameObject ball = Instantiate(ballPrefab, cannon3Transform.position, cannon3Transform.rotation);
			Rigidbody rb = ball.GetComponent<Rigidbody>();
			ProjectileMotion projectileMotion = ball.GetComponent<ProjectileMotion>();
			if (projectileMotion != null)
			{
				projectileMotion.startPoint = cannon3Transform;
				projectileMotion.targetPoint = ballGuideRUTransform;
			}

			if (rb != null)
			{
				// 초기 속도 설정
				Vector3 direction = ballGuideRUTransform.position - cannon3Transform.position;
				Vector3 velocityVector = direction.normalized * Mathf.Cos(angle) * velocity + Vector3.up * Mathf.Sin(angle) * velocity;
				projectileMotion.angleDeg = angleDeg;
				projectileMotion.velocity = velocity;
				//Debug.Log("angledeg: " + angleDeg);
				//Debug.Log("velocity: " + velocity);
				rb.velocity = velocityVector;

				//Debug.Log("테스트중인속도"+rb.velocity);

				// 시뮬레이션 대기
				yield return new WaitForSeconds(desiredTime);

				// 공의 위치와 목표 지점 비교
				float error = Vector3.Distance(ball.transform.position, ballGuideRUTransform.position);
				if (error < minError3)
				{
					minError3 = error;
					bestAngle3 = angleDeg;
					bestVelocity3 = velocity;
				}
				//Debug.Log("차이3: " + minError3);
				//Debug.Log("Best Angle1: " + bestAngle1);
				//Debug.Log("Best Velocity1: " + bestVelocity1);

			}

			// 시뮬레이션 후 공 제거
			Destroy(ball);
			if (minError3 < 0.1f) break;
		}

		// 최적의 각도와 속도를 기록
		Debug.Log("Final Best Angle3: " + bestAngle3);
		Debug.Log("Final Best Velocity3: " + bestVelocity3);
		coroutine3Done = true;

		// 모든 코루틴 완료 여부 확인
		CheckAllCoroutinesCompleted();

	}
	IEnumerator FindOptimalLaunchParameters4()
	{
		// RD 시뮬레이션
		for (float angleDeg = minAngle; angleDeg <= maxAngle; angleDeg += angleStep)
		{
			float angle = angleDeg * Mathf.Deg2Rad; //rad
			float distance = Vector3.Distance(cannon4Transform.position, ballGuideRDTransform.position);
			float velocity = distance / (desiredTime * Mathf.Cos(angle));

			GameObject ball = Instantiate(ballPrefab, cannon4Transform.position, cannon4Transform.rotation);
			Rigidbody rb = ball.GetComponent<Rigidbody>();
			ProjectileMotion projectileMotion = ball.GetComponent<ProjectileMotion>();
			if (projectileMotion != null)
			{
				projectileMotion.startPoint = cannon4Transform;
				projectileMotion.targetPoint = ballGuideRDTransform;
			}

			if (rb != null)
			{
				// 초기 속도 설정
				Vector3 direction = ballGuideRDTransform.position - cannon4Transform.position;
				Vector3 velocityVector = direction.normalized * Mathf.Cos(angle) * velocity + Vector3.up * Mathf.Sin(angle) * velocity;
				projectileMotion.angleDeg = angleDeg;
				projectileMotion.velocity = velocity;
				//Debug.Log("angledeg: " + angleDeg);
				//Debug.Log("velocity: " + velocity);
				rb.velocity = velocityVector;

				//Debug.Log("테스트중인속도"+rb.velocity);

				// 시뮬레이션 대기
				yield return new WaitForSeconds(desiredTime);

				// 공의 위치와 목표 지점 비교
				float error = Vector3.Distance(ball.transform.position, ballGuideRDTransform.position);
				if (error < minError4)
				{
					minError4 = error;
					bestAngle4 = angleDeg;
					bestVelocity4 = velocity;
				}
				//Debug.Log("차이4: " + minError4);
				//Debug.Log("Best Angle1: " + bestAngle1);
				//Debug.Log("Best Velocity1: " + bestVelocity1);

			}

			// 시뮬레이션 후 공 제거
			Destroy(ball);
			if (minError4 < 0.1f) break;
		}

		// 최적의 각도와 속도를 기록
		Debug.Log("Final Best Angle4: " + bestAngle4);
		Debug.Log("Final Best Velocity4: " + bestVelocity4);
		coroutine4Done = true;

		// 모든 코루틴 완료 여부 확인
		CheckAllCoroutinesCompleted();

	}
	void CheckAllCoroutinesCompleted()
	{
		if (coroutine1Done && coroutine2Done && coroutine3Done && coroutine4Done)
		{
			// 모든 코루틴이 완료된 후 실행할 작업
			Debug.Log("All coroutines have completed!");
			LaunchAfterAllSimulations();
		}
	}

	void LaunchAfterAllSimulations()
	{
		// 모든 시뮬레이션 완료 후 실행할 작업 코드 작성
		Debug.Log("Executing post-simulation task...");
		SavedValues savedValues = savedObject.GetComponent<SavedValues>();
		savedValues.velocity1 = bestVelocity1;
		savedValues.velocity2 = bestVelocity2;
		savedValues.velocity3 = bestVelocity3;
		savedValues.velocity4 = bestVelocity4;
		savedValues.angle1 = bestAngle1;
		savedValues.angle2 = bestAngle2;
		savedValues.angle3 = bestAngle3;
		savedValues.angle4 = bestAngle4;
		Debug.Log("angle4 = " + savedValues.angle4);
		LoadingText.text = "Ready";
	}
}
