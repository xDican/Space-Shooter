﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefeb;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab(){ return enemyPrefeb; }

    public List<Transform> GetWayPoints()
    { 
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }        
        return waveWayPoints; 
    }

    public float GetTimeBetweenSpaws(){ return timeBetweenSpawn; }

    public float GetSpawnRandomFactor(){ return spawnRandomFactor; }

    public int GetNumberOfEnemies(){ return numberOfEnemies; }
    
    public float GetMoveSpeed(){ return moveSpeed; }
}
