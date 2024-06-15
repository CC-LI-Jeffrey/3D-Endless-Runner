using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int score = 0;
    private int bestScore;
    public static GameManager inst;
    public float SpeedUpPerPoint;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] AudioManager audioManager;

    public void IncrementScore()
    {
        audioManager.playCoinSound();
        score++;
        scoreText.text = "SCORE: " + score + "\nBEST: " + bestScore;
        playerMovement.speed += SpeedUpPerPoint;
    }

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        bestScore = PlayerPrefs.GetInt("Best");
        scoreText.text = "SCORE: " + score + "\nBEST: " + bestScore;
        inst = this;
    }

    public void Restart()
    {
        audioManager.playDieSound();
        if (score > bestScore) {
            bestScore = score;
            PlayerPrefs.SetInt("Best", bestScore);
        }
        gameOverScreen.SetActive(true);
        StartCoroutine("RestartCoroutine");
    }

    private IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
