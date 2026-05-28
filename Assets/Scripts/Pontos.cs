using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pontos : MonoBehaviour
{
    public static Pontos instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private int highScore;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt(
                "HighScore",
                highScore
            );

            PlayerPrefs.Save();
        }

        UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Pontos: " + score;
    }
}