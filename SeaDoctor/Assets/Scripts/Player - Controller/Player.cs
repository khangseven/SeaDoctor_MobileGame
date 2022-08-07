using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public JoystickController joystickController;

    public Camera mainCamera;
    public Vector2 CamHeight = new Vector2(30, 25);
    private Vector3 camVelocity = Vector3.zero;

    private Rigidbody rbody;

    public float volume;
    public float currentVolume;
    public float speed = 10f;
    public GameObject boatContainer;
    public List<GameObject> trashes;

    public int coin=999;
    public Text coinText;
    public Image coinBackground;

    public float xRotationMax = 30f;

    private float torqueSpeed = 0.07f;
    private float trashHeight = 0;
    private int trashCount = 0;
    private int trashRemoveCount = 0;

    public bool onCheckPoint = false;
    private float delayTime = 0.1f;
    private float delayCount = 0f;


    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        currentVolume = 0;
        trashes = new List<GameObject>();
    }

    public void CoinAdding(int value)
    {
        coin += value;
        coinText.GetComponent<Animator>().Play(0);
    }

    void Update()
    {
        //move and rotate
        if (joystickController.Velocity.magnitude > 0f) {
            float angle = -Vector2.SignedAngle(Vector2.left, joystickController.Velocity);
            if (angle < 0) angle = 360 + angle;
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, 0);
        }
        rbody.velocity = new Vector3(-joystickController.Velocity.x * speed, 0, -joystickController.Velocity.y * speed);
        }

    private void LateUpdate()
    {
        //transfrom camera
        mainCamera.transform.position = transform.position + new Vector3(0, CamHeight.x, CamHeight.y);
        //mainCamera.transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        //mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position + new Vector3(0, CamHeight.x, CamHeight.y), ref camVelocity, 0.25f);

    }

    private void FixedUpdate()
    {
        //On check point = true => Tra hang
        if (onCheckPoint)
        {
            if (trashes.Count > 0 && delayCount == 0)
            {
                trashes[trashes.Count-1].GetComponent<Trash>().isCompleted = true;
                currentVolume -= trashes[trashes.Count - 1].GetComponent<Trash>().mass;
                if (currentVolume < 0) currentVolume = 0;
                trashes.RemoveAt(trashes.Count - 1);
                trashRemoveCount++;
                if (trashRemoveCount > 5)
                {
                    trashRemoveCount = 0;
                    trashHeight -= .3f;
                }
            }
            delayCount += Time.fixedDeltaTime;
            if (delayCount > delayTime) delayCount = 0;
        }
        //Simulator waving
        if (transform.eulerAngles.x + torqueSpeed >= xRotationMax && transform.eulerAngles.x <= xRotationMax)
        {
            torqueSpeed = torqueSpeed * -1f;
        }else if (transform.eulerAngles.x + torqueSpeed <= 360- xRotationMax && transform.eulerAngles.x >= 360 - xRotationMax)
        {
            torqueSpeed = torqueSpeed * -1f;
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x + torqueSpeed, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        //update coin
        coinText.text = coin + "";
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "CheckPoint")
        {
            onCheckPoint = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        GameObject obj = col.gameObject;
        if (obj.tag == "CheckPoint")
        {
            onCheckPoint = false;
        }
    }

    private void OnCollisionEnter(Collision col)
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
                Destroy(obj.GetComponent<Collider>());
                trashes.Add(obj);
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                obj.GetComponent<Trash>().localPos = new Vector3(Random.Range(-2f, 2f), trashHeight, Random.Range(-2f, 2f));
                obj.GetComponent<Trash>().isCollected = true;
                obj.GetComponent<Trash>().speed = speed + 5f ; 
                //obj.transform.parent = boatContainer.transform;
                //obj.transform.localPosition = new Vector3(Random.Range(-2f, 2f), trashHeight, Random.Range(-2f, 2f));
                trashCount++;
                if (trashCount > 5)
                {
                    trashCount = 0;
                    trashHeight += .3f;
                }
            }

        }
    }
}
