using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerShoot : MonoBehaviour
{

    public List<Upgrade> upgrade;
    [SerializeField] private TowerType towerType;
    [SerializeField] private List<CoefficientForEnemy> coefficient;
    [SerializeField] private List<string> tags;
    [SerializeField] private List<float> coef;
    [SerializeField] private List<Transform> GunPoint, TurelRotate;
    public SpawnEnemy spawner;
    private string enemyTag;
    public UpgradeTower upTower;
    public Transform bullet;
    public float range, cooldown, currentCooldown;
    public int price,tier=0,damage,speed;
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
                Transform nearestEnemy = null;
                foreach (string tag in tags)
                {
                    foreach (Transform enemy in spawner.allEnemy)
                    {
                        if (enemy != null)
                        {
                            if (enemy.CompareTag(tag))
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
                }
                if (nearestEnemy != null)
                {
                    enemy = nearestEnemy;
                    Shoot(nearestEnemy, tags.IndexOf(enemyTag, 0));
                }
                break;
            case 2:
                foreach (string tag in tags)
                {
                    if (enemies.Count != 0)
                    {
                        foreach (Transform enemy in enemies)
                        {
                            Shoot(enemy,tags.IndexOf(tag,0));
                        }
                    }
                    foreach (Transform enemy in spawner.allEnemy)
                    {
                        if (enemy != null)
                        {
                            if (enemy.CompareTag(tag))
                            {
                                float currDistance = Vector3.Distance(transform.position, enemy.position);
                                if (currDistance < distance && currDistance <= range)
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
        }

    }
    private void Shoot(Transform enemy, int tagIndex)
    {
        currentCooldown = cooldown;
        Transform tmpBullet = Instantiate(bullet);
        tmpBullet.position = GunPoint[tier].position;
        tmpBullet.GetComponent<BulletFly>().damage = damage;
        tmpBullet.GetComponent<BulletFly>().speed = speed;
        tmpBullet.GetComponent<BulletFly>().spawner = spawner;
        tmpBullet.GetComponent<BulletFly>().SetTarget(enemy, coef[tagIndex]);
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
    Normal = 1,
    Strong = 1,
    Tesla = 2,
    Rocket = 3
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
    public float Сoeficient;
}

