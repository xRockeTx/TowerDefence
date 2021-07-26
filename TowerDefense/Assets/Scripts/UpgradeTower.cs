using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour
{
    [SerializeField] private List<Text> enemyText;
    [SerializeField] private Text range, cooldown, level, damage, priceText, speed, title;
    [SerializeField] private Transform radius;
    private List<Transform> instRad=new List<Transform>();
    public GameObject upPan, damageStatForEnemy;
    private TowerShoot tower;
    private List<Upgrade> upgradeTower;
    private int price,tier;
    public BuyOpen buyOpen;
    private bool click=false;

    public void Sell()
    {
        buyOpen=FindObjectOfType<BuyOpen>();
        buyOpen.ChangeMoney(upgradeTower[tier].Price / 2);
        Destroy(tower.gameObject);
        for (int i = 0; i < instRad.Count; i++)
        {
            Destroy(instRad[i].gameObject);
        }
        instRad.Clear();
        upPan.SetActive(false);
    }
    public void CloseMenu()
    {
        damageStatForEnemy.SetActive(false);
        DestroyRadius();
        upPan.SetActive(false);
    }
    public void OnUpgrade()
    {
        if (tier < tower.MaxTier)
        {
            if (click)
            {
                Upgrade();
                click = false;
            }
            else
            {
                ViewStats(tier + 1);
                click = true;
            }
        }
    }
    public void OpenOrCloseStatForEnemy()
    {
        damageStatForEnemy.SetActive(!damageStatForEnemy.activeSelf);
        if (tower != null)
        {
            foreach (CoefficientForEnemy enemy in tower.coefficient)
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
            foreach (Text text in enemyText)
            {
                text.text = "0";
            }
        }
    }
    private void DestroyRadius()
    {
        for (int i = 0; i < instRad.Count; i++)
        {
            Destroy(instRad[i].gameObject);
        }
        instRad.Clear();
    }
    private void ViewStats(int lvl)
    {
        if (instRad.Count != 0)
        {
            DestroyRadius();
        }
        range.text = "Дальность: " + upgradeTower[lvl].Range;

        instRad.Add(Instantiate(radius));
        instRad[instRad.Count - 1].position = tower.transform.position;
        instRad[instRad.Count - 1].localScale = new Vector3(upgradeTower[lvl].Range * 2, upgradeTower[lvl].Range * 2, upgradeTower[lvl].Range * 2);

        cooldown.text = "Время перезарядки: " + upgradeTower[lvl].Cooldown;
        damage.text = "Урон: "+ upgradeTower[lvl].BulletDamage;
        speed.text = "Скорость пули: " + upgradeTower[lvl].BulletSpeed;
        level.text = "Уровень: " + lvl;
        title.text = tower.Title;
        if (lvl < tower.MaxTier)
        {
            priceText.text = "Стоимость: " + upgradeTower[lvl + 1].Price;
        }
        else
            priceText.text = "Макс уровень";
    }
    private void Upgrade()
    {
        if (buyOpen.money >= upgradeTower[tier].Price)
        {
            buyOpen.ChangeMoney(-upgradeTower[tier].Price);
            tower.transform.GetChild(tier).gameObject.SetActive(false);
            DestroyRadius();
            tier++;
            tower.tier = tier;
            /*tower.price = upgradeTower[tier].Price;
            tower.range = upgradeTower[tier].Range;
            tower.cooldown = upgradeTower[tier].Cooldown;
            tower.damage = upgradeTower[tier].BulletDamage;
            tower.speed = upgradeTower[tier].BulletSpeed; */
            tower.transform.GetChild(tier).gameObject.SetActive(true);
            ViewStats(tier);
        }
        //upPan.SetActive(false);
    }
    public void OpenPan(List<Upgrade> up,int tier,TowerShoot tower)
    {
        upgradeTower = up;
        this.tier = tier;
        this.tower = tower;
        ViewStats(tier);
        upPan.SetActive(true);
    }
}
