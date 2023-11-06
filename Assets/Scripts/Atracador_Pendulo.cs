using UnityEngine;

public class SeguirPendulo : MonoBehaviour
{
    public Transform pendulo; // Arraste o GameObject do p�ndulo aqui no Inspector.

    private void Update()
    {
        if (pendulo != null) // Certifique-se de que o p�ndulo est� definido.
        {
            // Obt�m a posi��o do p�ndulo e define a posi��o do objeto para coincidir.
            transform.position = pendulo.position;
        }
    }
}
