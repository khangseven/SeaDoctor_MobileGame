using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsGenerate : MonoBehaviour
{
    public GameObject[] trashes;

    public GameObject parent;
    public float maxRange;
    public float minRange;

    private void Start()
    {
        maxRange = GameObject.Find("LEVEL").GetComponent<Level>().maxRange;
        minRange = GameObject.Find("LEVEL").GetComponent<Level>().minRange;
    }

    public  void randomItem()
    {
        float range = maxRange-5;
        int i = Mathf.FloorToInt(Random.Range(0, 3));

        Vector2 randVec = new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        if (Mathf.Abs(randVec.magnitude) < minRange)
        {
            //randVec = randVec* ((minRange + Random.Range(minRange, range-minRange-5f)) /randVec.magnitude);
            randVec = randVec * ((minRange + Random.Range(0, range - minRange)) / randVec.magnitude);
        }
        GameObject temp = Instantiate(trashes[i], new Vector3(randVec.x + transform.position.x, trashes[i].transform.position.y, randVec.y + transform.position.z), trashes[i].transform.rotation);
        //Debug.Log(trashes[k].transform.position.y);
        temp.transform.parent = parent.transform;
    }


    public  void crateAtPosition(Vector3 pos)
    {
        int i = Mathf.FloorToInt(Random.Range(0, 3));
        GameObject temp = Instantiate(trashes[i], pos, trashes[i].transform.rotation);
        //Debug.Log(trashes[k].transform.position.y);
        temp.transform.parent = parent.transform;
    }
}
