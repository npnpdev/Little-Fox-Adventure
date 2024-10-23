using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 1.1f; // prêdkoœæ ruchu przeciwnika
    private bool isFacingRight = false; // zmienna do sprawdzania kierunku
    private float startPositionX; // pocz¹tkowa pozycja pozioma przeciwnika
    public float moveRange = 1.0f; // zasiêg ruchu przeciwnika
    private Rigidbody2D rigidBody;
    private bool isDead = false; // Zmienna do œledzenia, czy przeciwnik jest martwy

    private bool isMovingRight = true; // zmienna do okreœlania kierunku ruchu przeciwnika
    private Animator animator; // Animator przeciwnika

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Pobranie komponentu Animator
        startPositionX = this.transform.position.x; // przypisanie pocz¹tkowej pozycji
    }

    void MoveRight() {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        if (!isFacingRight)
        {
            Flip(); // odwrócenie przeciwnika, jeœli jest zwrócony w lewo
        }
    }

    void MoveLeft() {
        transform.Translate(-(moveSpeed * Time.deltaTime), 0.0f, 0.0f, Space.World);
        if (isFacingRight)
        {
            Flip(); // odwrócenie przeciwnika, jeœli jest zwrócony w prawo
        }
    }

    private void Flip() {
        isFacingRight = !isFacingRight; // zmiana kierunku
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // odwrócenie skali w osi X
        transform.localScale = theScale; // przypisanie zmienionej skali
    }

    private void Update() {
        if (isDead)
        {
            return;
        }

        // Logika ruchu przeciwnika
        if (isMovingRight)
        {
            if (this.transform.position.x < startPositionX + moveRange)
            {
                MoveRight(); // Poruszaj siê w prawo
            }
            else
            {
                isMovingRight = false; // Zmieñ kierunek na lewy
                Flip(); // Obróæ przeciwnika
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
                isMovingRight = true; // Zmieñ kierunek na prawy
                Flip(); // Obróæ przeciwnika
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Sprawdzenie kolizji z graczem
        if (other.CompareTag("Player"))
        {
            // Jeœli po³o¿enie gracza jest powy¿ej przeciwnika
            if (other.transform.position.y > this.transform.position.y)
            {
                isDead = true;
                animator.SetBool("isDead", true); // Ustawienie animacji œmierci na true
                StartCoroutine(KillOnAnimationEnd()); // Wywo³anie metody do animacji œmierci

                // Odbicie gracza w górê
                Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    // Ustaw odpowiedni¹ prêdkoœæ w górê
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 6.5f); // Mo¿esz dostosowaæ wartoœæ
                }
            }
        }
    }
    private IEnumerator KillOnAnimationEnd() {
        // Czekaj na zakoñczenie animacji œmierci
        yield return new WaitForSeconds(0.5f); // Mo¿esz dostosowaæ czas w zale¿noœci od d³ugoœci animacji

        // Dezaktywuj obiekt przeciwnika po zakoñczeniu animacji
        gameObject.SetActive(false);
        Debug.Log("Enemy has been killed");
    }
}