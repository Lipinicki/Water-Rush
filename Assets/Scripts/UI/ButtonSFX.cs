using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hoverSFX;
    [SerializeField] AudioClip pressSFX;

    public void PlayHoverSFX()
    {
        audioSource.PlayOneShot(hoverSFX);
    }

    public void PlayPressSFX()
    {
        audioSource.PlayOneShot(pressSFX);
    }
}
