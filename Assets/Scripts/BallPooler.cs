using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPooler : MonoBehaviour
{
    public GameObject ballPrefab;
    [HideInInspector] public GameObject ball;
    public int ballCount;
    public List<GameObject> balls = new List<GameObject>();

    public static BallPooler Instance;
    
    [Header("Ball Pool Location")] public Transform ballPool;
    

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < ballCount; i++)
        {
            ball = Instantiate(ballPrefab, ballPool);
            ball.SetActive(false);
            balls.Add(ball);
        }
    }

    public GameObject GetActiveBall()
    {
        foreach (var b in balls)
        {
            if (!b.activeInHierarchy)
            {
                return b;
            }
        }
        return null;
    }

    public void DeactiveTheBall()
    {
        foreach (var b in balls)
        {
            if (b.activeInHierarchy)
            {
                b.SetActive(false);
            }
            
        }
    }
    
}
