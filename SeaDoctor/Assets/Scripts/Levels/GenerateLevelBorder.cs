using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevelBorder : MonoBehaviour
{
    public GameObject _float;
    public GameObject _beacon;
    public float range;
    private Vector3 pivot;
    private Vector3[] direction;
    private float _range;
    private List<GameObject> list = new List<GameObject>();
    private void Start()
    {
        _range = range;
        if (_range % 3 == 0)
        {
            _range += 4f;
        }
        else if (_range % 3 == 1)
        {
            _range += 3f;
        }
        else _range += 2f;
        pivot = new Vector3(transform.localPosition.x - _range, 0, transform.localPosition.z - _range);
        direction = new Vector3[]{
            new Vector3(3,0,0),
            new Vector3(0,3,90),
            new Vector3(-3,0,0),
            new Vector3(0,-3,90)
        };
        GenerateBorder();
    }

    public void GenerateBorder()
    {
        Debug.Log(_range * 2 / 3);
        for (int k=0; k < 4; k++)
        {
            pivot = new Vector3(pivot.x + direction[k].x, 0, pivot.z + direction[k].y);
            for (int i = 0; i < (int)(_range*2 / 3 ); i++)
            {
                GameObject temp = Instantiate(_float, transform);
                temp.transform.position = pivot;
                temp.transform.eulerAngles = new Vector3(0, direction[k].z, 0);
                pivot = new Vector3(pivot.x + direction[k].x, 0, pivot.z + direction[k].y);
                list.Add(temp);
            }
            GameObject temp2 = Instantiate(_beacon, transform);
            temp2.transform.position = pivot;
            list.Add(temp2);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
