using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{    
    public Camera mainCamera; 
    public JoystickController joystickController;

    public Vector2 CamHeight = new Vector2(30,25);

    private Rigidbody rbody;


    public float volume;
    public float currentVolume;
    public float speed = 10f;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        currentVolume = 0;
    }

    void Update()
    {
        if(joystickController.Velocity.magnitude > 0f){
            float angle = -Vector2.SignedAngle( Vector2.left,joystickController.Velocity);
            if(angle<0) angle = 360 + angle;
            transform.rotation = Quaternion.Euler(0,angle,0);            
        }
        rbody.velocity = new Vector3(-joystickController.Velocity.x*speed, 0 ,-joystickController.Velocity.y*speed);
        mainCamera.transform.position = transform.position + new Vector3(0,CamHeight.x,CamHeight.y);
    }
    
    void OnTriggerEnter(Collider col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "Trash")
        {
            Trash trash = obj.GetComponent<Trash>();
            if (currentVolume + trash.mass > volume)
            {
                Debug.Log("Full");
            }
            else
            {
                currentVolume += trash.mass;
                Destroy(obj);
            }
            
        }
    }
}
