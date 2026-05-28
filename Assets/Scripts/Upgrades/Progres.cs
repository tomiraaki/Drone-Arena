using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progres : MonoBehaviour
{
    [Header("Referências")]
    public Spawn enemySpawner;

    public Atirar playerShooting;

    [Header("Pontuações")]

    public int fastSpawnScore = 20;

    public int explosiveShotScore = 60;

    public int strongEnemyScore = 120;

    public int fireRateScore = 240;

    public int hordeScore = 480;

    [Header("Estados")]
    private bool phase20;
    private bool phase60;
    private bool phase120;
    private bool phase240;
    private bool phase480;

    void Update()
    {
        int score = Pontos.instance.GetScore();

        // Spawn rápido
        if (!phase20 &&
            score >= fastSpawnScore)
        {
            phase20 = true;

            enemySpawner.spawnInterval = 0.5f;

            enemySpawner.CancelInvoke();

            enemySpawner.InvokeRepeating(
                "SpawnEnemy",
                1f,
                enemySpawner.spawnInterval
            );
        }

        // Tiro explosivo
        if (!phase60 &&
            score >= explosiveShotScore)
        {
            phase60 = true;

            playerShooting.explosiveShot = true;
        }

        // Inimigos fortes
        if (!phase120 &&
            score >= strongEnemyScore)
        {
            phase120 = true;

            enemySpawner.spawnStrongEnemies = true;
        }

        // Fire rate
        if (!phase240 &&
            score >= fireRateScore)
        {
            phase240 = true;

            playerShooting.fireRate = 0.2f;
        }

        // Hordas
        if (!phase480 &&
            score >= hordeScore)
        {
            phase480 = true;

            enemySpawner.hordeMode = true;
        }
    }
}