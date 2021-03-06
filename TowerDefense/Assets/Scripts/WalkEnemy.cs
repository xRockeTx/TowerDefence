using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    enum EnemyTypes
    {
        OnlyWalk = 3,
        Fasted = 1
    }
    enum EnemyMoneyPerKill
    {
        OnlyWalk = 5,
        FlyEnemy = 2
    }
    [SerializeField] private ParticleSystem Particle;
    [SerializeField] private EnemyMoneyPerKill money;
    [SerializeField] private EnemyTypes type;
    [SerializeField] private float minHP, speed;
    private float currentSpeed;
    [SerializeField] private Transform wayPointParent;
    [SerializeField] private RotateEnemy rotate;
    public Transform playerBase;
    public SpawnEnemy spawner;
    private List<Transform> waypoints = new List<Transform>();
    private int index = 0;
    [SerializeField] private float hp = 15;

    private void Start()
    {
        currentSpeed = speed;
        for (int i = 0; i < wayPointParent.childCount; i++)
        {
            waypoints.Add(wayPointParent.GetChild(i));
        }
        rotate.EnemyRotate(waypoints[index]);
        hp = minHP;
    }
    private void Update()
    {
        if (waypoints.Count != index)
        {
            Vector3 dir = waypoints[index].position - transform.position;
            transform.Translate(dir.normalized * Time.deltaTime * currentSpeed);
            if (Vector3.Distance(transform.position, waypoints[index].position) < 0.3f)
            {
                index++;
                if (index < waypoints.Count)
                    rotate.EnemyRotate(waypoints[index]);
            }
        }
        else if (Vector3.Distance(transform.position, playerBase.position) <= 1f)
        {
            playerBase.GetComponent<PlayerBaseHp>().GetDamage(Convert.ToInt32(type), transform);
            Destroy(gameObject);
        }
    }
    public void GetDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            FindObjectOfType<BuyOpen>().ChangeMoney(Convert.ToInt32(money));
            spawner.allEnemy.Remove(transform);
            Destroy(gameObject);
        }
    }
    public void ChangeSpeed(float speedCoef)
    {
        if (speed == currentSpeed)
            StartCoroutine(Speed(speedCoef));
    }
    private IEnumerator Speed(float speedCoef)
    {
        currentSpeed *= 0.5f;
        yield return new WaitForSeconds(1);
        currentSpeed /= 0.5f;
    }
    public void SetWaypoints(Transform waypointsParent)
    {
        wayPointParent = waypointsParent;
    }
    public void PlayParticle(ParticleSystem particle)
    {
        Particle = particle;
        Particle.gameObject.SetActive(true);
        Particle.Play();
    }
}
