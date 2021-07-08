using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Transform waypointsParent;
    [SerializeField] private Text timeToNextSpawn;
    [SerializeField] private List<GameObject> enemy;
    private int waveCount = 0,maxWave,nextExoWave,exoWaveCount=0;
    private float timeToSpawn = 4;
    private bool canStartWave=true;
    [SerializeField] private List<ExoWave> exoWave;
    [SerializeField] private List<Wave> Wave;

    private void Start()
    {
        nextExoWave = exoWave[0].wave;
        maxWave = Wave.Count-1;
    }
    private void Update()
    {
        if (waveCount != maxWave)
        {
            if (timeToSpawn <= 0)
            {
                canStartWave = false;
                StartCoroutine(SpawnWave());
                timeToSpawn = 4;
            }
            if (timeToSpawn > 0 && canStartWave)
            {
                timeToSpawn -= Time.deltaTime;
                timeToNextSpawn.text = timeToSpawn.ToString();
            }
        }
    }
    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < Wave[waveCount].count; i++)
        {
            InstantiateEnemy(enemy[Convert.ToInt32(Wave[waveCount].type)]);
            if (nextExoWave == waveCount)
            {
                InstantiateEnemy(enemy[Convert.ToInt32(exoWave[exoWaveCount].type)]);
            }
            yield return new WaitForSeconds(0.3f);
        }
        if (nextExoWave == waveCount)
        {
            exoWaveCount++;
            nextExoWave =exoWave[exoWaveCount].wave;
        }
        canStartWave = true;
        waveCount++;
    }
    private void InstantiateEnemy(GameObject enemy)
    {
        Transform tmpEnemy = Instantiate(enemy, transform).transform;
        tmpEnemy.SetParent(transform, false);
        tmpEnemy.position = transform.position;
        tmpEnemy.gameObject.GetComponent<WalkEnemy>().SetWaypoints(waypointsParent);
        tmpEnemy = null;
    }
}
[Serializable]
class ExoWave
{
    public int wave;
    public int count;
    public EnemyType type;
}
enum EnemyType
{
    normal,
    strong,
    flyEnemy
}
[Serializable]
class Wave
{
    public int count;
    public EnemyType type;
}
