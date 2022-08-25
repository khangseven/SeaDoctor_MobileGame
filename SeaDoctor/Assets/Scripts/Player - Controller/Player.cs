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
    public Transform collector;
    public ItemsGenerate ItemsGenerate;
    public float volume;
    public float currentVolume;
    public float speed = 10f;

    public GameObject bullet;
    public Transform gun;
    public Transform gunHead;
    public Transform gunAnchor;
    public Transform enemies;

    private float shotDelay = 0.5f;
    private float shotCount = 0f;
    private float gunDamage = 3f;

    public GameObject boatContainer;
    public List<GameObject> trashes;

    public int coin=999;
    public Text coinText;
    public Image coinBackground;
    public Slider volumeUI;
    public Image fullImage;

    public Image img1;
    public Image img2;
    public Image img3;

    public float xRotationMax = 30f;

    public float containerRadius;
    private float torqueSpeed = 0.1f;
    private float trashHeight = 0;
    private int trashCount = 0;
    private int trashRemoveCount = 0;

    public bool onCheckPoint = false;
    private float delayTime = 0.05f;
    private float delayCount = 0f;

    private bool animalsHelp=false;
    private GameObject animal;
    private bool animalsHelpCallback = false;
    private bool animalsTextCallback = false;
    private bool animalsResultCallback = false;
    private bool oneTimeAnimal = false;

    public RectTransform animalText;
    public RectTransform Salvage;

    public int _speed;
    public int _collector;
    public int _gun;
    public int _volume;
    public bool[] friends;
    public int levelCompleted;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        currentVolume = 0;
        trashes = new List<GameObject>();
        
        SaveLoad.Load().updatePlayer(this);
        levelCompleted = GameObject.Find("LEVEL").GetComponent<Level>().level;
        volume += 25 * _volume;
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
            rbody.velocity = new Vector3(-joystickController.Velocity.x * (speed + _speed) *(item1 ? 2 : 1), 0, -joystickController.Velocity.y * (speed + _speed) * (item1 ? 2 : 1));
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
        shotCount += Time.fixedDeltaTime;
        if (enemies.childCount == 0) return;
        //find Near
        Transform selectedEnemy = null;
        float distance = item3 ? 10f : 5f;
        
        for (int i=0;i< enemies.transform.childCount;i++)
        {
            float temp = (enemies.transform.GetChild(i).transform.position - transform.position).magnitude;
            if (temp < distance)
            {
                distance = temp;
                selectedEnemy = enemies.transform.GetChild(i);
            }
        }

        if (!selectedEnemy) return;

        //rotate

        float angle = Vector2.SignedAngle(new Vector2(gun.transform.position.x - gunAnchor.position.x, gun.transform.position.z - gunAnchor.position.z)
            , new Vector2(gun.transform.position.x - selectedEnemy.position.x, gun.transform.position.z - selectedEnemy.position.z));
        gun.eulerAngles = new Vector3(0,-angle + transform.eulerAngles.y, 0);

        //shot

        
        if (shotCount > shotDelay)
        {
            shotCount = 0;
            Instantiate(bullet,gunHead.position,bullet.transform.rotation,null);
            
            bullet.GetComponent<Bullet>().destination = selectedEnemy;
            bullet.GetComponent<Bullet>().Damage = item3 ? (gunDamage + _gun) *2 : gunDamage + _gun;
            bullet.GetComponent<Bullet>().go = true;
        }

    }

    private float itemTime1 = 15f;
    private float itemTime2 = 15f;
    private float itemTime3 = 15f;

    private float itemCount1 = 0f;
    private float itemCount2 = 0f;
    private float itemCount3 = 0f;

    public bool item1 = false;
    public bool item2 = false;
    public bool item3 = false;

    private void gotItem(int i)
    {
        if (i == 1)
        {
            item1 = true;
            itemCount1 = 0;
        }else if (i == 2)
        {
            item2 = true;
            itemCount2 = 0;
        }
        else
        {
            item3 = true;
            itemCount3 = 0;
        }
    }

    private void itemTimer()
    {
        if (item1)
        {
            itemCount1 += Time.fixedDeltaTime;
            if(itemCount1>= itemTime1)
            {
                item1 = false;
                itemCount1 = 0;
            }
        }
        if (item2)
        {
            itemCount2 += Time.fixedDeltaTime;
            if (itemCount2 >= itemTime2)
            {
                item2 = false;
                itemCount2 = 0;
            }
        }
        if (item3)
        {
            itemCount3 += Time.fixedDeltaTime;
            if (itemCount3 >= itemTime3)
            {
                item3 = false;
                itemCount3 = 0;
            }
        }
    }

    

    private void FixedUpdate()
    {
        if (item1) img1.gameObject.SetActive(true);
        else img1.gameObject.SetActive(false);
        if (item2) img2.gameObject.SetActive(true);
        else img2.gameObject.SetActive(false);
        if (item3) img3.gameObject.SetActive(true);
        else img3.gameObject.SetActive(false);

        itemTimer();
        if (item2)
        {
            collector.transform.localScale = new Vector3(2.994882f, 1, (1 + 0.2f * _collector) *3);
        }
        else
        {
            collector.transform.localScale = new Vector3(2.994882f, 1, 1 + 0.2f * _collector);
        }
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
        if (obj.CompareTag("Items"))
        {
            Debug.Log("item");
            gotItem(obj.GetComponent<Items>().type);
            Destroy(obj);
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

    private float itemsRandomCount = 0f;
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

                itemsRandomCount++;
                if(itemsRandomCount > 80)
                {
                    ItemsGenerate.randomItem();
                    itemsRandomCount = 0;
                }
            }

        }
    }

    public void volumeUp()
    {
        volume += 25 * _volume;
    }
}
