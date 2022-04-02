using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    int highScore;

    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] Image loadingScreenImg;
    [SerializeField] Button startBtn;

    void Start()
    {
        highScore = ScoreManager.instance.Highscore;
        highscoreText.text = "Highscore: " + highScore.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameIE());
    }

    IEnumerator StartGameIE()
    {
        startBtn.enabled = false;

        yield return StartCoroutine(LoadingScreen.instance.ShowLoadingScreen());

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
