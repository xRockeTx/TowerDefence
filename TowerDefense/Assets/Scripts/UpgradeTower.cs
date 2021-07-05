using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    public GameObject upPan;
    private TowerShoot tower;
    private List<int> upgradePrice;
    private List<float> towerRange,towerCooldown;
    private int price,tier;
    [SerializeField] private BuyOpen buyOpen;
    public void Sell()
    {
        buyOpen=FindObjectOfType<BuyOpen>();
        buyOpen.ChangeMoney(price / 2);
        Destroy(tower.gameObject);
        upPan.SetActive(false);
    }
    public void Upgrade()
    {
        Debug.Log(tier);
        if (tier+1 < tower.transform.childCount)
        {
            if (buyOpen.money >= upgradePrice[tier])
            {
                tower.transform.GetChild(tier).gameObject.SetActive(false);
                Debug.Log(tier);
                tier++;
                tower.price = upgradePrice[tier];
                tower.tier = tier;
                tower.range = towerRange[tier];
                tower.cooldown = towerCooldown[tier];
                tower.transform.GetChild(tier).gameObject.SetActive(true);
            }
        }
        if (tower.transform.childCount == tier)
        {
            Debug.Log("its max lvl");
        }
        upPan.SetActive(false);
    }
    public void OpenPan(int price,List<int> upList,List<float> towerRange, List<float> cooldown, int tier,TowerShoot tower)
    {
        this.price = price;
        upgradePrice = upList;
        this.towerRange = towerRange;
        towerCooldown = cooldown;
        this.tier = tier;
        this.tower = tower;
        upPan.SetActive(true);
    }
}
