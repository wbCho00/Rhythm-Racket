using UnityEngine;

public class BallCollisionLogger : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		// 큐브와 충돌했을 때 로그 표시
		if (other.CompareTag("DetectionBad"))
		{
			Debug.Log($"공이 {other.gameObject.name}(배드판정)과(와) 충돌했습니다.");
		}
		if (other.CompareTag("DetectionPerfect"))
		{
			Debug.Log($"공이 {other.gameObject.name}(퍼펙트판정)과(와) 충돌했습니다.");
		}
	}
}
