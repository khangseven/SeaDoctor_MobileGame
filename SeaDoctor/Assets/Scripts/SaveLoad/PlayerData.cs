using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public int LevelCompleted;
    public int _speed;
    public int _volume;
    public int _collector;
    public int _gun;
    public int coin;
    //public bool[] friends;

    public PlayerData(Player player)
    {
        LevelCompleted = player.levelCompleted;
        _speed = player._speed;
        _volume = player._volume;
        _collector = player._collector;
        _gun = player._gun;
        coin = player.coin;
        //friends = player.friends;
    }

    public PlayerData()
    {
        LevelCompleted = 0;
        _speed = 0;
        _volume = 0;
        _collector = 0;
        _gun = 0;
        coin = 0;
        //friends = new bool[2];
    }

    public void updatePlayer(Player player)
    {
        player.coin = coin;
        player._speed = _speed;
        player._volume = _volume;
        player._gun = _gun;
        player._collector = _collector;
        player.levelCompleted = LevelCompleted;
        //player.friends = friends;
    }
}
