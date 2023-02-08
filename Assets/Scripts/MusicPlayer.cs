using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	[SerializeField] bool haveOneInstanceOnly = true;

	void Awake()
	{
		int munsicPlayersCount = FindObjectsOfType<MusicPlayer>().Length;
		
		if (!haveOneInstanceOnly) return;

		if (munsicPlayersCount > 1)
		{
			Destroy(this.gameObject);
		}
		else
		{
			DontDestroyOnLoad(this.gameObject);
		}
	}
}
