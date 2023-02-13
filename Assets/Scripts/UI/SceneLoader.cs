using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator animator;

    int sceneToLoadIndex;
    
    public void QuitGame()
    {
        Application.Quit();
        print("Quit!");
    }

    public void FadeToScene(int levelIndex)
    {
        sceneToLoadIndex = levelIndex;
        animator.SetTrigger("FadeOut");   
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }
}
