using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] IntScriptableObject score;
    [SerializeField] IntScriptableObject checkpointScore;

    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        score.Value = checkpointScore.Value;
        scoreText.text = score.Value.ToString();
    }

    public void IncreaseScore(int amoutToIncrease)
    {
        score.Value += amoutToIncrease;
        UpdateScoreUI();
    }

    public void ResetCheckpointScore()
    {
        checkpointScore.Value = 0;
    }

    public void ResetScore()
    {
        ResetCheckpointScore();
        score.Value = checkpointScore.Value;
    }

    public void UpdateCheckpointScore()
    {
        checkpointScore.Value = score.Value;
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.Value.ToString();
    }

}
