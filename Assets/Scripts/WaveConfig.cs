using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [Header("Prefab Case")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;

    [Header("Characteristics")]
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;

    public GameObject GetEnemyPathing()
    {
        return enemyPrefab;
    }
    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform) 
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;

    }
    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetTimeBetweenSpawn()
    {
        return timeBetweenSpawns;
    }
    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }



}
