using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public float maxRange, minRange;
    public Transform Player;
    public Slider slider;
    private Vector3 destination;
    private Vector3 lastVelocity;

    private float HP;
    private float currentHP;

    private float count;
    private float timeOffset = 3f;

    // Start is called before the first frame update
    void Start()
    {
        HP = 10;
        currentHP = 10;
        maxRange = GameObject.Find("LEVEL").GetComponent<Level>().maxRange;
        minRange = GameObject.Find("LEVEL").GetComponent<Level>().minRange;
        //help.enabled = false;
    }

    private void FixedUpdate()
    {
        whenFriendly();
        slider.value = currentHP / HP;
        slider.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    void whenFriendly()
    {
        count += Time.fixedDeltaTime;
        if (count > timeOffset)
        {
            destination = RandomDesination();
            count = 0;
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


    }

    private Vector3 RandomDesination()
    {
        float range = maxRange;
        Vector2 randVec = new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        if (Mathf.Abs(randVec.magnitude) < minRange)
        {
            randVec = randVec * ((minRange + Random.Range(0, range - minRange)) / randVec.magnitude);
        }
        return new Vector3(randVec.x, transform.position.y, randVec.y);
    }
}
