using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateAnimation : MonoBehaviour
{
    public Sprite[] sprites; // Vetor de sprites com as 8 posições.
    public SpriteRenderer spriteRenderer;
    public float framesPerSecond = 10.0f; // Velocidade da animação.

    private int currentSprite = 0;
    private int direction = 1;
    private float timePerFrame;
    private float nextFrameTime;

    private void Start()
    {
        timePerFrame = 0.156f / framesPerSecond;
        spriteRenderer.sprite = sprites[currentSprite];
        nextFrameTime = Time.time + timePerFrame;
    }

    private void Update()
    {
        if (Time.time >= nextFrameTime)
        {
            currentSprite += direction;

            if (currentSprite == 0 || currentSprite == sprites.Length - 1)
            {
                direction = -direction;
            }

            currentSprite = Mathf.Clamp(currentSprite, 0, sprites.Length - 1);
            spriteRenderer.sprite = sprites[currentSprite];

            nextFrameTime = Time.time + timePerFrame;
        }
    }
}