using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyOpen : MonoBehaviour
{
    [SerializeField] private List<Transform> towers;
    [SerializeField] private List<int> towersPrice;
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private Text moneyTxt;
    [SerializeField] private UpgradeTower upTower;
    public int money;
    private Transform place;
    private void Start()
    {
        moneyTxt.text = money.ToString();
    }
    public void SetPlace(Transform placePos)
    {
        place = placePos;   
    }
    public void BuildTower(int i)
    {
        if (towersPrice[i]<=money)
        {
            money -= towersPrice[i];
            moneyTxt.text = money.ToString();
            Transform tmpTower = Instantiate(towers[i]);
            tmpTower.position = place.position;
            tmpTower.GetComponent<TowerShoot>().upTower = upTower;
        }
        buyPanel.SetActive(false);
    }
    public void ChangeMoney(int price)
    {
        money += price;
        moneyTxt.text = money.ToString();
    }
}
