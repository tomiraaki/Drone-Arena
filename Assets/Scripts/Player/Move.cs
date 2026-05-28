using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 5f;

    [Header("Referências")]
    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    void Update()
    {
        // Movimento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Posição do mouse
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Movimentação
        rb.velocity = movement.normalized * moveSpeed;

        // Rotação para o mouse
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = angle - 90f;
    }
}
