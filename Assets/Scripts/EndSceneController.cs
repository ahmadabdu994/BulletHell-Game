using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    public Text gameOverText;
    public Text gameTimeText;
    public Text scoreText;
    public Text highScoreText;


    UIManager uiMan;

    private void Start()
    {

       // float gameTime = uiMan.TimerDisplay();
        int score = uiMan.Score;
        int highScore = uiMan.HightScore;

        gameOverText.text = "Game Over";
        //gameTimeText.text = "Time: " + gameTime.ToString("F2") + "s";
        scoreText.text = "Score: " + score.ToString();

        // Compare and update high score
        if (score > highScore)
        {
            highScore = score;
            //uiMan.SetHighScore(highScore); // Replace with your actual implementation.
        }


        highScoreText.text = "High Score: " + highScore.ToString();

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}