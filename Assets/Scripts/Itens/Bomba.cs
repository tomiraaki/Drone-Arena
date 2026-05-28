using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Kaboom bombManager =
                collision.GetComponent<Kaboom>();

            // Só pega se não tiver bomba
            if (bombManager != null && !bombManager.hasItem)
            {
                bombManager.PickBomb(gameObject);
            }
        }
    }
}