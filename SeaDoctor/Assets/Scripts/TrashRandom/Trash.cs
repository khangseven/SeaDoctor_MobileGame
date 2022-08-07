using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public float mass;

    public float speed = 50f;
    public bool isCollected=false;
    public bool isCompleted=false;
    public Transform container; 
    public Vector3 localPos;
    public Vector3 factoryPos;
    public bool isFreeze = false;

    private bool highest=false;
    private float high = 5f;
    private float collectSpeed = 30f;

    private void Start()
    {
        container = GameObject.Find("Container").transform;
        factoryPos = GameObject.Find("Factory").transform.position;
        if (isFreeze)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        }
    }

    private void FixedUpdate()
    {
        if (isCompleted)
        {
            transform.parent = null;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            if (!highest)
            {
                GetComponent<Rigidbody>().velocity = Vector3.up * speed;
                if (transform.position.y >= high) highest = true;
            }
            else
            {
                GetComponent<Rigidbody>().velocity = (factoryPos - transform.position).normalized * collectSpeed;
                if (Vector3.Distance(transform.position, factoryPos) < 0.5f)
                {
                    DestroyImmediate(gameObject);
                    Debug.Log("Xoas");
                    //Tao ra coin earning
                    Vector3 coinPos = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(factoryPos);
                    GameObject.Find("CoinSystem").GetComponent<CoinSystem>().CreateCoin(coinPos, (int)mass);
                }
            }
        } 
        else if (isCollected)
        {
            if (!highest)
            {
                GetComponent<Rigidbody>().velocity = Vector3.up * speed;
                if (transform.position.y >= high) highest = true;
            }
            else
            {
                GetComponent<Rigidbody>().velocity = (container.TransformPoint(localPos) - transform.position).normalized * collectSpeed;
                //Debug.Log(Vector3.Distance(transform.position, container.TransformPoint(localPos)));
                if (Vector3.Distance(transform.position, container.TransformPoint(localPos)) < 0.5f)
                {
                    transform.position = container.TransformPoint(localPos);
                    //Debug.Log(transform.position.ToString()+container.position.ToString());
                    transform.parent = container;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    isCollected = false;
                    highest = false;
                }
            }
            //Debug.Log(transform.position.ToString()+container.position.ToString());
        }
        
    }
}
