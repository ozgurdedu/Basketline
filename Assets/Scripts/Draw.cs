using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject linePrefab;
    [HideInInspector]public GameObject line;
    [HideInInspector]public List<GameObject> lines;
    [HideInInspector]public LineRenderer lineRenderer;
    [HideInInspector]public EdgeCollider2D edgeCollider;
    [HideInInspector]public List<Vector2> fingerPositionList;
    public static Draw Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            CreateLine();
            // var b = BallPooler.Instance.GetActiveBall(); 
            // b.SetActive(true);
            // b.transform.position = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(fingerPos, fingerPositionList[^1] ) > .1f)
            {
                UpdateTheLine(fingerPos);
            }
        }
    }


    void CreateLine()
    {
        line = Instantiate(linePrefab);
        lines.Add(line);
        lineRenderer = line.GetComponent<LineRenderer>();
        edgeCollider = line.GetComponent<EdgeCollider2D>();
        fingerPositionList.Clear();
        
        fingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
        lineRenderer.SetPosition(0, fingerPositionList[0]);
        lineRenderer.SetPosition(1, fingerPositionList[1]);

        edgeCollider.points = fingerPositionList.ToArray();
    }

    void UpdateTheLine(Vector2 fingerPosition)
    {
        fingerPositionList.Add(fingerPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, fingerPosition);
        edgeCollider.points = fingerPositionList.ToArray();
    }


    public void ClearTheLines()
    {
        foreach (var l in lines)
        {
            l.SetActive(false);
        }
        lines.Clear();
    }
    
}
