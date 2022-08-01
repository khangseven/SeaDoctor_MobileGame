using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour
{

    public GameObject JoystickBG;
    public GameObject JoystickCT;
    public Vector2 Velocity;

    private float radius;
    private Vector2 touchPosition;

    private void Start(){
        radius = JoystickBG.GetComponent<RectTransform>().sizeDelta.y/4;
        JoystickBG.active = false;
        JoystickCT.active = false;
    }

    public void OnPointerDown(){
        JoystickBG.transform.position = Input.mousePosition;
        JoystickCT.transform.position = Input.mousePosition;
        touchPosition = Input.mousePosition;
        JoystickBG.active = true;
        JoystickCT.active = true;
    }

    public void Drag(BaseEventData baseEventData){
        PointerEventData pointer = baseEventData as PointerEventData;
        Vector2 dragPosition = pointer.position;
        Velocity = (dragPosition - touchPosition).normalized;

        float distance = Vector2.Distance(dragPosition, touchPosition);

        if(distance > radius){
            JoystickCT.transform.position = touchPosition + Velocity * radius;
        }else{
            JoystickCT.transform.position = touchPosition + Velocity * distance;
        }
    }

    public void OnPointerUp(){
        Velocity = Vector2.zero;
        JoystickBG.active = false;
        JoystickCT.active = false;
    }
}
