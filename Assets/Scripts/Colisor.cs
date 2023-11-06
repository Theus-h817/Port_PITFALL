using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atualizar_Colisor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Certifique-se de que o Collider2D está ativado
        polygonCollider.enabled = true;
    }

    void Update()
    {
        // Verifica se há uma sprite
        if (spriteRenderer.sprite != null)
        {
            // Atualiza o Collider2D para corresponder ao tamanho da sprite
            polygonCollider.SetPath(0, spriteRenderer.sprite.vertices);
        }
    }
}
