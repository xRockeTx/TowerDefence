﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerShoot : MonoBehaviour
{
    public List<int> upSpeed, upDamage, upgradePrice;
    public List<float> rangeTower, cooldownTower;
    [SerializeField] private List<string> tags;
    [SerializeField] private List<float> coef;
    private string enemyTag;
    public UpgradeTower upTower;
    public Transform bullet;
    public float range, cooldown, currentCooldown;
    public int price,tier=0,damage,speed;
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
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag(tag))
            {
                float currDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (currDistance < distance && currDistance <= range)
                {
                    nearestEnemy = enemy.transform;
                    distance = currDistance;
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
                upTower.OpenPan(price, upgradePrice,upSpeed,upDamage, rangeTower, cooldownTower, tier, this);
        }
    }
}
