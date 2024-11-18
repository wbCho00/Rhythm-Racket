using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
	public Transform startPoint;    // 시작 위치 오브젝트
	public Transform targetPoint;   // 목표 위치 오브젝트
	public Rigidbody rb;            // 날아가는 물체의 Rigidbody
	public float desiredTime = 1.2f;       // 목표 도착 시간 (고정)
	public float velocity = 24f;  //계산된 속도
	public float angleDeg = 13f;		//계산된 각도 degree

	void Start()
	{
		rb.position = startPoint.position;
		LaunchProjectile();
	}

	void LaunchProjectile()
	{
		if (startPoint == null || targetPoint == null || rb == null)
		{
			Debug.LogError("StartPoint, TargetPoint, or Rigidbody is not assigned.");
			return;
		}

		// 목표 지점까지의 방향 벡터와 거리 계산
		Vector3 direction = targetPoint.position - startPoint.position;
		float distance = direction.magnitude;

		// 중력 가속도
		float gravity = Physics.gravity.magnitude;

		//// 발사 각도와 초기 속력 계산
		//float bestAngle = 0f;
		//float bestVelocity = 0f;
		//float minError = float.MaxValue;
		//bool foundSolution = false;

		//for (float angleDeg = 5f; angleDeg <= 20f; angleDeg += 0.1f)
		//{
		float angleRad = angleDeg * Mathf.Deg2Rad;
		//angleDeg;
		velocity = distance / (desiredTime * Mathf.Cos(angleRad));

		//	// 목표 도달 시간 동안의 공의 궤적을 추적하여 오차 계산
		//	float timeStep = 0.1f; // 0.1초 단위로 궤적 계산
		//	float currentTime = 0f;
		//	float maxHeightError = 0f;

		//	while (currentTime <= desiredTime)
		//	{
		//		// 특정 시간에서의 y 위치 계산
		//		float yPosition = startPoint.position.y + (velocity * Mathf.Sin(angle) * currentTime) - (0.5f * gravity * Mathf.Pow(currentTime, 2));

		//		// 목표 지점과의 높이 차이 계산
		//		float targetY = Mathf.Lerp(startPoint.position.y, targetPoint.position.y, currentTime / desiredTime);
		//		float heightError = Mathf.Abs(yPosition - targetY);

		//		// 최대 높이 오차를 추적
		//		if (heightError > maxHeightError)
		//		{
		//			maxHeightError = heightError;
		//		}

		//		currentTime += timeStep;
		//	}

		//	// 최적의 각도와 속도를 찾기 위한 비교
		//	if (maxHeightError < minError)
		//	{
		//		minError = maxHeightError;
		//		bestAngle = angleDeg;
		//		bestVelocity = velocity;
		//		foundSolution = true;
		//	}
		//}
	

		//if (!foundSolution)
		//{
		//	Debug.LogError("Could not find a valid launch angle and velocity within the given range.");
		//	return;
		//}

		//Debug.Log("Calculated velocity: " + bestVelocity);
		//Debug.Log("Calculated angle: " + bestAngle);

		// 속도의 방향 벡터 계산
		//angleRad = angle * Mathf.Deg2Rad; //radius
		Vector3 velocityVector = direction.normalized * Mathf.Cos(angleRad) * velocity + Vector3.up * Mathf.Sin(angleRad) * velocity;

		// Rigidbody에 속도 적용
		rb.velocity = velocityVector;
		//Debug.Log("PM스크립트 내 속도벡터: "+rb.velocity);
		//Debug.Log(angleRad);
		//Debug.Log(velocity);
		//
	}
}
