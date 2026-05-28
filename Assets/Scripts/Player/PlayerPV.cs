using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerPV : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 10;
    private int currentHealth;
    public GameObject gameOverScreen;

    [Header("Invulnerabilidade")]
    public float invulnerableTime = 2f;
    private bool isInvulnerable = false;

    [Header("Piscar")]
    public SpriteRenderer spriteRenderer;
    public float blinkSpeed = 0.1f;

    [Header("UI")]
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    [Header("Game Over")]
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        currentHealth = maxHealth;

        healthBar.maxValue = maxHealth;

        UpdateHealthUI();
    }

    // DANO
    public void TakeDamage(int damage)
    {
        // Se estiver invulnerável
        if (isInvulnerable)
            return;

        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();

        StartCoroutine(Invulnerability());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // CURA
    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI();
    }

    // INVULNERABILIDADE
    IEnumerator Invulnerability()
    {
        isInvulnerable = true;

        float timer = 0f;

        while (timer < invulnerableTime)
        {
            // Pisca
            spriteRenderer.enabled =
                !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkSpeed);

            timer += blinkSpeed;
        }

        // Garante sprite visível
        spriteRenderer.enabled = true;

        isInvulnerable = false;
    }

    // UI
    void UpdateHealthUI()
    {
        healthBar.value = currentHealth;

        healthText.text =
            currentHealth + " / " + maxHealth;
    }

    // MORTE
    void Die()
    {
        // Mostra pontuação da partida
        finalScoreText.text =
            "Meus Pontos " +
            Pontos.instance.GetScore();

        // Abre game over
        gameOverScreen.SetActive(true);

        // Pausa jogo
        Time.timeScale = 0f;

        // Destrói player
        Destroy(gameObject);
    }
}