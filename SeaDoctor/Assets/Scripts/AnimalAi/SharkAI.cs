using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SharkAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform desTrash;
    public Transform Trashes;
    public Transform sharkContainer;
    public float maxRange, minRange;
    public Transform factory;
    private Vector3 destination;
    private bool haveTrash = false;
    private Vector3 lastVelocity;
    private bool onPoint=false;

    private float count;
    private float timeOffset = 3f;

    public Image help;
    // Start is called before the first frame update
    void Start()
    {
        maxRange = GameObject.Find("LEVEL").GetComponent<Level>().maxRange;
        minRange = GameObject.Find("LEVEL").GetComponent<Level>().minRange;
        //help.enabled = false;
       
    }

    private void FixedUpdate()
    {
        if (gameObject.CompareTag("Animals"))
        {

        }
        else if (gameObject.CompareTag("Friendly"))
        {
            whenFriendly();
        }

        help.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    void whenFriendly()
    {
        if (onPoint && haveTrash)
        {
            desTrash.GetComponent<Trash>().isCompleted = true;
            haveTrash = false;
        }

        //Find Trash
        if (!haveTrash)
        {
            if (desTrash != null) // Co des
            {
                destination = new Vector3(desTrash.position.x, transform.position.y, desTrash.position.z);
            }
            else if (Trashes.transform.childCount != 0 && desTrash == null) // chua co des
            {
                float min = (Trashes.GetChild(0).position - transform.position).magnitude;
                desTrash = Trashes.transform.GetChild(0);
                for (int i = 1; i < Trashes.transform.childCount; i++)
                {
                    float tempTange = (Trashes.GetChild(i).position - transform.position).magnitude;
                    if (tempTange < min)
                    {
                        desTrash = Trashes.transform.GetChild(i);
                        min = tempTange;
                    }
                }
                destination = new Vector3(desTrash.position.x, transform.position.y, desTrash.position.z);
            }
            else // Het trash
            {
                count += Time.fixedDeltaTime;
                if (count > timeOffset)
                {
                    destination = RandomDesination();
                    count = 0;
                }
            }
        }
        else
        {
            destination = factory.position;
        }

        //Go to Des
        NavMeshHit hit;
        if (NavMesh.SamplePosition(destination, out hit, 1f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }


        if (agent.velocity.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity);
            lastVelocity = agent.velocity;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(lastVelocity);
        }

        if (desTrash && !haveTrash)
        {
            if (!desTrash.GetComponent<Collider>())
            {
                desTrash = null;
            }
        }

        //take trash
        if (desTrash != null)
        {
            if ((desTrash.position - transform.position).magnitude < 0.5f)
            {
                //Debug.Log("GET");
                haveTrash = true;
                GameObject obj = desTrash.gameObject;
                Destroy(obj.GetComponent<Collider>());
                obj.transform.parent = transform;
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                obj.GetComponent<Trash>().container = sharkContainer;
                obj.GetComponent<Trash>().localPos = Vector3.zero;
                obj.GetComponent<Trash>().isCollected = true;
                obj.GetComponent<Trash>().speed = 20f;
            }
        }
    }

    private Vector3 RandomDesination()
    {
        float range = maxRange;
        Vector2 randVec = new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        if (Mathf.Abs(randVec.magnitude) < minRange)
        {
            randVec = randVec * ((minRange + Random.Range(0, range - minRange)) / randVec.magnitude);
        }
        return new Vector3(randVec.x, transform.position.y ,randVec.y);
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("CheckPoint"))
        {
            onPoint = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("CheckPoint"))
        {
            onPoint = false;
        }
    }
}
