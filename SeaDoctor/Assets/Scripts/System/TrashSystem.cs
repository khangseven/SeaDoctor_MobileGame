using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSystem : MonoBehaviour
{

    public GameObject[] trashes;
    public Material[] materials;
    public GameObject parent;


    public void randomTrash(float range,float minRange, int trashAmount,List<GameObject> list) { 
        
        for(int k=0; k< trashes.Length; k++){
            for(int i=0; i< trashAmount; i++){
                Vector2 randVec = new Vector2(Random.Range(-range,range),Random.Range(-range,range));
                if(Mathf.Abs(randVec.magnitude) < minRange){
                    //randVec = randVec* ((minRange + Random.Range(minRange, range-minRange-5f)) /randVec.magnitude);
                    randVec = randVec * ((minRange + Random.Range(0,range - minRange)) / randVec.magnitude);
                }
                GameObject temp = Instantiate(trashes[k],new Vector3(randVec.x + transform.position.x,trashes[k].transform.position.y,randVec.y + transform.position.z),Random.rotation);
                //Debug.Log(trashes[k].transform.position.y);
                list.Add(temp);
                temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                temp.transform.parent = parent.transform;
            }
        }
    }
}
