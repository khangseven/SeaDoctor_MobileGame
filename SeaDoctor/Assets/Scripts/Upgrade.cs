using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Player player;

    public Text speed;
    public Text collector;
    public Text volume;
    public Text power;

    public Text speedPrice;
    public Text collectorPrice;
    public Text volumePrice;
    public Text powerPrice;

    private int basicPrice = 150;

    private void FixedUpdate()
    {
        speed.text = "Speed " + player._speed;
        collector.text = "Collector " + player._collector;
        volume.text = "Volume " + player._volume;
        power.text = "Power " + player._gun;

        speedPrice.text = (player._speed + 1) * basicPrice + "";
        collectorPrice.text = (player._collector + 1) * basicPrice + "";
        volumePrice.text = (player._volume + 1) * basicPrice + "";
        powerPrice.text = (player._gun + 1) * basicPrice + "";
    }

    public void speedUp()
    {
        int price = (player._speed + 1) * basicPrice;
        if (player.coin >= price)
        {
            player.coin -= price;
            player._speed += 1;
        }

    }
    public void collectorUp()
    {
        int price = (player._collector + 1) * basicPrice;
        if (player.coin >= price)
        {
            player.coin -= price;
            player._collector += 1;
        }
    }

    public void volumeUp()
    {
        int price = (player._volume + 1) * basicPrice;
        if (player.coin >= price)
        {
            player.coin -= price;
            player._volume += 1;
            player.volumeUp();
        }
    }

    public void powerUp()
    {
        int price = (player._gun + 1) * basicPrice;
        if (player.coin >= price)
        {
            player.coin -= price;
            player._gun += 1;
        }
    }
}
