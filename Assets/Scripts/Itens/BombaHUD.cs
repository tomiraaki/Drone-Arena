using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaHUD : MonoBehaviour
{
    public float blinkSpeed = 2f;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        float alpha =
            Mathf.PingPong(
                Time.time * blinkSpeed,
                1f
            );

        canvasGroup.alpha = alpha;
    }
}