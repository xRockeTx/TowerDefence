using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuyOpen : MonoBehaviour
{
    [SerializeField] private List<Transform> towers;
    [SerializeField] private List<int> towersPrice;
    [SerializeField] private SpawnEnemy spawner;
    [SerializeField] private GameObject statPanel;
    [SerializeField] private Transform radius;
    public GameObject buyPanel;
    [SerializeField] private Text moneyTxt;
    [SerializeField] private UpgradeTower upTower;
    [SerializeField] private Text range, cooldown, damage, speed, level, price, title;
    private Transform instRad;
    public int money;
    private bool click=false;
    private int lastId;
    private Transform place;
    private void Start()
    {
        moneyTxt.text = money.ToString();
    }
    public void SetPlace(Transform placePos)
    {
        buyPanel.SetActive(true);
        place = placePos;   
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
            ViewStats(towers[i].gameObject.GetComponent<TowerShoot>());
            lastId = i;
            click = true;
        }
    }
    private void ViewStats(TowerShoot tower)
    {
        instRad=Instantiate(radius);
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
            tmpTower.position = place.position;
            tmpTower.GetComponent<TowerShoot>().upTower = upTower;
            tmpTower.GetComponent<TowerShoot>().allEnemy = spawner.allEnemy;
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
        statPanel.SetActive(false);
        buyPanel.SetActive(false);
        click = false;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
