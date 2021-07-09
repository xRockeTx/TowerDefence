using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuyOpen : MonoBehaviour
{
    [SerializeField] private List<Transform> towers;
    [SerializeField] private List<int> towersPrice;
    [SerializeField] private GameObject statPanel;
    [SerializeField] private Transform radius;
    public GameObject buyPanel;
    [SerializeField] private Text moneyTxt;
    [SerializeField] private UpgradeTower upTower;
    [SerializeField] private Text range, cooldown, damage, speed, level, price;
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
            ViewStats(towers[i].gameObject.GetComponent<TowerShoot>());
            lastId = i;
            click = true;
        }
    }
    private void ViewStats(TowerShoot tower)
    {
        instRad=Instantiate(radius);
        instRad.position = place.position;
        instRad.localScale = new Vector3(tower.rangeTower[0] * 2, tower.rangeTower[0] * 2, tower.rangeTower[0] * 2);
        range.text = "Дальность: " + tower.rangeTower[0];
        cooldown.text = "Время перезарядки: " + tower.cooldownTower[0];
        damage.text = "Урон: " + tower.upDamage[0];
        speed.text = "Скорость пули: " + tower.upSpeed[0];
        level.text = "Уровень: 0";
        price.text = "Стоимость: " + tower.upgradePrice[0];
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
