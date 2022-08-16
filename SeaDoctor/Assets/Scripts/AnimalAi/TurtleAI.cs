using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class TurtleAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public float maxRange, minRange;
    public Image help;
    public Transform enemies;
    private Vector3 destination;
    private Vector3 lastVelocity;
    public Slider slider;

    public Transform selectedEnemy;

    public bool waiting;
    private float waitTime = 25f;
    private float waitCount = 0;

    private float count;
    private float timeOffset = 3f;

    // Start is called before the first frame update
    void Start()
    {
        maxRange = GameObject.Find("LEVEL").GetComponent<Level>().maxRange;
        minRange = GameObject.Find("LEVEL").GetComponent<Level>().minRange;
        //help.enabled = false;
        waiting = true;
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
        slider.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    void whenFriendly()
    {
        slider.value = waitCount / waitTime;
        if (enemies.childCount == 0)
        {
            agent.speed = 8f;
            agent.acceleration = 8f;
            count += Time.fixedDeltaTime;
            if (count > timeOffset)
            {
                destination = RandomDesination();
                count = 0;
            }
        }
        else
        {
            if (waiting)
            {
                agent.speed = 8f;
                agent.acceleration = 8f;

                waitCount += Time.deltaTime;
                if (waitCount >= waitTime)
                {
                    waiting = false;
                }

                count += Time.fixedDeltaTime;
                if (count > timeOffset)
                {
                    destination = RandomDesination();
                    count = 0;
                }

            }
            else
            {
                //find child
                if (!selectedEnemy && enemies.childCount > 0)
                {
                    selectedEnemy = enemies.transform.GetChild(0);
                    float distance = 5f;

                    for (int i = 0; i < enemies.transform.childCount; i++)
                    {
                        float temp = (enemies.transform.GetChild(i).transform.position - transform.position).magnitude;
                        if (temp < distance)
                        {
                            distance = temp;
                            selectedEnemy = enemies.transform.GetChild(i);
                        }
                    }
                }
                if (selectedEnemy)
                {
                    destination = selectedEnemy.transform.position;
                    agent.speed = 20f;
                    agent.acceleration = 20f;
                    if ((transform.position - selectedEnemy.position).magnitude <= 2f)
                    {
                        selectedEnemy.GetComponent<Enemy>().takeDamage(20);
                        waiting = true;
                        waitCount = 0;
                    }
                }
            }
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
