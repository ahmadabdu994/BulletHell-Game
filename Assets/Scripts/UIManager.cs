using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public int Score;
    public static float FinalTime;
    public int HightScore;
    public int Playerhealth;
    private int BMeter = 0;
    public int BSpawnN;
    public GameObject Boss;

    public float Timer;

    public TextMeshProUGUI PlayerHealthText;
    public TextMeshProUGUI PlayerScore;
    public TextMeshProUGUI PlayerHightScore;
    public TextMeshProUGUI TimerText;


    public GameObject PausMenu;

    public AudioSource src1, src2;
    public AudioClip sfxM,sfxD, sfxP;
    void Start()
    {
        Timer = 0f;
        HealthDisplay();
        ScoreDisplay();
        HightScoreDisplay();
        TimerDisplay();
        backgroundMeusic();
       
    }

   
        
    

    void Update()
    {
        TimerUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButton();
            
        }
    }


    public void HealthDisplay()
    {
        PlayerHealthText.text = "Stock: " + Playerhealth;

    }

    public void ScoreDisplay()
    {
        PlayerScore.text = "Score: " + Score.ToString();

    }

    public void HightScoreDisplay()
    {
        PlayerHightScore.text = "High Score: " + HightScore.ToString();

    }
    public void TimerDisplay()
    {
        int minutes = Mathf.FloorToInt(Timer / 60);
        int seconds = Mathf.FloorToInt(Timer % 60);
        TimerText.text = "Time: " + minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }

    public void AddScore(int n)
    {
        src1.clip = sfxP;
        src1.Play();
        Score += n;
        PlayerScore.text = "Score: " + Score.ToString();
        if(Score >= BSpawnN)
        {
            Boss.SetActive(true);
        }
    }

    public void AddBossMeter(int B)
    {
        BMeter += B;
        if (BMeter >= BSpawnN)
        {
            Boss.SetActive(true);
        }
    }

    public void TimerUI()
    {
        Timer += Time.deltaTime;
        TimerDisplay();
        FinalTime = Timer;
    }


    public void ReduceLive()
    {
        src1.clip = sfxD;
        src1.Play();
        Debug.Log("Reducing health by: " + 1);

        Playerhealth -= 1;
        PlayerHealthText.text = "Stock: " + Playerhealth.ToString();
        if (Playerhealth <= 0)
        {
            PlayerPrefs.SetFloat("FinalTime", Timer);
            PlayerPrefs.Save(); // Ensure PlayerPrefs are saved immediately
            Debug.Log("Saving Timer: " + Timer);
            SceneManager.LoadScene("EndGame");
        }


        Debug.Log("Player's health now: " + Playerhealth);
    }


    public void ResetButton()
    {
        // SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseButton()
    {
        PausMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        PausMenu.SetActive(false);
        Time.timeScale = 1;
    }
   public void backgroundMeusic()
    {
        src2.clip = sfxM;
        src2.Play();
       
        
    }
}
