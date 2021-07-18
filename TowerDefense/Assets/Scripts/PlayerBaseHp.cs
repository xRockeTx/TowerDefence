using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseHp : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private SpawnEnemy spawner;
    [SerializeField] private Text HitPoint;
    [SerializeField] private int hitPoint;

    private void Start()
    {
        HitPoint.text = hitPoint.ToString();
    }
    public void GetDamage(int damage,Transform enemy)
    {
        hitPoint -= damage;
        HitPoint.text = hitPoint.ToString();
        if (hitPoint <= 0)
        {
            spawner.allEnemy.Remove(enemy);
            Destroy(enemy.gameObject);
            spawner.Lose = true;
            Time.timeScale = 0;
            LosePanel.SetActive(true);
        }
    }
}
