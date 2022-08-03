using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrashSystem : MonoBehaviour
{

    public int trashAmount;
    public float range;
    public float minRange;
    public GameObject[] trashes;

    public GameObject parent;
   
    void Start()
    {
        randomTrash();
    }   

    private void randomTrash(){
        
        for(int k=0; k< trashes.Length; k++){
            for(int i=0; i< trashAmount; i++){
                Vector2 randVec = new Vector2(Random.Range(-range,range),Random.Range(-range,range));
                if(Mathf.Abs(randVec.magnitude) < minRange){
                    randVec= randVec* (minRange/randVec.magnitude);
                    Debug.Log(randVec.ToString());
                }
                GameObject temp = Instantiate(trashes[k],new Vector3(randVec.x,0.2f,randVec.y),trashes[k].transform.rotation);
                temp.isStatic = true;
                temp.transform.parent = parent.transform;
            }
        }
    }
}
