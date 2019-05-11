using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button HowToPlay;
    public Button Back;
    public Button Play;
    public Button Exit;
    public Canvas mainMenu;
    public Canvas InstructionsMenu;
    private bool switchHit = false;
    public AudioSource source;

    void Start()
    {
        Screen.lockCursor = false;
        HowToPlay.onClick.AddListener(HowToPlayGame);
        Play.onClick.AddListener(StartGame);
        Exit.onClick.AddListener(QuitGame);
        Back.onClick.AddListener(HowToPlayGame);
        InstructionsMenu.gameObject.SetActive(false);
        source.Play();
    }

    private void HowToPlayGame()
    {
        if (switchHit == false)
        {
            InstructionsMenu.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(false);
            switchHit = true;
        }
        else
        {
            mainMenu.gameObject.SetActive(true);
            InstructionsMenu.gameObject.SetActive(false);
            switchHit = false;
        }
    }

    private void QuitGame()
    {
        source.Stop();
        Debug.Log("Quitting");
        Application.Quit();
    }

    private void StartGame()
    {
        source.Stop();
        SceneManager.LoadScene("MainGame");
    }
}