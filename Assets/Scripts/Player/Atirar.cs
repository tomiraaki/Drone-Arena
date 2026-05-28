using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.2f;

    private float nextFireTime;

    public bool explosiveShot = false;

    [Header("Sprites")]
    public Sprite normalBulletSprite;

    public Sprite explosiveBulletSprite;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Tiro tiroScript =
            bullet.GetComponent<Tiro>();

        SpriteRenderer sr =
            bullet.GetComponent<SpriteRenderer>();

        if (tiroScript != null)
        {
            tiroScript.isExplosive = explosiveShot;
        }

        // Troca sprite
        if (sr != null)
        {
            if (explosiveShot)
            {
                sr.sprite = explosiveBulletSprite;
            }
            else
            {
                sr.sprite = normalBulletSprite;
            }
        }
    }
}