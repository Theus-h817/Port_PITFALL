using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumMovement : MonoBehaviour
{
    public float length = 3.0f; // Comprimento do cipó.
    public float amplitude = 45.0f; // Amplitude do ângulo.
    public float speed = 1.0f; // Velocidade da oscilação.

    private Vector3 pivot;
    private float time;

    private void Start()
    {
        pivot = transform.position;
    }

    private void Update()
    {
        time += Time.deltaTime;

        float angle = amplitude * Mathf.Sin(time * speed);
        float x = pivot.x + Mathf.Sin(angle * Mathf.Deg2Rad) * length;
        float y = pivot.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
