using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] IntScriptableObject checkpointData;

    PlayableDirector playableDirector;

    void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();

        playableDirector.time = checkpointData.Value;
    }

    public void SetCheckpointAt(int time)
    {
        checkpointData.Value = time;
    }

    public void ResetCheckpoint()
    {
        checkpointData.Value = 0;
    }
}
