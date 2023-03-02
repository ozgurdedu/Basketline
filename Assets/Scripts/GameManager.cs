using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    [Header("@ BALL")] 
    public float shootingForce;
    public Sprite[] ballSprites;
    public List<GameObject> failImages; 
    public int activeImageIndex = 0;
    [HideInInspector] public GameObject b;
    [Header("@ BASKET")] public GameObject Basket;
    [Header("@ UI")] 
    public int score;
    public int highScore = 0;
    public TextMeshProUGUI scoreText;
    public ParticleSystem particle;
    public AudioSource sound;
    public bool reachedTheHighScore;


    #region Singleton
   public static GameManager Instance;

   private void Awake()
   {
       Instance = this;
   }
   #endregion

    
   private void Start()
   {
       reachedTheHighScore = false;
       PanelManager.Instance.tapToPlayPanel.SetActive(true);

       scoreText.text = score.ToString();
       //StartCoroutine(Shoot());
       
       if (!PlayerPrefs.HasKey("HighScore")) 
           PlayerPrefs.SetInt("HighScore", highScore);

   }

  
   
   #region Shooting Functions
   // ReSharper disable Unity.PerformanceAnalysis
   public IEnumerator Shoot()
   {

       while (true)
       {
           RandomizeBasketPosition();
           Draw.Instance.ClearTheLines();
           BallPooler.Instance.DeactiveTheBall();
           
            
           b = BallPooler.Instance.GetActiveBall();
           b.transform.position = gameObject.transform.position;
           b.GetComponent<CircleCollider2D>().isTrigger = true;

           var ballObject = b.GetComponent<Ball>();
           ballObject.onTheLine = false;
           ballObject.inside = false;
           b.SetActive(true);

           var spriteIndex = Random.Range(0, ballSprites.Length);
           b.GetComponent<SpriteRenderer>().sprite = ballSprites[spriteIndex];

           var x = Random.Range(-2f, 2f);
            
           var force = new Vector2(x, 5);

           b.GetComponent<Rigidbody2D>().AddForce(force * shootingForce);

           yield return new WaitForSeconds(3f);
           if (!ballObject.inside)
           {
               ballObject.failCount++;
               failImages[activeImageIndex].SetActive(false);

               if (activeImageIndex < failImages.Count - 1)
                   activeImageIndex++;
               else
                   GameOver();
           }
   
       }

   }

   public void RandomizeBasketPosition()
   {
       var basketX = Random.Range(-2.5f, 2.5f);
       var basketY = Random.Range(-3f, 1.25f);
       var basketPos = new Vector2(basketX, basketY);
       Basket.transform.position = basketPos;
       Basket.SetActive(true);
       Basket.transform.DOScale(Vector3.zero, 0.5f).From();
   }


   #endregion
   public void GameOver()
   {
       Debug.Log("game over");
       PanelManager.Instance.levelFailPanel.SetActive(true);
       //StopAllCoroutines();
       //StopCoroutine(nameof(Shoot));
       StopCoroutine(PanelManager.Instance.Coroutine);
       //b.SetActive(false);
       //Basket.SetActive(false);
       GetPanelScores(); 
       PanelManager.Instance.HighScoreAnimation();
       
       // Draw.Instance.lines.Clear();
        
   }

   public void GetPanelScores()
   {
       if (score > PlayerPrefs.GetInt("HighScore", highScore) )
       {
           reachedTheHighScore = true;
           highScore = score; 
           PlayerPrefs.SetInt("HighScore", highScore);
       }
       
       PanelManager.Instance.scoreText.text = score.ToString();
       PanelManager.Instance.highScoreTextLF.text = PlayerPrefs.GetInt("HighScore").ToString();
       
       
   }
   
   
   
   
  
   
   
}
