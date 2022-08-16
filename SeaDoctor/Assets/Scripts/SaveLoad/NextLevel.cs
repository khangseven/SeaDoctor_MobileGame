using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void next()
    {
        SaveLoad.Save(GameObject.Find("Player").GetComponent<Player>());

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void first()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void restart()
    {
        SaveLoad.Save2(new PlayerData());
        SceneManager.LoadScene("Loading");
    }

    public void load()
    {
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            PlayerData p = SaveLoad.Load();
            if ( p== null)
            {
                SaveLoad.Save2(new PlayerData());
                SceneManager.LoadScene("Intro");
            }
            else
            {
                if (p.LevelCompleted == 10)
                {
                    SceneManager.LoadScene("Outro");
                }
                else
                {
                    SceneManager.LoadScene((p.LevelCompleted + 1) + "");
                }
            }
        }
    }
}
