using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadTime = 1f;
    [SerializeField] GameObject crashVFX;

    PlayerController playerController;
    Collider playerCollider;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    void StartDeathSequence()
    {
        playerController.enabled = false;
        playerCollider.enabled = false;

        DisableMeshRenderers();
        crashVFX.SetActive(true);
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
