using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private float speed = 10f;
    private bool Go = false;

    public Transform boat;
    // Update is called once per frame
    void Update()
    {
        if (Go)
        {
            GetComponent<Rigidbody>().velocity = (boat.position - transform.position).normalized * speed;
            //var vel = transform.InverseTransformDirection((boat.position - transform.position).normalized * speed);
            //GetComponent<Rigidbody>().velocity = vel;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Go = true;
            Debug.Log(GameObject.Find("Container").transform.TransformPoint(Vector3.zero).ToString());
        }
    }
}
