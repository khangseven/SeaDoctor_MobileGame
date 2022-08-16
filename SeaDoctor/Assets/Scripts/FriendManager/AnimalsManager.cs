using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsManager : MonoBehaviour
{
    public List<Animals> animals;

    public Player player;

    private void Start()
    {
        int index = 0;
        foreach(bool i in player.friends)
        {
            animals[index].gameObject.SetActive(i);
            index++;
        }
    }
}
