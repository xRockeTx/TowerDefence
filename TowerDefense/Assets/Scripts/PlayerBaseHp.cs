using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseHp : MonoBehaviour
{
    [SerializeField] private Advertising advertising;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private SpawnEnemy spawner;
    [SerializeField] private Text HitPoint;
    [SerializeField] private int hitPoint;
    private int lose;

    private void Start()
    {
        HitPoint.text = hitPoint.ToString();
        lose = PlayerPrefs.GetInt("Lose");
    }
    public void GetDamage(int damage,Transform enemy)
    {
        hitPoint -= damage;
        HitPoint.text = hitPoint.ToString();
        if (hitPoint <= 0)
        {
            Debug.Log(PlayerPrefs.GetInt("Lose"));
            spawner.allEnemy.Remove(enemy);
            Destroy(enemy.gameObject);
            if (lose == 2)
            {
                advertising.ShowAdvertising(1);
                lose = 0;
            }
            else if (lose < 2)
            {
                lose++;
            }
            PlayerPrefs.SetInt("Lose", lose);
            spawner.Lose = true;
            Time.timeScale = 0;
            LosePanel.SetActive(true);
        }
    }
}
