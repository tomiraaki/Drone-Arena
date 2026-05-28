using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inimigo : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 3f;

    [Header("Pontuação")]
    public int points = 1;
    public int health = 1;

    private Transform player;

    void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Movimento
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );

            // Direção
            Vector2 direction =
                player.position - transform.position;

            // Ângulo
            float angle =
                Mathf.Atan2(direction.y, direction.x)
                * Mathf.Rad2Deg;

            // Rotação
            transform.rotation =
                Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Encostou no player
        if (collision.CompareTag("Player"))
        {
            PlayerPV health =
                collision.GetComponent<PlayerPV>();

            if (health != null)
            {
                health.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Pontos.instance.AddScore(points);

            Destroy(gameObject);
        }
    }
}