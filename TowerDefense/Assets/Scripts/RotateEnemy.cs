using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : MonoBehaviour
{
    [SerializeField] private EnemyRotate rotation; 
    public void EnemyRotate(Transform target)
    {
        transform.LookAt(target,Vector3.up);
        transform.Rotate(rotation.x, rotation.y, rotation.z);
    }
}

[Serializable]
public class EnemyRotate
{
    public int x, y, z;
}
