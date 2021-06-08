using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shape
{
    protected override void SetName()
    {
        ShapeName = "Square";
    }

    protected override void GenerateColor()
    {
        Color newColor = new Color(0, Random.value, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = newColor;
    }
}
