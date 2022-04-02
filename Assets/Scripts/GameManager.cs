using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro levelText;

    public int level { get; private set; }

    void Awake()
    {
        instance = this;
        level = 1;
    }

    public void AddScore()
    {
        ScoreManager.instance.CurrentScore++;
        scoreText.text = "Score: " + ScoreManager.instance.CurrentScore.ToString();
        level = ScoreManager.instance.CurrentScore/10 + 1;
        levelText.text = "Level: " + level.ToString();
    }

    public IEnumerator GameOver()
    {
        ScoreManager.instance.UpdateHighscore();

        yield return StartCoroutine(LoadingScreen.instance.ShowLoadingScreen());

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
