using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 1.1f; // pr�dko�� ruchu przeciwnika
    private bool isFacingRight = false; // zmienna do sprawdzania kierunku
    private float startPositionX; // pocz�tkowa pozycja pozioma przeciwnika
    public float moveRange = 1.0f; // zasi�g ruchu przeciwnika
    private Rigidbody2D rigidBody;
    private bool isDead = false; // Zmienna do �ledzenia, czy przeciwnik jest martwy

    private bool isMovingRight = true; // zmienna do okre�lania kierunku ruchu przeciwnika
    private Animator animator; // Animator przeciwnika

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Pobranie komponentu Animator
        startPositionX = this.transform.position.x; // przypisanie pocz�tkowej pozycji
    }

    void MoveRight() {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        if (!isFacingRight)
        {
            Flip(); // odwr�cenie przeciwnika, je�li jest zwr�cony w lewo
        }
    }

    void MoveLeft() {
        transform.Translate(-(moveSpeed * Time.deltaTime), 0.0f, 0.0f, Space.World);
        if (isFacingRight)
        {
            Flip(); // odwr�cenie przeciwnika, je�li jest zwr�cony w prawo
        }
    }

    private void Flip() {
        isFacingRight = !isFacingRight; // zmiana kierunku
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // odwr�cenie skali w osi X
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
                MoveRight(); // Poruszaj si� w prawo
            }
            else
            {
                isMovingRight = false; // Zmie� kierunek na lewy
                Flip(); // Obr�� przeciwnika
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
                isMovingRight = true; // Zmie� kierunek na prawy
                Flip(); // Obr�� przeciwnika
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Sprawdzenie kolizji z graczem
        if (other.CompareTag("Player"))
        {
            // Je�li po�o�enie gracza jest powy�ej przeciwnika
            if (other.transform.position.y > this.transform.position.y)
            {
                isDead = true;
                animator.SetBool("isDead", true); // Ustawienie animacji �mierci na true
                StartCoroutine(KillOnAnimationEnd()); // Wywo�anie metody do animacji �mierci

                // Odbicie gracza w g�r�
                Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    // Ustaw odpowiedni� pr�dko�� w g�r�
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 6.5f); // Mo�esz dostosowa� warto��
                }
            }
        }
    }
    private IEnumerator KillOnAnimationEnd() {
        // Czekaj na zako�czenie animacji �mierci
        yield return new WaitForSeconds(0.5f); // Mo�esz dostosowa� czas w zale�no�ci od d�ugo�ci animacji

        // Dezaktywuj obiekt przeciwnika po zako�czeniu animacji
        gameObject.SetActive(false);
        Debug.Log("Enemy has been killed");
    }
}