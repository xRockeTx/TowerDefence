using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    [SerializeField] private int minHP, speed;
    [SerializeField] private Transform wayPointParent;
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
            }
        }
    }
    public void GetDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            FindObjectOfType<BuyOpen>().ChangeMoney(Convert.ToInt32(damage));
            Destroy(gameObject);
        }
    }
    public void SetWaypoints(Transform waypointsParent)
    {
        wayPointParent = waypointsParent;
    }
}
