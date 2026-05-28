using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Kaboom itemManager =
                collision.GetComponent<Kaboom>();

            // Só pega se não tiver item
            if (itemManager != null &&
                !itemManager.hasItem)
            {
                itemManager.PickMedkit(gameObject);
            }
        }
    }
}