using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UIElements;

public class BulletFly : MonoBehaviour
{
    [SerializeField] private TowerType towerType;
    [SerializeField] private ParticleSystem particle;
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
        if(Convert.ToInt32(towerType) == 4)
            particle.gameObject.SetActive(true);
    }
    private void Move()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                switch (Convert.ToInt32(towerType))
                {
                    case 1:
                        target.GetComponent<WalkEnemy>().GetDamage(damage * coef);
                        Destroy(gameObject);
                        break;
                    case 2:
                        //target.GetComponent<WalkEnemy>().PlayParticle(particle);
                        goto case 1;
                    case 3:
                        List<Transform> enemies = new List<Transform>();
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
                        Destroy(gameObject);
                        break;
                    case 4:
                        Debug.Log("Do");
                        target.GetComponent<WalkEnemy>().ChangeSpeed(0.5f);
                        //target.GetComponent<WalkEnemy>().PlayParticle(particle);
                        Destroy(gameObject);
                        break;
                }
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
