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
            float H = Random.Range(0f, 1f);
            float Hmin=H, Hmax=H;
            //Debug.Log(H);
            if(H > 0.45f && H < 0.85f)
            {
                if (H > 0.65f)
                {
                    Hmin = 0.85f;
                    Hmax = 1;
                }
                else
                {
                    Hmin = 0;
                    Hmax = 0.45f;
                }
            }
            //Debug.Log(Hmin + " " + Hmax);
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV(Hmin,Hmax,0.8f,1,0.5f,0.8f);
        }else
        {
            GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
