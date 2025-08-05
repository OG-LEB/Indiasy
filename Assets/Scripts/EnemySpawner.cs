using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner instance;
    public static EnemySpawner GetInstance() { return instance; }
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private float[] WaveKotelHP;
    [SerializeField] private float[] WaveSpawnCooldown;
    private int waveCounter;
    private float SpawnCoolDown;
    [SerializeField] private List<EnemyScript> SpawnedEnemies;
    public void SpawnEnemy()
    {
        //Vector3 roundSpawn = Random.insideUnitSphere;
        int randomxValue = Random.Range(-50, 50);
        if (randomxValue > 0)
        {
            randomxValue = Mathf.Clamp(randomxValue, 15, 50);
        }
        if (randomxValue < 0)
        {
            randomxValue = Mathf.Clamp(randomxValue, -15, -50);
        }

        int randomyValue = Random.Range(-50, 50);
        if (randomyValue > 0)
        {
            randomyValue = Mathf.Clamp(randomyValue, 15, 50);
        }
        if (randomyValue < 0)
        {
            randomyValue = Mathf.Clamp(randomyValue, -15, -50);
        }

        Vector3 spawnPoint = new Vector3(randomxValue, 5, randomyValue);
        Debug.Log($"SPawned {spawnPoint}");
        GameObject enemy = Instantiate(Enemies[Random.Range(0, 3)], spawnPoint, Quaternion.identity);
        SpawnedEnemies.Add(enemy.GetComponent<EnemyScript>());
    }

    public void StartWave()
    {
        //Debug.Log("Start Wave");
        SpawnCoolDown = WaveSpawnCooldown[waveCounter];
        StartCoroutine(WaveSpawnCicle());
    }
    private IEnumerator WaveSpawnCicle()
    {

        SpawnEnemy();
        yield return new WaitForSeconds(SpawnCoolDown);
        StartCoroutine(WaveSpawnCicle());
    }

    public void StopWave()
    {
        StopAllCoroutines();
        waveCounter++;
        for (int i = 0; i < SpawnedEnemies.Count; i++)
        {
            Destroy(SpawnedEnemies[i].gameObject);
        }
        SpawnedEnemies.Clear();
        //Debug.Log("Wave is ended");
    }

    public void RemoveEnemyFromList(EnemyScript enemy)
    {
        SpawnedEnemies.Remove(enemy);
    }
}
