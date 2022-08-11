using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public float radius;
    public int points;
    public LineRenderer lineRenderer;

    void Start()
    {
        radius = GameObject.Find("LEVEL").GetComponent<Level>().minRange;
        Draw();
    }

    public void Draw()
    {
        lineRenderer.positionCount = points+1 ;
        for (int i = 0; i <= points; i++)
        {
            float radian = i / (float)points * Mathf.PI * 2;
            float x = Mathf.Cos(radian) * radius + transform.position.x;
            float y = Mathf.Sin(radian) * radius + transform.position.z;
            lineRenderer.SetPosition(i, new Vector3(x, transform.position.y, y));
        }
    }
}
