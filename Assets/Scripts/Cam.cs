using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [Header("Player")]
    public Transform target;

    [Header("Suavização")]
    public float smoothSpeed = 5f;

    [Header("Limites")]
    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    void LateUpdate()
    {
        if (target == null) return;

        // Posição desejada
        Vector3 desiredPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        // Suavização
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Limites
        smoothedPosition.x =
            Mathf.Clamp(smoothedPosition.x, minX, maxX);

        smoothedPosition.y =
            Mathf.Clamp(smoothedPosition.y, minY, maxY);

        // Aplicação
        transform.position = smoothedPosition;
    }
}