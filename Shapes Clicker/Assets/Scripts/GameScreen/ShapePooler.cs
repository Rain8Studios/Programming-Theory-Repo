﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePooler: MonoBehaviour
{
    public static ShapePooler SharedInstance;
    public List<Shape> PooledShapes;
    public Shape[] ShapesToPool;
    public int AmountToPool;

    private int currentIndex;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;

        // Loop through list of pooled random shapes,deactivating them and adding them to the list 
        PooledShapes = new List<Shape>();
        for (int i = 0; i < AmountToPool; i++)
        {
            Shape shp = (Shape)Instantiate(ShapesToPool[Random.Range(0, ShapesToPool.Length)]);
            shp.gameObject.SetActive(false);
            PooledShapes.Add(shp);
            shp.transform.SetParent(this.transform); // set as children of Main Manager
        }

        
    }

    public Shape GetPooledObject()
    {
        // check if pool is full first
        bool poolFull = true;
        foreach (Shape shape in PooledShapes)
        {
            if (!shape.gameObject.activeInHierarchy)
            {
                poolFull = false;
                break;
            }
        }

        if (poolFull) return null;
        else
        {
            // there must be an inactive object after first check
            while (true)
            {
                if (currentIndex < PooledShapes.Count - 1)
                {
                    currentIndex++;
                    if (!PooledShapes[currentIndex].gameObject.activeInHierarchy)
                    {
                        return PooledShapes[currentIndex];
                    }
                }
                else
                {
                    currentIndex = 0;
                    IListExtensions.Shuffle<Shape>(PooledShapes);
                }
            }
        }
    }
}
