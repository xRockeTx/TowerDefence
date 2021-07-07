using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : MonoBehaviour
{
    public void EnemyRotate(Transform target)
    {
        transform.LookAt(target,Vector3.up);
        transform.Rotate(90, 0, 0);
    }
}
