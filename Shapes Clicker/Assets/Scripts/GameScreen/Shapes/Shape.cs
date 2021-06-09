using System.Collections;
using UnityEngine;

public abstract class Shape : MonoBehaviour
{
    // use this for random target generation in Main Manager
    public enum Shapes
    {
        Square,
        Circle,
        Triangle
    }

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private ScreenShake shakeCamera;

    public string ShapeName { get; protected set; }
    public float Score { get; protected set; }

    protected Color color;

    private float sizeModifier;

    private void Awake()
    {
        InitSetup();
        shakeCamera = FindObjectOfType<ScreenShake>();
    }

    protected abstract void SetName();

    // can be white, override shapes used in this project so they can't be a color too close to the color of the board
    protected virtual void GenerateColor()
    {
        color = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = color;
    }

    // triangles need to be set up in a different way
    protected virtual void InitSetup()
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
            if (MainManager.SharedInstance.target.ToString() == this.ShapeName)
            {
                ParticleSystem.MainModule p_main = particles.main;
                p_main.startColor = color;
                Instantiate(particles, transform.position, particles.transform.rotation);
                MainManager.SharedInstance.AddScore(Score);
                MainUIManager.SharedInstance.AddTime(3);
            } else
            {
                shakeCamera.TriggerShake(0.7f);
                MainManager.SharedInstance.AddScore(-Score);
                MainUIManager.SharedInstance.AddTime(-1);
            }
            
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
