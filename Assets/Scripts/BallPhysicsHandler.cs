using UnityEngine;

public class BallPhysicsHandler : MonoBehaviour
{
	void OnCollisionEnter(Collision collision)
	{
		// 라켓과 충돌했을 때 물리적 반응
		if (collision.gameObject.CompareTag("Rackets"))
		{
			Debug.Log("공이 라켓과 충돌하여 반응합니다.");
			// 여기서 물리적 반응을 처리할 수 있음
		}
	}
}
