using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaSpawn : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject bombPrefab;

    public GameObject medkitPrefab;

    [Header("Player")]
    public Transform player;

    [Header("Área")]
    public float minX = -8f;
    public float maxX = 8f;

    public float minY = -5f;
    public float maxY = 5f;

    [Header("Sistema")]
    public int scoreNeeded = 20;

    public float spawnInterval = 10f;

    private bool systemUnlocked = false;

    private Kaboom itemManager;

    private float timer;

    void Start()
    {
        itemManager =
            player.GetComponent<Kaboom>();
    }

    void Update()
    {
        // Libera sistema
        if (!systemUnlocked &&
            Pontos.instance.GetScore() >= scoreNeeded)
        {
            systemUnlocked = true;

            Debug.Log("Sistema de itens liberado!");
        }

        // Sistema ativo
        if (systemUnlocked)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                timer = 0f;

                SpawnItem();
            }
        }
    }

    void SpawnItem()
    {
        // Player já tem item
        if (itemManager.hasItem)
            return;

        // Já existe item no mapa
        GameObject bomb =
            GameObject.FindGameObjectWithTag("bomba");

        GameObject medkit =
            GameObject.FindGameObjectWithTag("medkit");

        if (bomb != null || medkit != null)
            return;

        Vector2 randomPosition =
            new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
            );

        int randomItem = Random.Range(0, 2);

        if (randomItem == 0)
        {
            Instantiate(
                bombPrefab,
                randomPosition,
                Quaternion.identity
            );

            Debug.Log("Bomba spawnada");
        }
        else
        {
            Instantiate(
                medkitPrefab,
                randomPosition,
                Quaternion.identity
            );

            Debug.Log("Medkit spawnado");
        }
    }
}