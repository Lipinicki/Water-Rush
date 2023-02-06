using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] GameObject deathVFX;
	[SerializeField] GameObject hitVFX;
	[SerializeField] int scorePerHit = 25;
	[SerializeField] int hitPoints = 1;

	GameObject parentGameObject;
	ScoreBoard scoreBoard;

	void Start()
	{
		scoreBoard = FindObjectOfType<ScoreBoard>();
		parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
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
		Rigidbody rb = gameObject.AddComponent<Rigidbody>();
		rb.useGravity = false;
		rb.isKinematic = true;
	}

	void ProcessHit()
	{
		GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
		vfx.transform.parent = parentGameObject.transform;

		hitPoints--;
	}

	void KillEnemy()
	{
		scoreBoard.IncreaseScore(scorePerHit);

		GameObject vfx = Instantiate(deathVFX, transform.position, transform.rotation);
		vfx.transform.parent = parentGameObject.transform;

		Destroy(this.gameObject);
	}
}
