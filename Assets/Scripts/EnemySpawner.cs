using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    IEnumerator Start()
    {
        do
        {

            yield return StartCoroutine(SpawnAllWawes());
        }
        while (looping);
    }
    private IEnumerator SpawnAllWawes()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];

            yield return StartCoroutine(WaveEnemySpawner(currentWave));
        }
    }
    private IEnumerator WaveEnemySpawner(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {

        var newEnemy = Instantiate(waveConfig.GetEnemyPathing(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWayConfig(waveConfig);

        yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawn());
        } 
    }
      
}
