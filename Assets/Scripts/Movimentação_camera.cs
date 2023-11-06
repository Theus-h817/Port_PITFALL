using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoCamera : MonoBehaviour
{
    public Transform personagem;
    public float suavizacao = 5f;
    public float velocidadeCamera = 2f;

    private float limiteEsquerdo = -14f; // Limite esquerdo da área jogável (metade da amplitude).
    private float limiteDireito = 110f;  // Limite direito da área jogável (metade da amplitude).

    private void LateUpdate()
    {
        if (personagem != null)
        {
            // Calcula a posição alvo da câmera.
            Vector3 posicaoAlvo = transform.position;
            float posicaoXAlvo = personagem.position.x;

            // Limita a posição alvo dentro dos limites esquerdo e direito.
            posicaoXAlvo = Mathf.Clamp(posicaoXAlvo, limiteEsquerdo, limiteDireito);

            // Move suavemente a câmera em direção à posição alvo.
            posicaoAlvo.x = Mathf.Lerp(transform.position.x, posicaoXAlvo, suavizacao * Time.deltaTime);
            transform.position = posicaoAlvo;

            // Move a câmera constantemente para a direita.
            transform.Translate(Vector3.right * velocidadeCamera * Time.deltaTime);
        }
    }
}
