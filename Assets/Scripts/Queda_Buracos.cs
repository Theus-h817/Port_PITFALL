using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInteraction : MonoBehaviour
{
    public Collider2D groundCollider; // Referência para o Collider2D do chão que você deseja ativar.

    private bool playerIsOverHole = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador colidiu com o buraco.
        {
            playerIsOverHole = true;
            ActivateTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador saiu do buraco.
        {
            playerIsOverHole = false;
            DeactivateTrigger(); // Desativa o Trigger quando o jogador sai do buraco.
        }
    }

    private void ActivateTrigger()
    {
        if (playerIsOverHole && groundCollider != null)
        {
            groundCollider.isTrigger = true; // Ativa o Trigger do Collider2D do chão.
        }
    }

    private void DeactivateTrigger()
    {
        if (!playerIsOverHole && groundCollider != null)
        {
            groundCollider.isTrigger = false; // Desativa o Trigger do Collider2D do chão quando o jogador sai do buraco.
        }
    }
}