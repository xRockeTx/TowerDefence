using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Advertising advertising;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Transform waypointsParent;
    [SerializeField] private Text timeToNextSpawn;
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private Transform PlayerBase;
    [SerializeField] private Text WaveCount;
    public List<Transform> allEnemy;
    private int waveCount = 0,maxWave,nextExoWave,exoWaveCount=0;
    private float timeToSpawn = 4;
    private bool canStartWave=true;
    public bool Win,Lose;
    [SerializeField] private int level;
    [SerializeField] private List<ExoWave> exoWave;
    [SerializeField] private List<Wave> Wave;

    private void Start()
    {
        nextExoWave = exoWave[0].wave;
        maxWave = Wave.Count-1;
        //WaveCount.text = "Волна: " + waveCount;
        //maxWave = 3;
    }
    private void Update()
    {
        if (waveCount == maxWave&&allEnemy.Count==0)
        {
            winPanel.SetActive(true);
        }
        if (waveCount != maxWave&&!Lose)
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
        WaveCount.text = waveCount.ToString();
        for (int i = 0; i < Wave[waveCount].count; i++)
        {
            if (Lose)
            {
                yield return 0;
            }
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
        if(waveCount == maxWave)
        {
            if (level == 1)
            {
                advertising.ShowAds();
            }
        }
    }
    private void InstantiateEnemy(GameObject enemy)
    {
        Transform tmpEnemy = Instantiate(enemy, transform).transform;
        tmpEnemy.SetParent(transform, false);
        tmpEnemy.position = transform.position;
        allEnemy.Add(tmpEnemy);
        tmpEnemy.gameObject.GetComponent<WalkEnemy>().SetWaypoints(waypointsParent);
        tmpEnemy.gameObject.GetComponent<WalkEnemy>().spawner=this;
        tmpEnemy.gameObject.GetComponent<WalkEnemy>().playerBase = PlayerBase;
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
public enum EnemyType
{
    Normal,
    Strong,
    Fast,
    FlyEnemy
}
[Serializable]
class Wave
{
    public int count;
    public EnemyType type;
}
