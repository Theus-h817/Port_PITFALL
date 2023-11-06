using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoSprite : MonoBehaviour
{
    public float velocidade = 5.0f; // Ajuste a velocidade conforme necess�rio
    private Vector2 direcao = Vector2.left; // Define a dire��o inicial (para a esquerda)
    public float limiteEsquerdo = -15.0f;
    public float limiteDireito = 3.0f;

    void Update()
    {
        // Move o objeto na dire��o especificada
        transform.Translate(direcao * velocidade * Time.deltaTime);

        // Bloqueia o movimento no eixo X dentro dos limites

        Vector3 posicaoAtual = transform.position;
        posicaoAtual.x = Mathf.Clamp(posicaoAtual.x, limiteEsquerdo, limiteDireito);
        transform.position = posicaoAtual;

        // Inverte a dire��o e escala quando atinge um limite
        if (posicaoAtual.x == limiteEsquerdo || posicaoAtual.x == limiteDireito)
        {
            direcao *= -1;

            // Inverte a escala na dire��o X
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
}
