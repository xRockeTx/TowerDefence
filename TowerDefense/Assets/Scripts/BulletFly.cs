using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletFly : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private Transform target;
    private float coef;

    private void Update()
    {
        Move();
    }
    public void SetTarget(Transform enemy,float typeNum)
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
                target.GetComponent<WalkEnemy>().GetDamage(damage*coef);
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
