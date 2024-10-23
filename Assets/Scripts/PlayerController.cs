using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f; // moving speed of the player
    [Range(0.01f, 20.0f)][SerializeField] private float jumpForce = 6.0f; // jump speed of the player
    [SerializeField] private float jumpCooldown = 0.1f; // Czas, w którym gracz nie mo¿e ponownie skakaæ
    private float jumpTimer = 0f; // Licznik czasu
    private Animator animator;

    private bool isLadder = false;
    private bool isClimbing = false;
    private bool isWalking = false;
    private bool isJumping = false;
    private bool isFacingRight = true;
    private float vertical;

    private int lives = 3;
    private int score = 0;
    private int keysFound = 0;
    private int points = 0;
    private const int keysNumber = 3;
    private int additionalHeartsFounded = 0;
    private const int additionalHearts = 3;
    Vector2 startPosition;
    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;
    const float rayLength = 1.0f;
    const float rayWidth = 1.0f;
    //[Space(10)]

    bool IsGrounded() {
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        // Wymiary "promienia" (BoxCast), gdzie szerokoœæ to szerokoœæ postaci
        Vector2 boxSize = new Vector2(rayWidth, rayLength);

        // BoxCast wykrywaj¹cy ziemiê
        return Physics2D.BoxCast(this.transform.position, boxSize, 0f, Vector2.down, rayLength, groundLayer);
    }

    void Jump() {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Flip() {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        if (isFacingRight)
        {
            isFacingRight = false;
        }
        else
        {
            isFacingRight = true;
        }
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        isWalking = false;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            isWalking = true;

            if (!isFacingRight)
            {
                Flip();
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            transform.Translate(-(moveSpeed * Time.deltaTime), 0.0f, 0.0f, Space.World);
            isWalking = true;

            if (isFacingRight)
            {
                Flip();
            }
        }

        // Aktualizacja timera skoku
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }

        // Sprawdzanie skoku
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && jumpTimer <= 0)
        {
            isJumping = true;
            Jump();
            jumpTimer = jumpCooldown; // Ustaw czas "odnowienia" skoku
        }

        //Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.white, 1, false);
        /*Debug.DrawRay(this.transform.position, Vector2.down * rayLength, Color.red);
        Debug.DrawLine(new Vector2(this.transform.position.x - 0.5f, this.transform.position.y),
                       new Vector2(this.transform.position.x - 0.5f, this.transform.position.y - rayLength), Color.red);
        Debug.DrawLine(new Vector2(this.transform.position.x + 0.5f, this.transform.position.y),
                       new Vector2(this.transform.position.x + 0.5f, this.transform.position.y - rayLength), Color.red);*/

        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isClimbing", isClimbing);

        // Sprawdzenie, czy gracz jest w powietrzu
        if (!IsGrounded() && rigidBody.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
        }
        else if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }

        if (isLadder)
        {
            vertical = Input.GetAxis("Vertical");

            // Start climbing if vertical input is not zero and player is near ladder
            if (Mathf.Abs(vertical) > 0)
            {
                isClimbing = true;
            }
        }
    }
    void FixedUpdate() {
        if (isClimbing)
        {
            // Disable gravity while climbing
            rigidBody.gravityScale = 0;

            // Move the player vertically based on the input
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, vertical * moveSpeed);
        }
        else
        {
            // Restore gravity when not climbing
            rigidBody.gravityScale = 1;
        }
    }

    private void ResetPlayerPosition() {
        // Ustaw po³o¿enie gracza na pocz¹tkow¹ pozycjê
        transform.position = new Vector3(-8.5f, -13f, 0f); // Mo¿esz ustawiæ konkretn¹ pozycjê
        Debug.Log("Player position reset to the starting point");
    }

    private void Death() {
        lives--; // Zmniejsz liczbê ¿yæ
        Debug.Log("Player hit! Lives left: " + lives);

        if (lives <= 0)
        {
            Debug.Log("Game Over");
        }
        else
        {
            // Resetuj po³o¿enie gracza (mo¿esz ustawiæ konkretn¹ pozycjê)
            ResetPlayerPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Sprawdzenie kolizji z przeciwnikiem
        if (other.CompareTag("Enemy"))
        {
            // Jeœli po³o¿enie gracza jest powy¿ej przeciwnika
            if (transform.position.y > other.transform.position.y)
            {
                score += 1; // Zwiêksz liczbê zdobytych punktów
                Debug.Log("Killed an enemy");
            }
            else
            {
                lives--; // Zmniejsz liczbê ¿yæ
                Debug.Log("Player hit! Lives left: " + lives);

                if (lives <= 0)
                {
                    Debug.Log("Game Over");
                }
                else
                {
                    // Resetuj po³o¿enie gracza (mo¿esz ustawiæ konkretn¹ pozycjê)
                    ResetPlayerPosition();
                }
            }
        }
        else if (other.CompareTag("Key"))
        {
            keysFound++; // Zwiêksz liczbê znalezionych kluczy
            Debug.Log("Znaleziono klucz! Liczba kluczy: " + keysFound); // Wypisz komunikat
            other.gameObject.SetActive(false); // Dezaktywuj obiekt klucza
        }
        else if (other.CompareTag("Heart"))
        {
            additionalHeartsFounded++;
            other.gameObject.SetActive(false); // Dezaktywuj obiekt klucza

            if (additionalHeartsFounded == additionalHearts)
            {
                Debug.Log("Znaleziono wszystkie dodatkowe serca! Otrzymujesz dodatkowe zycie!");
                lives++;
            }
            else
            {
                Debug.Log("Znaleziono dodatkowe serce! Zbierz jeszcze " + (additionalHearts - additionalHeartsFounded) + " i zdobadz jedno dodatkowe zycie!" + keysFound);   
            }
        }
        else if (other.CompareTag("endPoint"))
        {
            if(keysFound == keysNumber)
            {
                Debug.Log("Game Over!");
            }
            else
            {
                Debug.Log("Nie zebrales wszystkich kluczy! Pozostalo do zebrania: " + (keysNumber - keysFound));
            }
        }
        else if (other.CompareTag("fallLevel"))
        {
            Death();
        }
        else if (other.CompareTag("Cherry"))
        {
            points += 100;
            Debug.Log("Dodatkowe 100 punktow! Liczba punktow: " + points);
            other.gameObject.SetActive(false); // Dezaktywuj obiekt 
        }
        else if (other.CompareTag("Ladder"))
        {
            isLadder = true;
        }
        else if (other.CompareTag("Fire"))
        {
            Death();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false; // Stop climbing when leaving the ladder
            rigidBody.gravityScale = 1; // Restore gravity
        }
    }
}
