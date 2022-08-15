using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public JoystickController joystickController;

    public Camera mainCamera;
    public Vector2 CamHeight = new Vector2(30, 25);
    private Rigidbody rbody;

    public float volume;
    public float currentVolume;
    public float speed = 10f;

    public GameObject bullet;
    public Transform gun;
    public Transform gunHead;
    public Transform enemies;

    public GameObject boatContainer;
    public List<GameObject> trashes;

    public int coin=999;
    public Text coinText;
    public Image coinBackground;
    public Slider volumeUI;
    public Image fullImage;

    public float xRotationMax = 30f;

    public float containerRadius;
    private float torqueSpeed = 0.1f;
    private float trashHeight = 0;
    private int trashCount = 0;
    private int trashRemoveCount = 0;

    public bool onCheckPoint = false;
    private float delayTime = 0.1f;
    private float delayCount = 0f;

    private bool animalsHelp=false;
    private GameObject animal;
    private bool animalsHelpCallback = false;
    private bool animalsTextCallback = false;
    private bool animalsResultCallback = false;
    private bool oneTimeAnimal = false;

    public RectTransform animalText;
    public RectTransform Salvage;
    


    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        currentVolume = 0;
        trashes = new List<GameObject>();

        SaveLoad.Save(this);
        Debug.Log(Application.persistentDataPath);
    }

    public void CoinAdding(int value)
    {
        coin += value;
        coinText.GetComponent<Animator>().Play(0);
    }

    void Update()
    {
        //move and rotate
        if (!animalsHelp)
        {
            if (joystickController.Velocity.magnitude > 0f)
            {
                float angle = -Vector2.SignedAngle(Vector2.left, joystickController.Velocity);
                if (angle < 0) angle = 360 + angle;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, 0);
            }
            rbody.velocity = new Vector3(-joystickController.Velocity.x * speed, 0, -joystickController.Velocity.y * speed);
        }
    }

    private void LateUpdate()
    {
        //transfrom camera
        CamHeight = mainCamera.GetComponent<CameraFollow>()._offset;
        mainCamera.transform.position = transform.position + new Vector3(0, CamHeight.x, CamHeight.y);
        if (animalsHelp)
        {
            mainCamera.transform.position = animal.transform.position + new Vector3(0, 15, 10);
            mainCamera.transform.eulerAngles = mainCamera.GetComponent<CameraFollow>()._defaultAngle;
        }
        //mainCamera.transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        //mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, transform.position + new Vector3(0, CamHeight.x, CamHeight.y), ref camVelocity, 0.25f);
       
    }

    private void Guning()
    {
        if (enemies.childCount == 0) return;
        //find Near
        Transform selectedEnemy = null;
        float distance = (enemies.transform.GetChild(0).transform.position - transform.position).magnitude;
        selectedEnemy = enemies.transform.GetChild(0);
        
        for (int i=1;i< enemies.transform.childCount;i++)
        {
            float temp = (enemies.transform.GetChild(i).transform.position - transform.position).magnitude;
            if (temp < distance)
            {
                distance = temp;
                selectedEnemy = enemies.transform.GetChild(i);
            }
        }
        //rotate
        Quaternion q = Quaternion.LookRotation(selectedEnemy.position - gun.position);
        if(selectedEnemy.position.z < gun.position.z)
        {
            q = q * Quaternion.Euler(0, 90, 0);
        }
        else
        {
            q = q * Quaternion.Euler(0, -180, 0);
        }
        gun.rotation = new Quaternion(gun.rotation.x, q.y ,gun.rotation.z, gun.rotation.w);
        //shot
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

        //Aniamals help

        if (animalsHelp && oneTimeAnimal)
        {
            oneTimeAnimal = false;
            StartCoroutine(animalHelp());
        }

        //Simulator waving
        /* if (transform.eulerAngles.x + torqueSpeed >= xRotationMax && transform.eulerAngles.x <= xRotationMax)
         {
             torqueSpeed = torqueSpeed * -1f;
         }else if (transform.eulerAngles.x + torqueSpeed <= 360- xRotationMax && transform.eulerAngles.x >= 360 - xRotationMax)
         {
             torqueSpeed = torqueSpeed * -1f;
         }
         else
         {
             transform.eulerAngles = new Vector3(transform.eulerAngles.x + torqueSpeed, transform.eulerAngles.y, transform.eulerAngles.z);
         }*/

        #region UI Update
        //update coin
        coinText.text = coin + "";

        //update volume ui
        volumeUI.value = currentVolume / volume;

        //Full Warning
        if (fullImage.enabled && currentVolume < volume)
        {
            fullImage.enabled = false;
        }
        else if(!fullImage.enabled && currentVolume == volume)
        {
            fullImage.enabled = true;
        }
        fullImage.transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);

        #endregion

        Guning();
    }

    public void playerHelpClick()
    {
        animalsTextCallback = true;
    }

    public void helpComplete(bool rs)
    {
        animalsResultCallback = rs;
        animalsHelpCallback = true;
    }

    private IEnumerator animalHelp()
    {
        animalText.gameObject.SetActive(true);
        yield return new WaitUntil(() => animalsTextCallback == true);
        animalsTextCallback = false;
        animalText.gameObject.SetActive(false);
        Salvage.gameObject.SetActive(true);
        yield return new WaitUntil(() => animalsHelpCallback == true);
        animalsHelpCallback = false;
        Debug.Log(animalsResultCallback);
        if (!animalsResultCallback)
        {
            //Fail
            animalsHelp = false;
            animal.GetComponent<Animals>().cantRescure();
            animal = null;
        }
        else
        {
            //Success
            Debug.Log("thang");
            StartCoroutine(animal.GetComponent<Animals>().rescure(doLast));
            //animal = null;
            //animalsHelp = false;
        }
    }

    public void doLast()
    {
        animal = null;
        animalsHelp = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("CheckPoint"))
        {
            onCheckPoint = true;
        }
        else if(obj.CompareTag("Animals"))
        {
            animalsHelp = true;
            oneTimeAnimal = true;
            animal = obj;
            rbody.velocity = Vector3.zero;
            obj.transform.Find("Animals_Canvas").GetChild(0).GetComponent<Image>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("CheckPoint"))
        {
            onCheckPoint = false;
        }
        else if (obj.CompareTag("Animals"))
        {
            animalsHelp = false;
            obj.transform.Find("Animals_Canvas").GetChild(0).GetComponent<Image>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("Trash") && col.contacts[0].thisCollider.name == "Collector")
        {
            Trash trash = obj.GetComponent<Trash>();
            if (currentVolume + trash.mass > volume)
            {
                
            }
            else
            {
                currentVolume += trash.mass;
                Destroy(obj.GetComponent<Collider>());
                trashes.Add(obj);
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                obj.GetComponent<Trash>().localPos = new Vector3(Random.Range(-containerRadius, containerRadius), trashHeight, Random.Range(-containerRadius, containerRadius));
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
