using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Animals : MonoBehaviour
{
    public int ID;
    public string Name;
    public string Description;

    public Transform net;
    public Transform vfx;
    public Image help;

    public RectTransform failText;
    private float failTextCount;

    public RectTransform successText;
    private bool successOkay;

    public IEnumerator rescure(Action doLast)
    {
        
        DestroyImmediate(net.gameObject);
        DestroyImmediate(vfx.gameObject);
        
        successOkay = false;
        successText.GetChild(1).GetComponent<Text>().text = Description;
        successText.gameObject.SetActive(true);
        yield return new WaitUntil(() => successOkay == true);
        successOkay = false;
        successText.gameObject.SetActive(false);
        gameObject.tag = "Friendly";
        //GameObject.Find("Player").GetComponent<Player>().friends[ID]=true;
        doLast();
        //DestroyImmediate(gameObject);
    }

    public void rescureOkay()
    {
        successOkay = true;
    }

    public void cantRescure()
    {
        Debug.Log("Fail");
        failTextCount = 0;
        failText.gameObject.SetActive(true);
        failText.gameObject.GetComponent<Animator>().Play(0);

    }

    private void FixedUpdate()
    {
        //help toggle
        if (gameObject.CompareTag("Animals"))
        {
            help.gameObject.SetActive(true);
        }
        else
        {
            help.gameObject.SetActive(false);
        }
        

        //Faild
        if (failText.gameObject.activeSelf)
        {
            failTextCount += Time.fixedDeltaTime;

        }

        if(failTextCount > 3)
        {
            failText.gameObject.SetActive(false);
        }

        //Success

    }
}
