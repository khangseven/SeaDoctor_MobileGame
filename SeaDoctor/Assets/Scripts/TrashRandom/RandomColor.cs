using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{

    public Color color;
    public bool random;

    void Start()
    {
        if (random)
        {
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0,1,0.8f,1,1,1);
        }else
        {
            GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
