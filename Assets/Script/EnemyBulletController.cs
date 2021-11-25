using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public void EnemyShot(Vector3 direction,float shotSpeed)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(direction*shotSpeed);
    }
}
