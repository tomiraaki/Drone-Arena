using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float speed = 15f;
    public int damage = 1;
    public float lifeTime = 3f;

    private Rigidbody2D rb;

    [Header("Explosão")]
    public bool isExplosive = false;

    public float explosionRadius = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("inimigo"))
        {
            Inimigo hp =
                collision.GetComponent<Inimigo>();

            if (hp != null)
            {
                hp.TakeDamage(damage);
            }

            // Explosivo
            if (isExplosive)
            {
                Explode();
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("barreira"))
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                transform.position,
                explosionRadius
            );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("inimigo"))
            {
                Inimigo hp =
                    hit.GetComponent<Inimigo>();

                if (hp != null)
                {
                    hp.TakeDamage(damage);
                }

                Pontos.instance.AddScore(1);
            }
        }
    }
}