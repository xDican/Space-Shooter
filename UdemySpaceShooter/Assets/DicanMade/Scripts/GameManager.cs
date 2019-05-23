using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int playerScore;
    
    
    void Awake()
    {
        CreateSingleTon();        
    }

    private void CreateSingleTon()
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public int GetPlayerScore(){ return playerScore;}

    public void AddToScore(int scoreValue){ playerScore += scoreValue;}

    public void ResetScore(){ Destroy(this.gameObject);}
}
