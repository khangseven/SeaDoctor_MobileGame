using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public GameObject coin;
    public GameObject parent;

    public void CreateCoin(Vector3 pos, int value)
    {
        GameObject temp = Instantiate(coin, parent.GetComponent<RectTransform>(),false);
        
        temp.GetComponent<RectTransform>().position = pos;
        temp.GetComponent<CoinEarning>().coin = value;
    }
}
