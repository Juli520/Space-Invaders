using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviourPun
{
    public static ScoreManager Instance = null;

    public TMP_Text scoreText;
    
    [SerializeField, HideInInspector] private int _score;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _score = 0;
    }

    private void Update()
    {
        if (scoreText == null)
            return;

        scoreText.text = "Score: " + _score; 
    }

    public void AddScore(int points)
    {
        photonView.RPC("AddScoreRPC", RpcTarget.All, points);
    }  
    
    [PunRPC]
    public void AddScoreRPC(int points)
    {
        _score += points;
    }
}
