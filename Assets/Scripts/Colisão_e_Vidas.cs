using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidasJogador : MonoBehaviour
{
    public int vidasIniciais = 3;
    private int vidasRestantes;

    private Vector3 posicaoInicial; // Armazena a posi��o inicial do jogador.

    [SerializeField]
    private GameObject[] mobs; // Array para adicionar os mobs no Inspector.

    [SerializeField]
    private Transform pontoInicial; // Transform para adicionar o ponto inicial no Inspector.

    [SerializeField]
    private AudioClip perdaVidaSound; // Som de perda de vida.

    [SerializeField]
    private AudioClip gameOverSound; // Som de game over.

    private AudioSource audioSource;

    private void Start()
    {
        vidasRestantes = vidasIniciais;
        posicaoInicial = pontoInicial.position; // Usando o Transform para obter a posi��o inicial.
        audioSource = GetComponent<AudioSource>(); // Obt�m o componente de �udio.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsMob(other.gameObject))
        {
            PerdeVida();

            // Adicione o deslocamento lateral aqui.
            float direction = (transform.position.x < other.transform.position.x) ? -1f : 1f; // Esquerda ou direita
            transform.Translate(Vector3.right * direction * 2.5f); // Desloca 2.5 unidades na dire��o apropriada.

            // Toque o som de perda de vida.
            ReproduzirSom(perdaVidaSound);
        }
    }

    private bool IsMob(GameObject obj)
    {
        // Verifica se o GameObject passado � um dos mobs adicionados no Inspector.
        return System.Array.Exists(mobs, element => element == obj);
    }

    private void PerdeVida()
    {
        vidasRestantes--;

        if (vidasRestantes <= 0)
        {
            // O jogador perdeu todas as vidas, ent�o coloque-o de volta na posi��o inicial.
            transform.position = posicaoInicial;
            vidasRestantes = vidasIniciais;

            // Toque o som de game over.
            ReproduzirSom(gameOverSound);
        }
    }

    private void ReproduzirSom(AudioClip som)
    {
        if (audioSource != null && som != null)
        {
            audioSource.PlayOneShot(som);
        }
    }
}
