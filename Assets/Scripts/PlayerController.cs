using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms.Impl;

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

    private int additionalHeartsFounded = 0;
    private const int additionalHearts = 3;
    Vector2 startPosition;
    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;
    const float rayLength = 1.0f;
    const float rayWidth = 1.0f;

    // Referencja do GameManager
    private GameManager gameManager;

    // DŸwiêki
    [Range(0.01f, 20.0f)][SerializeField] float soundsLevel = 2.0f;
    [SerializeField] AudioClip bSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip bonusSound;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip enemyKilledSound;
    private AudioSource source;

    //[Space(10)]

    bool IsGrounded() {
        LayerMask groundLayer = LayerMask.GetMask("Ground");

        // Wymiary "promienia" (BoxCast), gdzie szerokoœæ to szerokoœæ postaci
        Vector2 boxSize = new Vector2(rayWidth, rayLength);

        // BoxCast wykrywaj¹cy ziemiê
        return Physics2D.BoxCast(this.transform.position, boxSize, 0f, Vector2.down, rayLength, groundLayer);
    }

    void Jump() {
        source.PlayOneShot(jumpSound, soundsLevel);
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
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
        // Znalezienie GameManager na scenie (jeœli nie korzystasz z Singletona)
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager nie zosta³ znaleziony!");
        }
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
        gameManager.setPlayerLives(gameManager.getPlayerLives()-1);
        gameManager.updateHeartsDisplay();

        if (gameManager.getPlayerLives() <= 0)
        {
            source.PlayOneShot(gameOverSound, soundsLevel);
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
            // Jeœli po³o¿enie gracza jest powy¿ej przeciwnika, uwzglêdniaj¹c wysokoœæ obu obiektów - dlatego dodajemy wysokosc gracza
            if (this.transform.position.y > other.transform.position.y + this.transform.localScale.y / 2)
            {
                source.PlayOneShot(enemyKilledSound, soundsLevel);
                Debug.Log("player: " + transform.position.y + " enemy: " + other.transform.position.y);
                gameManager.increaseKilledEnemies();
            }
            else
            {
                // Odtwórz dŸwiêk
                source.PlayOneShot(deathSound, soundsLevel);

                // Przeciwnik zabi³ gracza
                gameManager.setPlayerLives(gameManager.getPlayerLives()-1);
                gameManager.updateHeartsDisplay();

                if (gameManager.getPlayerLives() <= 0)
                {
                    source.PlayOneShot(gameOverSound, soundsLevel);
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
            // Odtwórz dŸwiêk
            source.PlayOneShot(bSound, soundsLevel);

            gameManager.AddKeys();
            other.gameObject.SetActive(false); // Dezaktywuj obiekt klucza
        }
        else if (other.CompareTag("Heart"))
        {
            additionalHeartsFounded++;
            other.gameObject.SetActive(false); // Dezaktywuj obiekt klucza

            if (additionalHeartsFounded == additionalHearts)
            {
                Debug.Log("Znaleziono wszystkie dodatkowe serca! Otrzymujesz dodatkowe zycie!");
                //
                int numberOfLives = gameManager.getPlayerLives();
                numberOfLives++;
                Debug.Log(numberOfLives);
                gameManager.setPlayerLives(numberOfLives);
                gameManager.updateHeartsDisplay();
            }
            else
            {
                Debug.Log("Znaleziono dodatkowe serce! Zbierz jeszcze " + (additionalHearts - additionalHeartsFounded) + " i zdobadz jedno dodatkowe zycie!");   
            }
        }
        else if (other.CompareTag("endPoint"))
        {
            if(gameManager.getKeysFounded() == gameManager.getKeysNumber())
            {
                source.PlayOneShot(gameOverSound, soundsLevel);


                gameManager.setPlayerScore(gameManager.getPlayerScore() + (100 * gameManager.getPlayerLives()));
                gameManager.LevelCompleted();
            }
            else
            {
                Debug.Log("Nie zebrales wszystkich kluczy! Pozostalo do zebrania: " + (gameManager.getKeysNumber() - gameManager.getKeysFounded()));
            }
        }
        else if (other.CompareTag("fallLevel"))
        {
            Death();
        }
        else if (other.CompareTag("Cherry"))
        {
            source.PlayOneShot(bonusSound, soundsLevel);
            gameManager.AddPoints(100);
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
        else if (other.CompareTag("movingPlatform") || other.CompareTag("generatedPlatform")){
            transform.SetParent(other.gameObject.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false; // Stop climbing when leaving the ladder
            rigidBody.gravityScale = 1; // Restore gravity
        }
        else if (other.CompareTag("movingPlatform") || other.CompareTag("generatedPlatform"))
        {
            transform.SetParent(null);
        }
    }
}
