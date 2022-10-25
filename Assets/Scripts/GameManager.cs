using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    private const int COIN_SCORE_AMOUNT = 5;
    public static GameManager Instance { set; get; }

    public bool IsDead { set; get; }
    private PlayerMotor motor;

    private bool isSilent = false;

    private bool isGameStarted = false;
    public GameObject highScoreObject, innerHighScore, silent, playButton, menuButton, menuOptions;

    // UI and the UI feilds
    public Animator gameCanvas, menuAnim, diamondAnim;
    public TextMeshProUGUI scoreText, coinText, modifierText, highScore;
    private float score, coinScore, modifierScore;
    private int lastScore;

    // Audio
    public AudioSource audioCrash, audioPlay, audioBackGround, audioPoint;

    // Death menu
    public Animator deathMenuAnim;
    public TextMeshProUGUI deadScoreText, deadCoinText;
    public int player;
    private void Awake() 
    {
        player = PlayerSelector.sp;

        Instance = this;
        modifierScore = 1;         
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

        modifierText.text = "x" + modifierScore.ToString("0.0");
        coinText.text = coinScore.ToString("0");
        scoreText.text = scoreText.text = score.ToString("0");

        highScore.text = "High Score\n" + PlayerPrefs.GetInt("highScore").ToString();
    }

    private void Update() 
    {
        /**if(MobileInput.Instance.Tap && !isGameStarted)    
        {
            audioPlay.Play();
            isGameStarted = true;
            motor.StartRunning();
            FindObjectOfType<GlacierSpawner>().IsScrolling = true;
            FindObjectOfType<CameraMotor>().IsMoving = true;
            gameCanvas.SetTrigger("Show");
            menuAnim.SetTrigger("Hide");
        }*/
        SelectedPlayer();

        if(isGameStarted && !IsDead)
        {
            
            highScoreObject.SetActive(false);
            // Bump score up
            lastScore = (int)score;
            score += (Time.deltaTime * modifierScore);
            if(lastScore != (int)score)
            {
                lastScore = (int)score;
                scoreText.text = score.ToString("0");
            }
        }

        innerHighScore.GetComponent<TextMeshProUGUI>().text = "High Score\n" + PlayerPrefs.GetInt("highScore").ToString();
    }

    public void GetCoin()
    {
        audioPoint.Play();
        diamondAnim.SetTrigger("Collect");
        coinScore++;
        coinText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = scoreText.text = score.ToString("0");
    }

    public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");
    }

    public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnDeath()
    {
        audioCrash.Play();
        audioPlay.Stop();
        IsDead = true;
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        deadScoreText.text = score.ToString("0");
        deadCoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");
        gameCanvas.SetTrigger("Hide");
        

         
        // Check if this is highscore
        if(score > PlayerPrefs.GetInt("highScore")) 
        {
            float s = score;
            if(s % 1 == 0)
                s += 1;
            PlayerPrefs.SetInt("highScore", (int)s);
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("highScore", 0);
    }

    public void MenuOption()
    {
        menuOptions.SetActive(true);
        SceneManager.LoadScene("Players");
    }

    public void OncancelB()
    {
        menuOptions.SetActive(false);
    }
    public void SelectedPlayer()
    {
        players[player].SetActive(true);
        for (int n = 0; n < players.Length; n++)
        {
            if(n != player)
            {
                players[n].SetActive(false);
            }
        }            
    }
    public void OnPlay()
    {
            audioPlay.Play();
            isGameStarted = true;
            motor.StartRunning();
            FindObjectOfType<GlacierSpawner>().IsScrolling = true;
            FindObjectOfType<CameraMotor>().IsMoving = true;
            gameCanvas.SetTrigger("Show");
            menuAnim.SetTrigger("Hide");
            playButton.SetActive(false);
            menuButton.SetActive(false);
    }

    public void MuteButton()
    {
        if(!isSilent)
            AudioOff();
        else
            AudioOn();
    }
    public void AudioOff()
    {
        silent.SetActive(true);
        audioCrash.volume = 0;
        audioPlay.volume = 0;
        audioBackGround.volume = 0;
        audioPoint.volume = 0;
        isSilent = true;
    }
    public void AudioOn()
    {
        silent.SetActive(false);
        audioCrash.volume = 1.0f;
        audioPlay.volume = 0.5f;
        audioBackGround.volume = 0.1f;
        audioPoint.volume = 1.0f;
        isSilent = false;
    }
}
