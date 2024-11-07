using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private PlayerController playerController; // Odniesienie do skryptu gracza

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Duplicated Game Manager", gameObject);
            Destroy(gameObject);
        }
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
    }

    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        UpdatePlayerControllerState(); // Aktualizacja stanu skryptu gracza
    }

    public void PauseMenu()
    {
        SetGameState(GameState.PAUSE_MENU);
    }

    public void InGame()
    {
        SetGameState(GameState.GAME);
    }

    public void LevelCompleted()
    {
        SetGameState(GameState.LEVEL_COMPLETED);
    }

    public void GameOver()
    {
        SetGameState(GameState.GAME_OVER);
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
    }

    // Metoda w³¹cza/wy³¹cza skrypt gracza w zale¿noœci od stanu gry
    private void UpdatePlayerControllerState()
    {
        if (playerController != null)
        {
            playerController.enabled = (currentGameState == GameState.GAME);
        }
    }
}
