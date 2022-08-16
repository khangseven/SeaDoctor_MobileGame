using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerate : MonoBehaviour
{

    public GameObject trashes;
    public GameObject parent;
    public void randomEnemies(float range, float minRange, int trashAmount, List<GameObject> list)
    {

        minRange += 4;
        for (int i = 0; i < trashAmount; i++)
        {
            Vector2 randVec = new Vector2(Random.Range(-range, range), Random.Range(-range, range));
            if (Mathf.Abs(randVec.magnitude) < minRange)
            {
                //randVec = randVec* ((minRange + Random.Range(minRange, range-minRange-5f)) /randVec.magnitude);
                randVec = randVec * ((minRange + Random.Range(0, range - minRange)) / randVec.magnitude);
            }
            GameObject temp = Instantiate(trashes, new Vector3(randVec.x + transform.position.x, trashes.transform.position.y, randVec.y + transform.position.z), Random.rotation);
            //Debug.Log(trashes[k].transform.position.y);
            list.Add(temp);
            temp.transform.parent = parent.transform;
        }
    }
}
