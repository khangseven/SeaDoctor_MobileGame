using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public float maxRange;
    public float minRange;
    public int trashAmount;

    public TrashSystem trashGenerate;
    public GenerateLevelBorder borderGenerate;

    private void Start()
    {
        borderGenerate.GenerateBorder(maxRange);
        trashGenerate.randomTrash(maxRange,minRange,trashAmount);
    }
}
