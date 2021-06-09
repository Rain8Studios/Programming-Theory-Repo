using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Shape
{
    private Mesh m;
    private MeshFilter mf;
    private MeshCollider mcol;

    protected override void SetName()
    {
        ShapeName = "Triangle";
    }

    protected override void GenerateColor()
    {
        color = new Color(Random.value, 0, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = color;
    }

    protected override void InitSetup()
    {
        mf = GetComponent<MeshFilter>();
        mcol = GetComponent<MeshCollider>();
        m = new Mesh();
        mf.mesh = m;
        drawTriangle();

        SetName();
        SetScore();
        GenerateColor();
    }

    //This draws a triangle
    private void drawTriangle()
    {
        //We need two arrays one to hold the vertices and one to hold the triangles
        Vector3[] VerteicesArray = new Vector3[3];
        int[] trianglesArray = new int[3];

        //lets add 3 vertices in the 3d space
        VerteicesArray[0] = new Vector3(0, 1f, 0);
        VerteicesArray[1] = new Vector3(-0.5f, 0, 0);
        VerteicesArray[2] = new Vector3(0.5f, 0, 0);

        //define the order in which the vertices in the VerteicesArray shoudl be used to draw the triangle
        trianglesArray[0] = 0;
        trianglesArray[1] = 1;
        trianglesArray[2] = 2;

        //add these two triangles to the mesh
        m.vertices = VerteicesArray;
        m.triangles = trianglesArray;
        mcol.sharedMesh = m;

        transform.Rotate(new Vector3(0, 180, 0));
    }
}
