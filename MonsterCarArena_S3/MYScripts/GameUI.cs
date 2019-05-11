using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    public Button Back;
    public Button mainMenu;
    public Canvas gameUI;
    public Canvas gameMenu;
    public Text Health;
    public Text Turrets;
    public Text TimeSec;
    private int HitPoints = 100;
    private int TurretCounter = 10;
    private float TimeLeft = 300f;
    public bool Quit = false;
    public AudioSource source;

    void Start ()
    {
        Screen.lockCursor = true;
        Health.text = "Health: " + HitPoints;
        Turrets.text = "Turrets: " + TurretCounter;
        TimeSec.text = "Time: " + TimeLeft;
        Back.onClick.AddListener(BackButtonmClick);
        mainMenu.onClick.AddListener(mainMenuScene);
        gameMenu.gameObject.SetActive(false);
        source.Play();
    }
	
	void FixedUpdate ()
    {
        if(Quit != true)
        {
            UpdateTime();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Quit != true)
            {
                Quit = true;
                QuitMenu();
            }
        }
    }

    public void UpdateHealth(int dam)
    {
        HitPoints -= dam;
        Health.text = "Health: " + HitPoints;
        checkDeath();
    }

    void checkDeath()
    {
        if (HitPoints <= 0)
        {
            Health.text = "Health: " + 0;
            Debug.Log("Game Over");
            source.Stop();
            SceneManager.LoadScene("GameOver");
        }
    }

    public void UpdateTurret()
    {
        TurretCounter -= 1;
        Turrets.text = "Turrets: " + TurretCounter;
        checkTurret();
    }

    void checkTurret()
    {
        if (TurretCounter <= 0)
        {
            Turrets.text = "Turrets: " + 0;
            Debug.Log("You Win");
            source.Stop();
            SceneManager.LoadScene("WinScreen");
        }
    }

    private void UpdateTime()
    {
        if(TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            TimeSec.text = "Time: " + TimeLeft.ToString("#.00");
        }
        else
        {
            TimeSec.text = "Time: " + 0;
            Debug.Log("You Lose");
            source.Stop();
            SceneManager.LoadScene("GameOver");
        }
    }

    private void QuitMenu()
    {
        if(Quit == true)
        {
            Screen.lockCursor = false;
            Time.timeScale = 0;
            gameMenu.gameObject.SetActive(true);
            gameUI.gameObject.SetActive(false);
        }
        else
        {
            Screen.lockCursor = true;
            Time.timeScale = 1;
            gameUI.gameObject.SetActive(true);
            gameMenu.gameObject.SetActive(false);
        }
    }

    private void BackButtonmClick()
    {
        Quit = false;
        QuitMenu();
    }

    private void mainMenuScene()
    {
        Time.timeScale = 1;
        source.Stop();
        SceneManager.LoadScene("MainMenu");
    }
}
