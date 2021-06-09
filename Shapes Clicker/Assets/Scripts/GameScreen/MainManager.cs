using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager SharedInstance;
    
    public bool GameOver { get; private set; }
    public Shape.Shapes target { get; private set; }

    [SerializeField] private ShapePooler shapePooler;

    private float targetDelay = 10f; // delay between changing targets
    private float minX = -3.15f;
    private float maxX = 2.9f;
    private float minY = -2.66f;
    private float maxY = 3.95f;
    private float currentScore = 0f;
    private int currentLevel = 1;

    void Awake()
    {
        SharedInstance = this;
        StartCoroutine(TriggerShapeSpawner());
        GenerateRandomTarget();
    }

    private IEnumerator TriggerShapeSpawner()
    {
        yield return new WaitForSeconds(1f);
        if (!GameOver)
        {
            SpawnShape();
            StartCoroutine(TriggerShapeSpawner());
        }
    }

    private void SpawnShape()
    {
        Shape shapeToSpawn = shapePooler.GetPooledObject();
        if (shapeToSpawn != null) // pooler is not full
        {
            shapeToSpawn.Spawn(new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0));
        }
    }

    public void TriggerGameOver()
    {
        GameOver = true;
        MainUIManager.SharedInstance.gameOverScreen.SetActive(true);
        if (Mathf.FloorToInt(currentScore) > GameManager.Instance.BestScore)
        {
            GameManager.Instance.BestScore = Mathf.FloorToInt(currentScore);
            GameManager.Instance.BestScoreName = GameManager.Instance.CurrentName;
            GameManager.Instance.SaveBestScore();
        }
    }

    public void AddScore(float scoreToAdd)
    {
        currentScore += scoreToAdd;
        MainUIManager.SharedInstance.UpdateCurrentScore(currentScore);

        UpdateLevel();
    }

    private void UpdateLevel()
    {
        if (currentLevel < 1 + (Mathf.FloorToInt(currentScore) / 60))
        {
            currentLevel++;
            Time.timeScale += 0.2f;
            MainUIManager.SharedInstance.UpdateLevel(currentLevel);
        }
    }

    private void GenerateRandomTarget()
    {
        target = (Shape.Shapes)Random.Range(0, System.Enum.GetNames(typeof(Shape.Shapes)).Length);
        MainUIManager.SharedInstance.UpdateTargetName();
        MainUIManager.SharedInstance.UpdateTargetImage(target);
        StartCoroutine(TriggerTargetDelay());
    }

    private IEnumerator TriggerTargetDelay()
    {
        yield return new WaitForSeconds(targetDelay);
        if (!GameOver)
        {
            GenerateRandomTarget();
        }
    }
}
