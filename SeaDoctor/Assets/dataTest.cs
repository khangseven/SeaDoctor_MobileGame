using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Object.DontDestroyOnLoad(GameObject.Find("Player"));
        Object.DontDestroyOnLoad(GameObject.Find("Main Camera"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
