using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CircleCollider2D _col;
    public bool onTheLine;
    public bool inside;
    public int failCount;
    
    
    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _col = gameObject.GetComponent<CircleCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Left"))
        {
            _rb.velocity = Vector2.right * 3f;
        }

        if (col.CompareTag("Right"))
        {
            _rb.velocity = Vector2.left * 3f;
        }

        if (col.CompareTag("Top"))
        {
            _rb.velocity = Vector2.down * 3f;
        }


        if (col.CompareTag("Bottom"))
        {
            GameManager.Instance.GameOver();
            
        }

        if (col.CompareTag("Line"))
        {
            onTheLine = true;
            _col.isTrigger = false;
        }
        
        
        
        
        if (col.CompareTag("Inside"))
        {
            if (onTheLine)
            {
                GameManager.Instance.score++;
                GameManager.Instance.scoreText.text = GameManager.Instance.score.ToString();

                var ps = GameManager.Instance.particle;
                Instantiate(ps, col.transform);

                GameManager.Instance.sound.Play();
                inside = true;
            }
        }
       
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bottom"))
        {
            //game over
            GameManager.Instance.GameOver();
            
        }
    }


    // enum Directions
    // {
    //     Right, 
    //     Left, 
    //     Top
    // }

    

    

    
}
