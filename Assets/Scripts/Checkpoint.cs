using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] CheckpointScriptableObject checkpointData;

    PlayableDirector playableDirector;

    void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();

        playableDirector.time = checkpointData.currentInitialTime;
    }

    public void SetCheckpointAt(int time)
    {
        checkpointData.currentInitialTime = time;
    }
}
