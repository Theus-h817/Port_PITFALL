using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subir_Descer_Escada : MonoBehaviour
{
    public List<Collider2D> contatosObjetosEscalaveis; // Lista de Colliders da escada para detec��o de contato.
    public Collider2D chaoCollider; // Collider do ch�o que voc� deseja ativar/desativar.
    public Correr scriptCorrer; // Refer�ncia ao script "Correr" para desativa��o.
    public Atualizar_Colisor scriptAtualizarColisor; // Refer�ncia ao script "Atualizar_Colisor" para desativa��o.

    private Rigidbody2D rb2d;
    private bool estaEscalando = false;

    public float velocidadeEscalada = 2.0f; // Velocidade de subida/descida
    public Sprite[] spritesSubida; // Array de sprites para anima��o de subida
    public Sprite[] spritesDescida; // Array de sprites para anima��o de descida

    private int spriteAtual = 0;
    private SpriteRenderer spriteRenderer;
    private Sprite spriteUltima; // �ltima sprite exibida

    public float velocidadeAnimacao = 0.2f; // Ajuste a velocidade da anima��o aqui

    private float tempoUltimaMudanca = 0f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (estaEscalando)
        {
            // Desativa a gravidade do jogador enquanto estiver escalando.
            rb2d.gravityScale = 0f;

            float movimentoVertical = Input.GetAxis("Vertical");
            float movimentoHorizontal = Input.GetAxis("Horizontal");

            // Verifica se o jogador est� pressionando o direcional para cima ou para baixo.
            bool estaMovendoVerticalmente = Mathf.Abs(movimentoVertical) > 0.1f;

            // Verifica se o jogador est� pressionando o direcional esquerdo ou direito.
            bool estaMovendoHorizontalmente = Mathf.Abs(movimentoHorizontal) > 0.1f;

            // Move o jogador para cima ou para baixo com base na entrada vertical.
            Vector3 movimento = new Vector3(movimentoHorizontal * velocidadeEscalada * Time.deltaTime, movimentoVertical * velocidadeEscalada * Time.deltaTime, 0);

            if (estaMovendoVerticalmente)
            {
                AtualizarAnimacao(movimentoVertical);
            }
            else
            {
                // Se o jogador n�o est� pressionando o direcional vertical, mant�m a �ltima sprite.
                if (spriteUltima != null)
                {
                    spriteRenderer.sprite = spriteUltima;
                }
            }

            if (estaMovendoHorizontalmente)
            {
                // Aqui voc� pode adicionar a l�gica para mover horizontalmente se desejar.
            }

            transform.Translate(movimento);
        }
    }

    private void AtualizarAnimacao(float movimentoVertical)
    {
        // Seleciona o array de sprites apropriado com base na dire��o do movimento
        Sprite[] sprites = movimentoVertical > 0 ? spritesSubida : spritesDescida;

        if (sprites.Length > 0)
        {
            // Verifica o tempo decorrido desde a �ltima mudan�a de sprite
            tempoUltimaMudanca += Time.deltaTime;

            if (tempoUltimaMudanca >= velocidadeAnimacao)
            {
                // Atualiza a imagem atual e exibe-a
                spriteUltima = sprites[spriteAtual];
                spriteRenderer.sprite = spriteUltima;

                // Avan�a para a pr�xima imagem no array
                spriteAtual = (spriteAtual + 1) % sprites.Length;

                tempoUltimaMudanca = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (contatosObjetosEscalaveis.Contains(other))
        {
            estaEscalando = true;
            rb2d.gravityScale = 0f;

            if (chaoCollider != null)
            {
                // Ativa o trigger da Collider do Chao_Collider
                chaoCollider.isTrigger = true;
            }

            // Desativa o script "Correr" ao entrar na escada
            if (scriptCorrer != null)
            {
                scriptCorrer.enabled = false;
            }

            // Desativa o script "Atualizar_Colisor" ao entrar na escada
            if (scriptAtualizarColisor != null)
            {
                scriptAtualizarColisor.enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (contatosObjetosEscalaveis.Contains(other))
        {
            estaEscalando = false;

            if (chaoCollider != null)
            {
                // Desativa o trigger da Collider do Chao_Collider
                chaoCollider.isTrigger = false;
            }

            // Ativa o script "Correr" ao sair da escada
            if (scriptCorrer != null)
            {
                scriptCorrer.enabled = true;
            }

            // Ativa o script "Atualizar_Colisor" ao sair da escada
            if (scriptAtualizarColisor != null)
            {
                scriptAtualizarColisor.enabled = true;
            }

            if (spriteUltima != null)
            {
                spriteRenderer.sprite = spriteUltima;
            }

            rb2d.gravityScale = 1f;
        }
    }
}
