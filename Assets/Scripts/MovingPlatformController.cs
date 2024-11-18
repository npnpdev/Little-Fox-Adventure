using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 1.1f; // prêdkoœæ ruchu platformy
    private float startPositionX; // pocz¹tkowa pozycja pozioma
    public float moveRange = 1.0f; // zasiêg ruchu
    private Rigidbody2D rigidBody;

    private bool isMovingRight = true; // zmienna do okreœlania kierunku ruchu

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPositionX = this.transform.position.x; // przypisanie pocz¹tkowej pozycji
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
                MoveRight(); // Poruszaj siê w prawo
            }
            else
            {
                Flip(); // Obróæ
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - moveRange)
            {
                MoveLeft(); // Poruszaj siê w lewo
            }
            else
            {
                Flip(); // Obróæ
            }
        }
    }
}