using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PanelManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreTextLF;
    public TextMeshProUGUI highScoreTextTTP;
    public GameObject levelFailPanel;
    public GameObject tapToPlayPanel;
    public Coroutine Coroutine;
    public static PanelManager Instance;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        highScoreTextTTP.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void GetSceneButton()
    {
        levelFailPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TapToPlayButton()
    {
        tapToPlayPanel.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Coroutine = StartCoroutine(GameManager.Instance.Shoot());

    }

    public void HighScoreAnimation()
    {
        if(GameManager.Instance.reachedTheHighScore)
            scoreText.transform.DOMove(Vector3.zero, 1f).From();
    }
    
    
}
