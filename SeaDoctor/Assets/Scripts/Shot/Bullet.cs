using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform destination;
    public float Damage;

    public bool go=false;

    private void FixedUpdate()
    {
        if (!go) return;
        if (!destination)
        {
            DestroyImmediate(gameObject);
            return;
        }

        transform.position += (destination.position - transform.position).normalized*1.2f;
        //GetComponent<Rigidbody>().velocity = (destination.position - transform.position).normalized * 1.2f;


        if ((destination.position - transform.position).magnitude <= 1.5f)
        {
            destination.GetComponent<Enemy>().takeDamage(Damage);
            DestroyImmediate(gameObject);
        }

    }
}
