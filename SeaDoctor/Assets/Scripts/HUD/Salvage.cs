using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Salvage : MonoBehaviour
{

    public Image fill;
    public Image pivot;
    public Image slider;

    public float range = 350f;
    public float sliderSize;
    private float offset = 0.3f;
    private float minRange, maxRange;
    private float sliderVelocity;

    public float volume;
    private float currentVolume=0;
    private float volumeOffset = 0.08f;

    private float pivotVelocity;
    private float pivotRange = 320f;
    private float pivotDelayTime = 0.1f;
    private float pivotCount;

    private int pivotBigCount=0;
    private float pivotBigDelay = 20;

    private bool isPointing;

    private bool start=false;
    public RectTransform preSalvage;
    public RectTransform panel;



    public void setup()
    {
        slider.rectTransform.sizeDelta = new Vector2(slider.rectTransform.sizeDelta.x, sliderSize);
        minRange = -range + sliderSize/2 ;
        maxRange = range - sliderSize/2;
        currentVolume = 0;
        zeroCount = 0;
        pivotBigCount = 0;
        pivotCount = 0 ;
        currentVolume = 0;
        sliderVelocity=0;
        pivotVelocity=0;
    }

    // Start is called before the first frame update
    void Start()
    {
        setup();
    }

    private void OnEnable()
    {
        setup();
    }

    public void onPointerDown()
    {
        isPointing = true;
        sliderVelocity += 40f;
        if (sliderVelocity >= 180f)
        {
            sliderVelocity = 180f;
        }
    }

    public void onPointerUp()
    {
        isPointing = false;
    }

    public void Play()
    {
        preSalvage.gameObject.SetActive(false);
        start = true;
    }

    private float zeroCount;

    private void FixedUpdate()
    {
        if (!start) return;

        pivotCount += Time.fixedDeltaTime;
        if(pivotCount > pivotDelayTime)
        {
            pivotCount = 0;
            pivotBigCount++;
            if (pivotBigCount >= pivotBigDelay)
            {
                pivotVelocity = Random.Range(-250, 250);
                if(pivotBigCount>= pivotBigDelay + 5)
                {
                    pivotBigCount = 0;
                }
            }else
                pivotVelocity = Random.Range(-10, 10);
            float temp = pivot.rectTransform.anchoredPosition.y + pivotVelocity;
            if (temp > pivotRange) temp = pivotRange;
            else if (temp < -pivotRange) temp = -pivotRange;
            pivot.rectTransform.anchoredPosition = new Vector2(pivot.rectTransform.anchoredPosition.x, temp);
        }

        sliderVelocity -= 5f;
        if(sliderVelocity<-5f) sliderVelocity = -5f;

        float sliderTemp = slider.rectTransform.anchoredPosition.y + sliderVelocity;

        if (sliderTemp > maxRange)
        {
            sliderTemp = maxRange;
        }
        else if (sliderTemp < minRange)
        {
            sliderTemp = minRange;
        }

        slider.rectTransform.anchoredPosition = new Vector2(slider.rectTransform.anchoredPosition.x, sliderTemp);


        if(Mathf.Abs(pivot.rectTransform.anchoredPosition.y - slider.rectTransform.anchoredPosition.y) < sliderSize / 2)
        {
            currentVolume += volumeOffset + 0.1f;
        }else currentVolume -= volumeOffset;

        if (currentVolume > volume)
        {
            currentVolume = volume;
        }
        if (currentVolume < 0)
        {
            currentVolume = 0;
        }
        GetComponent<Slider>().value = currentVolume/ volume;


        if (currentVolume == 0)
        {
            zeroCount += Time.fixedDeltaTime;
        }
        else
        {
            zeroCount = 0;
        }

        if (zeroCount > 4)
        {
            start = false;
            panel.gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<Player>().helpComplete(false);
            preSalvage.gameObject.SetActive(true);
        }

        if(currentVolume == volume)
        {
            
            start = false;
            panel.gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<Player>().helpComplete(true);
            GameObject.Find("LEVEL").GetComponent<Level>().setRes();
            preSalvage.gameObject.SetActive(true);
        }
    }
}
