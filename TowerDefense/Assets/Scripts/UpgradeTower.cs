using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour
{
    [SerializeField] private Text range, cooldown, level, damage,priceText,speed;
    public GameObject upPan;
    private TowerShoot tower;
    private List<int> upgradePrice,bulletDamage,bulletSpeed;
    private List<float> towerRange,towerCooldown;
    private int price,tier;
    [SerializeField] private BuyOpen buyOpen;
    private bool click=false;
    public void Sell()
    {
        buyOpen=FindObjectOfType<BuyOpen>();
        buyOpen.ChangeMoney(price / 2);
        Destroy(tower.gameObject);
        upPan.SetActive(false);
    }
    public void CloseMenu()
    {
        upPan.SetActive(false);
    }
    public void OnUpgrade()
    {
        if (click&& tier + 1 < tower.transform.childCount)
        {
            Upgrade();
            click = false;
        }
        else if(tier + 1 < tower.transform.childCount)
        {
            ViewStats(tier+1);
            click = true;
        }
    }
    private void ViewStats(int lvl)
    {
        range.text = "Дальность: " + towerRange[lvl];
        cooldown.text = "Время перезарядки: " + towerCooldown[lvl];
        damage.text = "Урон: "+ bulletDamage[lvl];
        speed.text = "Скорость пули: " + bulletSpeed[lvl];
        level.text = "Уровень: " + lvl;
        if (tier + 1 < tower.transform.childCount) 
            priceText.text = "Стоимость: " + upgradePrice[tier+1];
        else
            priceText.text = "Макс уровень";
    }
    private void Upgrade()
    {
        if (buyOpen.money >= upgradePrice[tier])
        {
            buyOpen.ChangeMoney(-upgradePrice[tier]);
            tower.transform.GetChild(tier).gameObject.SetActive(false);
            tier++;
            tower.price = upgradePrice[tier];
            tower.tier = tier;
            tower.range = towerRange[tier];
            tower.cooldown = towerCooldown[tier];
            tower.damage = bulletDamage[tier];
            tower.speed = bulletSpeed[tier];
            tower.transform.GetChild(tier).gameObject.SetActive(true);
        }
        upPan.SetActive(false);
    }
    public void OpenPan(int price,List<int> upList, List<int> upSpeed, List<int> upDamage,List<float> towerRange, List<float> cooldown, int tier,TowerShoot tower)
    {
        this.price = price;
        upgradePrice = upList;
        bulletSpeed = upSpeed;
        bulletDamage = upDamage;
        this.towerRange = towerRange;
        towerCooldown = cooldown;
        this.tier = tier;
        this.tower = tower;
        ViewStats(tier);
        upPan.SetActive(true);
    }
}
