using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 25;
    [SerializeField] int hitPoints = 1;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();    
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints == 0)
        {
			KillEnemy();
		}
    }

	void ProcessHit()
	{
        GameObject vfx = Instantiate(hitVFX, transform.position, transform.rotation);
        vfx.transform.parent = parent;

		scoreBoard.IncreaseScore(scorePerHit);
        hitPoints--;
	}

	void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, transform.rotation);
        vfx.transform.parent = parent;

        Destroy(this.gameObject);
    }
}
