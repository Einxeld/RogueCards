using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highscoreTxt;
    [SerializeField] TextMeshProUGUI scoreTxt;

    void Awake()
    {
        highscoreTxt.text = "Highscore: " + ScoreManager.instance.Highscore.ToString();
        scoreTxt.text = "Score: " + ScoreManager.instance.CurrentScore.ToString();
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameIE());
    }

    public IEnumerator RestartGameIE()
    {
        ScoreManager.instance.UpdateHighscore();

        yield return StartCoroutine(LoadingScreen.instance.ShowLoadingScreen());

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
