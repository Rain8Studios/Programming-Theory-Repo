using System.Collections;
using UnityEngine;

public abstract class Shape : MonoBehaviour
{
    public string ShapeName;
    public float Score;

    private float sizeModifier;

    private void Awake()
    {
        InitSetup();
    }

    protected abstract void SetName();

    protected virtual void GenerateColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = newColor;
    }

    protected void InitSetup()
    {
        SetName();
        SetScore();
        GenerateColor();
    }

    protected void SetScore()
    {
        GenerateSize();
        Score = 5 / sizeModifier;
    }

    private void GenerateSize()
    {
        sizeModifier = Random.Range(0.4f, 1f);
        transform.localScale *= sizeModifier;
    }

    private void OnMouseDown()
    {
        if (!MainManager.SharedInstance.GameOver)
        {
            MainManager.SharedInstance.AddScore(Score);
            MainUIManager.SharedInstance.AddTime(1);
            gameObject.SetActive(false);
        }
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        StartCoroutine(Deactivate());
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

}
