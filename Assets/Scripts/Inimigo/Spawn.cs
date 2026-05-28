using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject enemyPrefab;

    [Header("Spawn")]
    public float spawnInterval = 2f;

    public int maxEnemies = 15;

    public float spawnDistance = 12f;

    private Transform player;

    [Header("Progressão")]
    public bool spawnStrongEnemies = false;

    public bool hordeMode = false;

    public GameObject strongEnemyPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("inimigo");

        if (enemies.Length >= maxEnemies)
            return;

        SpawnSingleEnemy();

        // Chance de horda
        if (hordeMode)
        {
            int randomChance =
                Random.Range(0, 100);

            // 25% de chance
            if (randomChance < 25)
            {
                SpawnHorde();
            }
        }
    }

    void SpawnSingleEnemy()
    {
        Vector2 randomDirection =
            Random.insideUnitCircle.normalized;

        Vector2 spawnPosition =
            (Vector2)player.position +
            (randomDirection * spawnDistance);

        GameObject enemyToSpawn =
            enemyPrefab;

        // Inimigo forte
        if (spawnStrongEnemies)
        {
            if (Random.value > 0.5f)
            {
                enemyToSpawn =
                    strongEnemyPrefab;
            }
        }

        Instantiate(
            enemyToSpawn,
            spawnPosition,
            Quaternion.identity
        );
    }

    void SpawnHorde()
    {
        int amount =
            Random.Range(5, 12);

        for (int i = 0; i < amount; i++)
        {
            Vector2 randomDirection =
                Random.insideUnitCircle.normalized;

            Vector2 spawnPosition =
                (Vector2)player.position +
                (randomDirection * spawnDistance);

            Vector2 randomOffset =
                Random.insideUnitCircle * 3f;

            GameObject enemyToSpawn =
                enemyPrefab;

            // Mistura inimigos fortes
            if (spawnStrongEnemies)
            {
                if (Random.value > 0.5f)
                {
                    enemyToSpawn =
                        strongEnemyPrefab;
                }
            }

            Instantiate(
                enemyToSpawn,
                spawnPosition + randomOffset,
                Quaternion.identity
            );
        }
    }
}