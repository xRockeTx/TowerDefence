using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UIElements;

public class BulletFly : MonoBehaviour
{
    [SerializeField] private TowerType towerType;
    public SpawnEnemy spawner;
    public int damage, speed;
    private Transform target;
    private float coef;

    private void Update()
    {
        Move();
    }
    public void SetTarget(Transform enemy, float typeNum)
    {
        target = enemy;
        coef = typeNum;
    }
    private void Move()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                switch (Convert.ToInt32(towerType))
                {
                    case 1:
                        target.GetComponent<WalkEnemy>().GetDamage(damage * coef);
                        break;
                    case 2:
                        goto case 1;
                    case 3:
                        List<Transform> enemies= new List<Transform>();
                        foreach (Transform enemy in spawner.allEnemy)
                        {
                            if (enemy != null)
                            {
                                float distance = Vector3.Distance(target.position, enemy.position);
                                if (distance < 2f)
                                {
                                    enemies.Add(enemy);
                                }
                            }
                        }
                        foreach(Transform enemy in enemies)
                        {
                            if(enemy!=null)
                                enemy.GetComponent<WalkEnemy>().GetDamage(damage);
                        }
                        target.GetComponent<WalkEnemy>().GetDamage(damage);
                        break;
                    case 4:
                        target.GetComponent<WalkEnemy>().ChangeSpeed(0.5f, 3f);
                        break;
                }
                Destroy(gameObject);
            }
            else
            {
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * speed);
            }
        }
        else
            Destroy(gameObject);
    }

}
