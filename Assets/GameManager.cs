using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
		}
		else
		{
			Destroy(gameObject);
		}
		Application.targetFrameRate = 72; // Meta Quest 2의 주사율에 맞춰 프레임 제한 설정
	}

}
