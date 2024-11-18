using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Obiekt, za którym kamera ma pod¹¿aæ (np. postaæ)
    public Vector3 offset;     // Przesuniêcie kamery wzglêdem postaci
    public float smoothSpeed = 0.125f; // Szybkoœæ p³ynnego pod¹¿ania kamery
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Wyznaczamy pozycjê, do której kamera powinna zmierzaæ
        Vector3 desiredPosition = target.position + offset;

        // P³ynne przejœcie do nowej pozycji kamery
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Przypisujemy obliczon¹ pozycjê kamerze
        transform.position = smoothedPosition;

    }
}
