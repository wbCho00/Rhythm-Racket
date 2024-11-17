using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
	public Transform startPoint;    // 시작 위치 오브젝트
	public Transform targetPoint;   // 목표 위치 오브젝트
	public Rigidbody rb;            // 날아가는 물체의 Rigidbody
	public float launchAngle = 45f; // 발사 각도 (도 단위)

	void Start()
	{
		rb.position = startPoint.position;
		LaunchProjectile();
	}

	void LaunchProjectile()
	{
		Vector3 direction = targetPoint.position - startPoint.position; // 목표 지점까지의 방향 벡터
		float distance = direction.magnitude; // 목표 지점까지의 거리

		// 중력 가속도
		float gravity = Physics.gravity.magnitude;

		// 발사 각도를 라디안으로 변환
		float angle = launchAngle * Mathf.Deg2Rad;

		// 초기 속력 계산
		float velocity = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * angle));

		// 속도의 방향 벡터 계산
		Vector3 velocityVector = direction.normalized * Mathf.Cos(angle) * velocity + Vector3.up * Mathf.Sin(angle) * velocity;

		// Rigidbody에 속도 적용
		rb.velocity = velocityVector;
	}
}
