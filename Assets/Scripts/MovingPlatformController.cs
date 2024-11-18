using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 1.1f; // pr�dko�� ruchu platformy
    private float startPositionX; // pocz�tkowa pozycja pozioma
    public float moveRange = 1.0f; // zasi�g ruchu
    private Rigidbody2D rigidBody;

    private bool isMovingRight = true; // zmienna do okre�lania kierunku ruchu

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPositionX = this.transform.position.x; // przypisanie pocz�tkowej pozycji
    }

    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    void MoveLeft()
    {
        transform.Translate(-(moveSpeed * Time.deltaTime), 0.0f, 0.0f, Space.World);
    }

    private void Flip()
    {
        isMovingRight = !isMovingRight; // zmiana kierunku
    }

    private void Update()
    {
        // Logika ruchu
        if (isMovingRight)
        {
            if (this.transform.position.x < startPositionX + moveRange)
            {
                MoveRight(); // Poruszaj si� w prawo
            }
            else
            {
                Flip(); // Obr��
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - moveRange)
            {
                MoveLeft(); // Poruszaj si� w lewo
            }
            else
            {
                Flip(); // Obr��
            }
        }
    }
}