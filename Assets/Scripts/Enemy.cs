using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] GameObject deathFX;
	[SerializeField] GameObject hitVFX;
	[SerializeField] int scorePerHit = 25;
	[SerializeField] int hitPoints = 1;

	GameObject parentGameObject;
	ScoreBoard scoreBoard;
	AudioSource enemyAudio;

	void Start()
	{
		scoreBoard = FindObjectOfType<ScoreBoard>();
		parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
		enemyAudio = GetComponent<AudioSource>();
		AddRigidbody();
	}


	void OnParticleCollision(GameObject other)
	{
		ProcessHit();
		if (hitPoints == 0)
		{
			KillEnemy();
		}
	}

	void AddRigidbody()
	{
		if (GetComponent<Collider>() == null)
		{
			Rigidbody rb = gameObject.AddComponent<Rigidbody>();
			rb.useGravity = false;
			rb.isKinematic = true;
		}
	}

	void ProcessHit()
	{
		GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
		vfx.transform.parent = parentGameObject.transform;

		enemyAudio.Play();

		hitPoints--;
	}

	void KillEnemy()
	{
		scoreBoard.IncreaseScore(scorePerHit);

		GameObject fx = Instantiate(deathFX, transform.position, transform.rotation);
		fx.transform.parent = parentGameObject.transform;

		Destroy(this.gameObject);
	}
}
