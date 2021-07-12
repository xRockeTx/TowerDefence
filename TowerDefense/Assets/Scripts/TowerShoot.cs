using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerShoot : MonoBehaviour
{
    public List<Upgrade> upgrade;
    [SerializeField] private List<CoefficientForEnemy> coefficient;
    [SerializeField] private List<string> tags;
    [SerializeField] private List<float> coef;
    public List<Transform> allEnemy;
    private string enemyTag;
    public UpgradeTower upTower;
    public Transform bullet;
    public float range, cooldown, currentCooldown;
    public int price,tier=0,damage,speed;
    public int MaxTier=4;
    public string Title;
    private Transform enemy;

    private void Update()
    {
        if (enemy != null)
        {
            transform.GetChild(tier).GetChild(0).LookAt(enemy, Vector3.up);
            transform.GetChild(tier).GetChild(0).Rotate(-89.98f, 0, 0);
        }
        if (CanShoot())
        {
            SearchTarget();
        }
        if(currentCooldown>0)
            currentCooldown -= Time.deltaTime;
    }
    private bool CanShoot()
    {
        if (currentCooldown<=0)
            return true;
        return false;
    }
    private void SearchTarget()
    {
        Transform nearestEnemy = null;
        float distance = Mathf.Infinity;
        foreach (string tag in tags)
        {
            foreach (Transform enemy in allEnemy)
            {
                if (enemy.gameObject.CompareTag(tag))
                {
                    float currDistance = Vector3.Distance(transform.position, enemy.position);
                    if (currDistance < distance && currDistance <= range)
                    {
                        nearestEnemy = enemy;
                        distance = currDistance;
                    }
                }
                enemyTag = tag;
            }
        }
        if (nearestEnemy != null)
        {
            enemy = nearestEnemy;
            Shoot(nearestEnemy,tags.IndexOf(enemyTag,0));
        }

    }
    private void Shoot(Transform enemy,int tagIndex)
    {
        currentCooldown = cooldown;
        Transform tmpBullet = Instantiate(bullet);
        tmpBullet.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        tmpBullet.GetComponent<BulletFly>().damage = damage;
        tmpBullet.GetComponent<BulletFly>().speed = speed;
        tmpBullet.GetComponent<BulletFly>().SetTarget(enemy,coef[tagIndex]);
    }
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if(!upTower.gameObject.GetComponent<BuyOpen>().buyPanel.activeSelf)
                upTower.OpenPan(upgrade,tier, this);
        }
    }
}

[Serializable]
public class Upgrade
{
    public int Price;
    public int Range;
    public float Cooldown;
    public int BulletDamage;
    public int BulletSpeed;
}

[Serializable]
public class CoefficientForEnemy
{
    public string Tag;
    public float coeficient;
}

