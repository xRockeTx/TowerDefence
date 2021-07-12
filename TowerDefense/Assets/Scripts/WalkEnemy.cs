using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    enum EnemyType
    {
        OnlyWalk = 3,
        Fasted = 1
    }
    [SerializeField] private EnemyType type;
    [SerializeField] private int minHP, speed;
    [SerializeField] private Transform wayPointParent;
    [SerializeField] private RotateEnemy rotate;
    public Transform playerBase;
    public SpawnEnemy spawner;
    private List<Transform> waypoints = new List<Transform>();
    private int index = 0;
    private float hp = 15;
    private float hitPoint
    {
        get { return hitPoint; }
        set
        {
            if (value >= hitPoint)
            {
                Destroy(gameObject);
            }
            else
                hitPoint -= value;
        }
    }
    private void Start()
    {
        for(int i = 0; i < wayPointParent.childCount; i++)
        {
            waypoints.Add(wayPointParent.GetChild(i));
        }
        rotate.EnemyRotate(waypoints[index]);
    }
    private void Update()
    {
        if (waypoints.Count != index)
        {
            Vector3 dir = waypoints[index].position - transform.position;
            transform.Translate(dir.normalized*Time.deltaTime*speed);
            if (Vector3.Distance(transform.position,waypoints[index].position)<0.3f)
            {
                index++;
                if(index<waypoints.Count)
                    rotate.EnemyRotate(waypoints[index]);
            }
        }
        else if (Vector3.Distance(transform.position,playerBase.position)<=0.5f)
        {
            playerBase.GetComponent<PlayerBaseHp>().GetDamage(Convert.ToInt32(type));
            Destroy(gameObject);
        }
    }
    public void GetDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            FindObjectOfType<BuyOpen>().ChangeMoney(Convert.ToInt32(damage));
            spawner.allEnemy.Remove(this.transform);
            Destroy(gameObject);
        }
    }
    public void SetWaypoints(Transform waypointsParent)
    {
        wayPointParent = waypointsParent;
    }
}
