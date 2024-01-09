using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLastScore : MonoBehaviour
{
    [SerializeField]
    Text lastScoreUI;
    [SerializeField]
    GameObject newRecordText;

    private void Awake()
    {
        newRecordText.SetActive(false);
    }

    void Start()
    {
        lastScoreUI.text = "Score: " + ScoreCounter.Score;
        CheckNewHighScore();
    }

    public void CheckNewHighScore()
    {
        
        int OldScore = PlayerPrefs.GetInt("highScore", 0);
        int NewScore = ScoreCounter.Score;

        if (OldScore <= NewScore)
        {
            newRecordText.SetActive(true);
        }
    }
}
