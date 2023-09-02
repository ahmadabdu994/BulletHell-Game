using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public GameObject gameTitleText;
    public TextMeshProUGUI highScoreText;

    public AudioSource src1,src2;
    public AudioClip sfxSt,sfxM;
    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore"); // Get the saved high score
        highScoreText.text = "High Score: " + highScore.ToString();
        src2.clip = sfxM;
        src2.Play();
    }
    public void Update()
    {
        
    }
    public void StartGame()
    {
        src1.clip = sfxSt;
        src1.Play();
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
        
        
    
}
