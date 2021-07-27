using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerShoot : MonoBehaviour
{

    public List<Upgrade> upgrade;
    [SerializeField] private TowerType towerType;
    public List<CoefficientForEnemy> coefficient;
    [SerializeField] private List<Transform> GunPoint, TurelRotate;
    public SpawnEnemy spawner;
    private string enemyTag;
    public UpgradeTower upTower;
    public Transform bullet;
    public float currentCooldown;
    public int tier=0;
    public int MaxTier=4;
    public string Title;
    private Transform enemy;
    private List<Transform> enemies=new List<Transform>();

    private void Update()
    {
        if (enemy != null)
        {
            TurelRotate[tier].LookAt(enemy, Vector3.up);
            TurelRotate[tier].Rotate(0, 0, 0);
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
        float distance = Mathf.Infinity;
        switch (Convert.ToInt32(towerType))
        {
            case 1:
                int id1 = -1;
                Transform nearestEnemy = null;
                foreach (CoefficientForEnemy tagCoef in coefficient)
                {
                    id1++;
                    string tag = tagCoef.Tag.ToString();
                    foreach (Transform enemy in spawner.allEnemy)
                    {
                        if (enemy != null)
                        {
                            if (enemy.tag==tag)
                            {
                                float currDistance = Vector3.Distance(transform.position, enemy.position);
                                if (currDistance < distance && currDistance <= upgrade[0].Range)
                                {
                                    nearestEnemy = enemy;
                                    distance = currDistance;
                                }
                            }
                            enemyTag = tag;
                        }
                    }
                }
                if (nearestEnemy != null)
                {
                    enemy = nearestEnemy;
                    Shoot(enemy, id1);
                }
                break;
            case 2:
                int id2 = -1;
                foreach (CoefficientForEnemy tagCoef in coefficient)
                {
                    id2 ++;
                    string tag = tagCoef.Tag.ToString();
                    if (enemies.Count != 0)
                    {
                        foreach (Transform enemy in enemies)
                        {
                            Shoot(enemy, id2);
                        }
                    }
                    foreach (Transform enemy in spawner.allEnemy)
                    {
                        if (enemy != null)
                        {
                            if (enemy.tag == tag)
                            {
                                float currDistance = Vector3.Distance(transform.position, enemy.position);
                                if (currDistance < distance && currDistance <= upgrade[0].Range)
                                {
                                    enemies.Add(enemy);
                                    distance = currDistance;
                                }
                            }
                            enemyTag = tag;
                        }
                    }
                }
                break;
            case 3:
                goto case 1;
            case 4:
                goto case 2;
        }

    }
    private void Shoot(Transform enemy, int tagIndex)
    {
        currentCooldown = upgrade[0].Cooldown;
        Transform tmpBullet = Instantiate(bullet);
        if (Convert.ToInt32(towerType) != 4&& Convert.ToInt32(towerType) != 2)
        {
            tmpBullet.position = GunPoint[tier].position;
            tmpBullet.GetComponent<BulletFly>().damage = upgrade[tier].BulletDamage;
        }
        if (Convert.ToInt32(towerType) == 4&& Convert.ToInt32(towerType) == 2)
        {
            tmpBullet.position = enemy.position;
            tmpBullet.GetComponent<BulletFly>().damage = 0;
        }
        tmpBullet.GetComponent<BulletFly>().speed = upgrade[0].BulletSpeed;
        tmpBullet.GetComponent<BulletFly>().spawner = spawner;
        tmpBullet.GetComponent<BulletFly>().SetTarget(enemy, coefficient[tagIndex].Сoeficient);
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

enum TowerType
{
    Turrel = 1,
    Gause = 1,
    Tesla = 2,
    Rocket = 3,
    Slow = 4
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
    public EnemyType Tag;
    public float Сoeficient;
}

