using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameState
{
    [InspectorName("Gameplay")] GAME,
    [InspectorName("Pause")] PAUSE_MENU,
    [InspectorName("Level completed")] LEVEL_COMPLETED,
    [InspectorName("Game Over!")] GAME_OVER
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentGameState = GameState.PAUSE_MENU;

    public Canvas inGameCanvas;
    public Canvas pauseMenuCanvas;
    public Canvas levelCompletedCanvas;

    private PlayerController playerController; // Odniesienie do skryptu gracza

    // Tablica dla kluczy
    public Image[] keysTab;

    private int keysFound = 0;

    // Ilosc zabitych przeciwników
    private int enemiesKilled = 0;
    public TMP_Text killedEnemiesText;

    // Tablica dla ¿yæ
    public Image[] heartsTab;

    private int heartsNumber = 3;

    // Zliczanie czasu gry
    public TMP_Text timeText;
    private float timer = 0.0f;
    private bool isGameRunning = false; // Flaga okreœlaj¹ca, czy gra trwa - moment, od którego zaczynamy liczyæ czas

    // Dodane zmienne do zarz¹dzania punktami
    public TMP_Text scoreText; // Publiczna zmienna typu TMP_Text do wyœwietlania punktów
    private int score = 0;     // Prywatna zmienna przechowuj¹ca aktualny wynik

    private const string keyHighScore = "HighScoreLevel1";
    public TMP_Text highScoreText;

    private void Awake()
    {
        // Pocz¹tkowy stan gry
        InGame();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Duplicated Game Manager", gameObject);
            Destroy(gameObject);
        }

        for (int i = 0; i < keysTab.Length; i++)
        {
            keysTab[i].color = Color.gray;

        }

        // Pomijamy serce numer '0', poniewaz jest to serce dodatkowe
        for (int i = 1; i < heartsTab.Length; i++)
        {
            heartsTab[i].enabled = true;

        }
        // Serce dodatkowe
        heartsTab[0].enabled = false;

        if (PlayerPrefs.HasKey(keyHighScore))
        {
            PlayerPrefs.SetInt(keyHighScore, 0);
        }

        // Ustawienie wyœwietlania pocz¹tkowej liczby punktów
        UpdateScoreDisplay();
        UpdateKilledEnemiesDisplay();
    }

    private void Start()
    {
        // Zak³adamy, ¿e skrypt gracza jest przypisany do tego samego obiektu co tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
            UpdatePlayerControllerState();
        }

        // Ustawienie Canvas na podstawie pocz¹tkowego stanu gry
        if (inGameCanvas != null)
        {
            inGameCanvas.enabled = (currentGameState == GameState.GAME);
        }
    }

    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        UpdatePlayerControllerState();

        // W³¹czanie/wy³¹czanie Canvas
        if (inGameCanvas != null)
        {
            inGameCanvas.enabled = (currentGameState == GameState.GAME);
        }

        if(pauseMenuCanvas != null)
        {
            pauseMenuCanvas.enabled = (currentGameState == GameState.PAUSE_MENU);
        }

        if(levelCompletedCanvas != null)
        {
            levelCompletedCanvas.enabled = (currentGameState == GameState.LEVEL_COMPLETED);
        }

        if(newGameState == GameState.LEVEL_COMPLETED)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Level 1")
            {
                int highScore = PlayerPrefs.GetInt(keyHighScore);

                if(highScore < score)
                {
                    highScore = score;
                    PlayerPrefs.SetInt(keyHighScore, highScore);
                }

                // Przypisz aktualny wynik do etykiety
                scoreText.text = "Score: " + score;

                // Przypisz najlepszy wynik do etykiety
                highScoreText.text = "High Score: " + highScore;
            }
        }
    }

    public void PauseMenu()
    {
        SetGameState(GameState.PAUSE_MENU);
        isGameRunning = false;
    }

    public void InGame()
    {
        SetGameState(GameState.GAME);
        isGameRunning = true;
    }

    public void LevelCompleted()
    {
        SetGameState(GameState.LEVEL_COMPLETED);
        isGameRunning = false;
    }

    public void GameOver()
    {
        SetGameState(GameState.GAME_OVER);
        isGameRunning = false;
    }

    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnReturnToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.GAME)
            {
                PauseMenu();
            }
            else if (currentGameState == GameState.PAUSE_MENU)
            {
                InGame();
            }
        }

        // Czas na ekranie
        if (isGameRunning) {
            timer += Time.deltaTime;
            UpdateTimeDisplay();
        }
    }

    // Metoda w³¹cza/wy³¹cza skrypt gracza w zale¿noœci od stanu gry
    private void UpdatePlayerControllerState()
    {
        if (playerController != null)
        {
            playerController.enabled = (currentGameState == GameState.GAME);
        }
    }


    public void increaseKilledEnemies(){
        enemiesKilled++;
        UpdateKilledEnemiesDisplay();
    }

    // Metoda zwiêkszaj¹ca punkty i aktualizuj¹ca wyœwietlany wynik
    public void AddPoints(int points)
    {
        score += points; // Dodanie punktów
        UpdateScoreDisplay(); // Zaktualizowanie wyœwietlanego wyniku
    }
    public void updateHeartsDisplay()
    {
        if (heartsTab.Length == 0)
        {
            Debug.LogWarning("Tablica serc jest pusta!");
            return;
        }

        // Pêtla od koñca tablicy do pocz¹tku
        for (int heartIndex = heartsTab.Length - 1; heartIndex >= 0; heartIndex--)
        {
            if (heartIndex >= heartsTab.Length - heartsNumber)
            {
                heartsTab[heartIndex].enabled = true;
            }
            else
            {
                heartsTab[heartIndex].enabled = false; 
            }
        }

    }

    // Aktualizacja wyœwietlania wyniku
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString("D3");
        }
    }
    private void UpdateKilledEnemiesDisplay()
    {
        if (killedEnemiesText != null)
        {
            // Format liczby jako dwucyfrowej
            killedEnemiesText.text = enemiesKilled.ToString("D2");
        }
    }

    private void UpdateTimeDisplay()
    {
        if (timeText != null)
        {
            // Obliczanie minut, sekund i milisekund
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

            // Formatowanie czasu
            timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }

    // Publiczna metoda zmieniaj¹ca kolor klucza i zwiêkszaj¹ca liczbê znalezionych kluczy
    public void AddKeys()
    {
        // Szukamy "pierwszego" niezdobytego klucza
        for (int keyIndex = 0; keyIndex < keysTab.Length; keyIndex++)
        {
            if (keysTab[keyIndex].color == Color.gray)
            {
                keysTab[keyIndex].color = Color.white;
                keysFound++;
                break;
            }
        }
    }
    
    public int getKeysNumber()
    {
        return keysTab.Length;
    }

    public int getKeysFounded()
    {
        return keysFound;
    }

    public int getPlayerLives()
    {
        return heartsNumber;
    }

    public void setPlayerLives(int livesNumber)
    {
        heartsNumber = livesNumber;
    }

    public void setPlayerScore(int newScore)
    {
        score = newScore;
    }

    public int getPlayerScore()
    {
        return score;
    }
}
