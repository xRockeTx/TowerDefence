using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Transform waypointsParent;
    [SerializeField] private Text timeToNextSpawn;
    [SerializeField] private List<GameObject> enemy;
    [SerializeField] private List<int> enemyType;
    [SerializeField] private List<int> enemyCount;
    private int waveCount = 0;
    private float timeToSpawn = 4;

    private void Update()
    {
        if (timeToSpawn <= 0)
        {
            StartCoroutine(SpawnWave());
            timeToSpawn = 4;
        }
        timeToSpawn -= Time.deltaTime;
        timeToNextSpawn.text = timeToSpawn.ToString();
    }
    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemyCount[waveCount]; i++)
        {
            Transform tmpEnemy = Instantiate(enemy[enemyType[waveCount]], transform).transform;
            tmpEnemy.SetParent(transform,false);
            tmpEnemy.position = transform.position;
            tmpEnemy.gameObject.GetComponent<WalkEnemy>().SetWaypoints(waypointsParent);
            tmpEnemy = null;
            yield return new WaitForSeconds(0.3f);
        }
        waveCount++;
    }
}
