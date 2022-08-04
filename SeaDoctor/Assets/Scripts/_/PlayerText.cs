using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : MonoBehaviour
{
    public Camera _cam;
    public Player player;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        GameObject obj = GameObject.Find("Volume");
        Text text = obj.GetComponent<Text>();
        if(player.volume - player.currentVolume < 0.5f)
        {
            text.text = "FULL";
        }
        else
        {
            text.text = Mathf.Floor((player.currentVolume * 100f / player.volume)) + "%";
        }
        
    }
}
