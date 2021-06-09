using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager SharedInstance;

    public GameObject gameOverScreen;

    [SerializeField] private Text currentScoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text targetName;
    [SerializeField] private Image targetImage;
    [SerializeField] private Sprite squareSprite;
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite triangleSprite;
    [SerializeField] private Slider timerSlider;

    private float maxTime;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxTime(30.0f);
        UpdateCurrentScore(0);
        UpdateLevel(1);
        UpdateBestScore();
    }

    void Awake()
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

    public void SetMaxTime(float maxTimeToSet)
    {
        maxTime = maxTimeToSet;
        timeLeft = maxTime;
        timerSlider.maxValue = maxTime;
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

    public void UpdateTargetName()
    {
        
        targetName.text = MainManager.SharedInstance.target.ToString();
    }

    public void UpdateTargetImage(Shape.Shapes shape)
    {
        switch (shape)
        {
            case Shape.Shapes.Square:
                targetImage.sprite = squareSprite;
                break;
            case Shape.Shapes.Circle:
                targetImage.sprite = circleSprite;
                break;
            case Shape.Shapes.Triangle:
                targetImage.sprite = triangleSprite;
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
