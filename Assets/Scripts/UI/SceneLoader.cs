using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void QuitGame()
    {
        Application.Quit();
        print("Quit!");
    }

    public void FadeToScene()
    {
        animator.SetTrigger("FadeOut");   
    }

    public void OnFadeComplete(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
