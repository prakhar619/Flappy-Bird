using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int CurrentScore;
    public static int HighScore;
    private TextMeshProUGUI Text;
    private TextMeshProUGUI HText;
    private GameObject ScoreBoardObj;
    private GameObject HighScoreObj;

    // Start is called before the first frame update
    void Awake()
    {
        ScoreBoardObj = GameObject.Find("ScoreBoard");
        HighScoreObj = GameObject.Find("HighScore");
        Text = ScoreBoardObj.GetComponent<TextMeshProUGUI>();
        HText = HighScoreObj.GetComponent<TextMeshProUGUI>();
        CurrentScore = 0;
    }

    void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore",0);
    }

    // Update is called once per frame
    void Update()
    {
        int actualScore = CurrentScore/2;
        Text.text = "Score:"+ actualScore.ToString();
        HText.text = "HighScore\n" + HighScore.ToString();
        if(actualScore > HighScore)
        {
            HighScore = actualScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }
}
