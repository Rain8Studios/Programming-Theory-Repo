using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    protected override void SetName()
    {
        ShapeName = "Circle";
    }

    protected override void GenerateColor()
    {
        color = new Color(Random.value, Random.value, 0, 1.0f);
        GetComponent<Renderer>().material.color = color;
    }
}
