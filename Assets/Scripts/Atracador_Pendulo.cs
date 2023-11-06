using UnityEngine;

public class SeguirPendulo : MonoBehaviour
{
    public Transform pendulo; // Arraste o GameObject do pêndulo aqui no Inspector.

    private void Update()
    {
        if (pendulo != null) // Certifique-se de que o pêndulo está definido.
        {
            // Obtém a posição do pêndulo e define a posição do objeto para coincidir.
            transform.position = pendulo.position;
        }
    }
}
