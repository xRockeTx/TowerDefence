using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuyOpen : MonoBehaviour
{
    [SerializeField] private List<Text> enemyText;
    [SerializeField] public GameObject pause;
    [SerializeField] private List<Transform> towers;
    [SerializeField] private List<int> towersPrice;
    [SerializeField] private SpawnEnemy spawner;
    [SerializeField] private GameObject statPanel, damageStatForEnemy;
    public GameObject faston;
    public GameObject fastty;
    public GameObject nomal;
    public GameObject fastfri;
    [SerializeField] private Transform radius;
    public GameObject buyPanel;
    [SerializeField] private Text moneyTxt;
    [SerializeField] private UpgradeTower upTower;
    [SerializeField] private Text range, cooldown, damage, speed, level, price, title;
    private Transform instRad;
    public int money;
    private bool click=false;
    private int lastId,needTimeId=0,j=0;
    private Transform place;
    private TowerShoot Tower;

    private void Start()
    {
        moneyTxt.text = money.ToString();
    }
    public void SetPlace(Transform placePos)
    {
        if (!buyPanel.activeSelf)
        {
            buyPanel.SetActive(true);
            place = placePos;
        }
    }
    public void OpenOrCloseStatForEnemy()
    {
        damageStatForEnemy.SetActive(!damageStatForEnemy.activeSelf);
        if (Tower != null)
        {
            foreach (CoefficientForEnemy enemy in Tower.coefficient)
            {
                EnemyType tag = enemy.Tag;
                int id = 0;
                if (EnemyType.Normal == tag)
                {
                    id = 0;
                    enemyText[id].text = (enemy.Сoeficient * 100).ToString();
                }
                else if (EnemyType.Strong == tag)
                {
                    id = 1;
                    enemyText[id].text = (enemy.Сoeficient * 100).ToString();
                }
                else if (EnemyType.Fast == tag)
                {
                    id = 2;
                    enemyText[id].text = (enemy.Сoeficient * 100).ToString();
                }
            }
        }
        else
        {
            foreach(Text text in enemyText)
            {
                text.text = "0";
            }
        }
    }
    public void OnPlace(int i)
    {
        if (click&&i==lastId)
        {
            BuildTower(i);
            click = false;
        }
        else {
            statPanel.SetActive(true);
            if(instRad!=null)
                Destroy(instRad.gameObject);
            Tower = towers[i].gameObject.GetComponent<TowerShoot>();
            ViewStats(Tower);
            lastId = i;
            click = true;
        }
    }
    private void ViewStats(TowerShoot tower)
    {
        OpenOrCloseStatForEnemy();
        damageStatForEnemy.SetActive(!damageStatForEnemy.activeSelf);
        instRad = Instantiate(radius);
        instRad.position = place.position;
        instRad.localScale = new Vector3(tower.upgrade[0].Range * 2, tower.upgrade[0].Range * 2, tower.upgrade[0].Range * 2);
        range.text = "Дальность: " + tower.upgrade[0].Range;
        cooldown.text = "Время перезарядки: " + tower.upgrade[0].Cooldown;
        damage.text = "Урон: " + tower.upgrade[0].BulletDamage;
        speed.text = "Скорость пули: " + tower.upgrade[0].BulletSpeed;
        level.text = "Уровень: 0";
        title.text = tower.Title;
        price.text = "Стоимость: " + tower.upgrade[0].Price;
    }
    private void BuildTower(int i)
    {
        if (towersPrice[i] <= money)
        {
            money -= towersPrice[i];
            moneyTxt.text = money.ToString();
            Destroy(instRad.gameObject);
            Transform tmpTower = Instantiate(towers[i]);
            tmpTower.position = new Vector3(place.position.x , 1f , place.position.z);
            tmpTower.GetComponent<TowerShoot>().upTower = upTower;
            tmpTower.GetComponent<TowerShoot>().spawner = spawner;
            buyPanel.SetActive(false);
        }
    }
    public void ChangeMoney(int price)
    {
        money += price;
        moneyTxt.text = money.ToString();
    }
    public void Close()
    {
        if (instRad != null)
            Destroy(instRad.gameObject);
        statPanel.SetActive(false);
        buyPanel.SetActive(false);
        click = false;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }

    public void Pause()     
    {
        pause.SetActive(true);
        Time.timeScale = 0;
    }
    public void comtini()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
   }

    public void ChangeTimeScale()
    {
        switch (needTimeId)
        {
            /*case 0:
                Time.timeScale = 0.5f;
                needTimeId++;
                faston.SetActive(true);
                fastty.SetActive(false);
                fastfri.SetActive(false);
                nomal.SetActive(false);
                break;
            case 1:
                Time.timeScale = 0.25f;
                faston.SetActive(false);
                fastty.SetActive(true);
                fastfri.SetActive(false);
                nomal.SetActive(false);
                needTimeId++;
                break;*/
            case 0:
                Time.timeScale = 0f;
                faston.SetActive(false);
                fastty.SetActive(false);
                fastfri.SetActive(true);
                nomal.SetActive(false);
                needTimeId++;
                break;
            case 1:
                Time.timeScale = 1f;
                faston.SetActive(false);
                fastty.SetActive(false);
                fastfri.SetActive(false);
                nomal.SetActive(true);
                needTimeId =0;
                break;
        }
    }
}
