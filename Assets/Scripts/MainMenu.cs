using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// W celu testowania w edytorze unity
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Definicja metody StartGame z opóŸnieniem
    private IEnumerator StartGame(string levelName)
    {
        // OpóŸnienie przed wczytaniem sceny
        yield return new WaitForSeconds(0.1f);

        // Wczytanie nowej sceny
        SceneManager.LoadScene(levelName);
    }

    public void OnLevel1ButtonPressed()
    {
        StartCoroutine(StartGame("Level 1"));

    }

    public void OnExitToDesktopButtonPressed()
    {
        Application.Quit();
    }
}
