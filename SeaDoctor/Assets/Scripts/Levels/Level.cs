using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public int level;

    public float maxRange;
    public float minRange;
    public int trashAmount;
    public int enemiesAmount;

    public TrashSystem trashGenerate;
    public GenerateLevelBorder borderGenerate;
    public EnemiesGenerate EnemiesGenerate;

    public List<GameObject> trashes;
    public List<GameObject> enemies;

    public Text text1;
    public Text text2;
    public Text text3;

    public int missionType;

    private bool res = false;

    public Image NextLevle;

    private void Start()
    {
        borderGenerate.GenerateBorder(maxRange);
        trashGenerate.randomTrash(maxRange,minRange,trashAmount,trashes);
        EnemiesGenerate.randomEnemies(maxRange, minRange, enemiesAmount,enemies);

        if (missionType == 1)
        {
            text2.gameObject.SetActive(false);
            text3.gameObject.SetActive(false);
        }else if(missionType == 2)
        {
            text3.gameObject.SetActive(false);
        }
        else
        {

        }

    }

    private void FixedUpdate()
    {
        trashes.RemoveAll(GameObject => GameObject == null);
        enemies.RemoveAll(GameObject => GameObject == null);

        if (missionType == 1)
        {
            text1.text = "Clear all trashes! "+ trashes.Count + " left.";
        }
        else if (missionType == 2)
        {
            text1.text = "Clear all trashes! " + trashes.Count + " left.";
            text2.text = "Kill all enemies! " + enemies.Count + " left.";
        }
        else
        {
            text1.text = "Clear all trashes! " + trashes.Count + " left.";
            text2.text = "Kill all enemies! " + enemies.Count + " left.";
        }

        if (trashes.Count == 0)
        {
            text1.color = Color.green;
        }
        if (enemies.Count == 0)
        {
            text2.color = Color.green;
        }
        if(res) text3.color = Color.green;


        if (missionType == 1)
        {
            if (trashes.Count == 0) NextLevle.gameObject.SetActive(true);
        }
        else if (missionType == 2)
        {
            if (trashes.Count == 0 && enemies.Count == 0) NextLevle.gameObject.SetActive(true);
        }
        else
        {
            if (trashes.Count == 0 && enemies.Count == 0 && res==true) NextLevle.gameObject.SetActive(true);
        }
    }

    public void setRes()
    {
        res = true;
    }
}
