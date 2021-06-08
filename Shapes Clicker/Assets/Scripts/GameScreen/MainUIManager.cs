using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager SharedInstance;

    public GameObject gameOverScreen;
    public Text currentScoreText;
    public Text bestScoreText;
    public Text levelText;
    public Slider timerSlider;

    private float maxTime;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        maxTime = 30.0f;
        timeLeft = maxTime;
        timerSlider.maxValue = maxTime;
        UpdateCurrentScore(0);
        UpdateLevel(1);
        UpdateBestScore();
    }

    private void Awake()
    {
        SharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerSlider.value = timeLeft;
        } else
        {
            MainManager.SharedInstance.TriggerGameOver();
        }
    }

    public void AddTime(float timeToAdd)
    {
        timeLeft += timeToAdd;
        if (timeLeft > maxTime) timeLeft = maxTime;
    }

    public void UpdateCurrentScore(float currentScore)
    {
        currentScoreText.text = $"Score: {Mathf.FloorToInt(currentScore)}";
    }

    public void UpdateBestScore()
    {
        bestScoreText.text = $"{GameManager.Instance.BestScoreName} {GameManager.Instance.BestScore}";
    }

    public void UpdateLevel(int level)
    {
        levelText.text = $"Level: {Mathf.FloorToInt(level)}";
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
