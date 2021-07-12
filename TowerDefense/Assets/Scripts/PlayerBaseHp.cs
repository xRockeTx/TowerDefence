using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseHp : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private SpawnEnemy spawner;
    [SerializeField] private int hitPoint;

    public void GetDamage(int damage)
    {
        hitPoint -= damage;
        if (hitPoint <= 0)
        {
            spawner.Lose = true;
            LosePanel.SetActive(true);
        }
    }
}
