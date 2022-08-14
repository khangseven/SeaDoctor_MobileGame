using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiTest : MonoBehaviour
{
    public NavMeshAgent agent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            agent.SetDestination(new Vector3(100,0,100));
        }
    }
}
