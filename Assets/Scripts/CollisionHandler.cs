using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadTime = 1f;
    [SerializeField] ParticleSystem crashVFX;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();

        //print($"{this.name} ** has triggered ** {other.gameObject.name}!");
    }

    void StartDeathSequence()
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Collider>().enabled = false;
        DisableMeshRenderers();
        crashVFX.Play();
        Invoke(nameof(ReloadLevel), reloadTime);
    }

    private void DisableMeshRenderers()
    {
        foreach(var renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }
    }

    void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
