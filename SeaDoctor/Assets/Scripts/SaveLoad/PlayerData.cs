using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
  
    public int speedLevel;
    public int volumeLevel;
    public int Level;
    

    public PlayerData(Player player)
    {
        speedLevel = (int)player.speed;
        volumeLevel = (int)player.volume;

    }
}
