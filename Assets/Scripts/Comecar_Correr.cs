using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correr : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] spritesDeAnimacao;
    public Sprite spritePulo; // Adicione a sprite de pulo no Inspector

    public float tempoPorQuadro = 0.1f;
    private int quadroAtual = 0;
    private float tempoDecorrido = 0f;
    private Rigidbody2D rb2d;
    public float velocidade = 5f;
    public float forcaDoPulo = 5.5f; // Força do pulo

    // Limites de movimento no eixo X
    public float limiteEsquerdo = -19f;
    public float limiteDireito = 119f;

    private bool estaPulando = false;
    private float tempoPulando = 0f;
    private bool estaNoChao = true; // Adicione esta variável

    [SerializeField]
    private AudioClip JumpSound;
    private AudioSource audioSource;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        bool pulo = Input.GetKeyDown(KeyCode.Space);

        if (movimentoHorizontal > 0 && transform.position.x < limiteDireito)
        {
            spriteRenderer.flipX = false;
            MoverDireita();
        }
        else if (movimentoHorizontal < 0 && transform.position.x > limiteEsquerdo)
        {
            spriteRenderer.flipX = true;
            MoverEsquerda();
        }
        else
        {
            PararMovimento();
        }

        if (pulo && estaNoChao) // Verifique se está no chão antes de pular
        {
            Pular();
        }

        if (estaPulando)
        {
            tempoPulando += Time.deltaTime;

            // Mantém a sprite de pulo por 0.7 segundos
            if (tempoPulando <= 0.7f)
            {
                spriteRenderer.sprite = spritesDeAnimacao[2]; // Defina o índice correto para a segunda sprite
            }
        }
    }

    // Adicione este método para detectar quando o personagem toca no chão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = true;
        }
    }

    // Adicione este método para detectar quando o personagem deixa o chão
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            estaNoChao = false;
        }
    }

    void MoverDireita()
    {
        transform.rotation = Quaternion.identity;

        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= tempoPorQuadro)
        {
            quadroAtual = (quadroAtual + 1) % spritesDeAnimacao.Length;
            spriteRenderer.sprite = spritesDeAnimacao[quadroAtual];
            tempoDecorrido = 0f;
        }

        rb2d.velocity = new Vector2(velocidade, rb2d.velocity.y);
    }

    void MoverEsquerda()
    {
        transform.rotation = Quaternion.identity;

        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= tempoPorQuadro)
        {
            quadroAtual = (quadroAtual + 1) % spritesDeAnimacao.Length;
            spriteRenderer.sprite = spritesDeAnimacao[quadroAtual];
            tempoDecorrido = 0f;
        }

        rb2d.velocity = new Vector2(-velocidade, rb2d.velocity.y);
    }

    void PararMovimento()
    {
        tempoDecorrido = 0f;
        transform.rotation = Quaternion.identity;

        if (spriteRenderer.flipX)
        {
            quadroAtual = 5;
        }
        else
        {
            quadroAtual = 5;
        }
        spriteRenderer.sprite = spritesDeAnimacao[quadroAtual];

        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }

    void Pular()
    {
        // Aplica a força do pulo ao Rigidbody2D
        rb2d.velocity = new Vector2(rb2d.velocity.x, forcaDoPulo);
        spriteRenderer.sprite = spritePulo; // Define a sprite de pulo
        estaPulando = true;
        tempoPulando = 0f;
        estaNoChao = false; // Quando você pular, não estará mais no chão.
        ReproduzirSom(JumpSound);
    }
    private void ReproduzirSom(AudioClip som)
    {
        if (audioSource != null && som != null)
        {
            audioSource.PlayOneShot(som);
        }
    }
}
