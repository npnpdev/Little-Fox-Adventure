using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Obiekt, za kt�rym kamera ma pod��a� (np. posta�)
    public Vector3 offset;     // Przesuni�cie kamery wzgl�dem postaci
    public float smoothSpeed = 0.125f; // Szybko�� p�ynnego pod��ania kamery
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Wyznaczamy pozycj�, do kt�rej kamera powinna zmierza�
        Vector3 desiredPosition = target.position + offset;

        // P�ynne przej�cie do nowej pozycji kamery
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Przypisujemy obliczon� pozycj� kamerze
        transform.position = smoothedPosition;

    }
}
