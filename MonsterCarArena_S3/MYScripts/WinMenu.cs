using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour {

    public Button MainMenuButton;
    public AudioClip Success;
    public AudioSource source;
    private float vol = 1f;
    private float timer = 0;
    private bool once = false;

    void Start ()
    {
        Screen.lockCursor = false;
        source.PlayOneShot(Success, vol);
        MainMenuButton.onClick.AddListener(ExitingtoMainMenu);
    }

	void FixedUpdate ()
    {
        if(once == false)
        {
            if (Success.length >= timer && once == false)
            {
                timer += 1.0f * Time.deltaTime;
            }
            else
            {
                once = true;
                source.Play();
            }
        }
	}

    void ExitingtoMainMenu()
    {
        source.Stop();
        SceneManager.LoadScene("MainMenu");
    }
}
