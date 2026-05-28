using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public void PlayGame()
    {
        SceneManager.LoadScene(1); // troca para a cena do jogo (fase 1)
        Time.timeScale = 1f;
    }

    public void BackMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // troca para a cena do jogo (menu)
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); // mensagem no console
        Application.Quit(); // fecha o jogo
    }

    void Start()
    {
        int highScore =
            PlayerPrefs.GetInt(
                "HighScore",
                0
            );

        highScoreText.text = "Meu Maior Recorde   " + highScore;
    }
}