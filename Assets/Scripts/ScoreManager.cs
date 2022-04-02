using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int Highscore { get; private set; }
    public int CurrentScore { get; set; }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            Highscore = PlayerPrefs.GetInt("Highscore", 0);
            DontDestroyOnLoad(this);
        }
    }

    public void UpdateHighscore()
    {
        if (CurrentScore > Highscore)
        {
            Highscore = CurrentScore;
            PlayerPrefs.SetInt("Highscore", Highscore);
        }
    }
}
