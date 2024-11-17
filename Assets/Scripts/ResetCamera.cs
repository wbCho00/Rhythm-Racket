using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCamera : MonoBehaviour
{
	public GameObject cameraPosition;
	public float posY = 1.7f;

	public void OnResetCameraButtonClick()
	{
		cameraPosition.transform.position = new Vector3(0, posY, 0);
		cameraPosition.transform.rotation = Quaternion.Euler(0, 0, 0);
	}
}
