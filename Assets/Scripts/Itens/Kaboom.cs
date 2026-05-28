using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kaboom : MonoBehaviour
{
    public enum ItemType
    {
        None,
        Bomb,
        Medkit
    }

    [Header("Estado")]
    public bool hasItem = false;
    public ItemType currentItem = ItemType.None;

    [Header("Visuais")]
    public GameObject bombVisual;
    public GameObject medkitVisual;

    [Header("HUD")]
    public GameObject bombHUD;
    public GameObject medkitHUD;

    [Header("Explosão")]
    public float explosionRadius = 5f;

    [Header("Efeitos")]
    public GameObject explosionEffectPrefab;

    public float explosionEffectTime = 0.5f;

    void Start()
    {
        bombVisual.SetActive(false);
        medkitVisual.SetActive(false);

        bombHUD.SetActive(false);
        medkitHUD.SetActive(false);
    }

    void Update()
    {
        if (hasItem &&
            Input.GetKeyDown(KeyCode.Space))
        {
            UseItem();
        }
    }

    // PEGAR BOMBA
    public void PickBomb(GameObject bombObject)
    {
        hasItem = true;

        currentItem = ItemType.Bomb;

        Destroy(bombObject);

        bombVisual.SetActive(true);

        bombHUD.SetActive(true);
    }

    // PEGAR MEDKIT
    public void PickMedkit(GameObject medkitObject)
    {
        hasItem = true;

        currentItem = ItemType.Medkit;

        Destroy(medkitObject);

        medkitVisual.SetActive(true);

        medkitHUD.SetActive(true);
    }

    // USAR ITEM
    void UseItem()
    {
        switch (currentItem)
        {
            case ItemType.Bomb:
                UseBomb();
                break;

            case ItemType.Medkit:
                UseMedkit();
                break;
        }

        ResetItem();
    }

    // BOMBA
    void UseBomb()
    {
        // Efeito visual
        GameObject explosion =
            Instantiate(
                explosionEffectPrefab,
                transform.position,
                Quaternion.identity
            );

        // Ajusta tamanho pela área
        explosion.transform.localScale =
            Vector3.one * explosionRadius;

        Destroy(
            explosion,
            explosionEffectTime
        );

        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                transform.position,
                explosionRadius
            );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("inimigo"))
            {
                Destroy(hit.gameObject);

                Pontos.instance.AddScore(1);
            }
        }
    }

    // MEDKIT
    void UseMedkit()
    {
        PlayerPV health =
            GetComponent<PlayerPV>();

        if (health != null)
        {
            health.Heal(1);
        }
    }

    // RESETAR ITEM
    void ResetItem()
    {
        hasItem = false;

        currentItem = ItemType.None;

        bombVisual.SetActive(false);
        medkitVisual.SetActive(false);

        bombHUD.SetActive(false);
        medkitHUD.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            explosionRadius
        );
    }
}