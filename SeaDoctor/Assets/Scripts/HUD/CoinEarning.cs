using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEarning : MonoBehaviour
{
    private RectTransform targetPos;
    private RectTransform rect;
    private float speed = 30f;
    private GameObject Player;
    public int coin;

    private void Start()
    {
        targetPos = GameObject.Find("Coin_HUD").GetComponent<RectTransform>();
        rect = GetComponent<RectTransform>();
        Player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        rect.localPosition += (targetPos.localPosition - (rect.localPosition + new Vector3(100f,0f,0f))).normalized * speed;
        if (Vector3.Distance(targetPos.localPosition, (rect.localPosition + new Vector3(100f, 0f, 0f))) < 100f)
        {
            Player.GetComponent<Player>().CoinAdding(coin);
            Destroy(gameObject);
        }
    }
}
