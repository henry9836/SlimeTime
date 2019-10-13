using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public float timer;
    public float spanwRate;
    public float spawnRateModifier;
    public float maxSpawnRate;
    public List<GameObject> powerups;
    public bool startdelay = false;
    public float startdelaytime;


    public enum POWERUPS
    {
        NULL,
        RAPIDFIRE,
        MULTISHOTT1,
        MULTISHOTT2,
        SPREAD,
        HEAL,
        WALLOFDEATH

    }


    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= startdelaytime)
        {
            startdelay = true;
        }

        if (timer >= spanwRate && startdelay == true)
        {
            if (spanwRate <= maxSpawnRate)
            {
                spanwRate = maxSpawnRate;
            }
            else
            {
                spanwRate *= spawnRateModifier;
            }
            timer = 0.0f;

            int tospawn = Random.Range(1, (sizeof(POWERUPS) - 1));

            StartCoroutine(GetComponent<slimeSpawner>().Spawnpowerup((POWERUPS)tospawn));
        }
    }

}
