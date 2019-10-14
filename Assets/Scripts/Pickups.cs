using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public float timer;
    public float spanwRate;
    public float spawnRateModifier;
    public float maxSpawnRate;
    public GameObject powerup;
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


            int tospawn = Random.Range(1, (System.Enum.GetValues(typeof(POWERUPS)).Length));

            StartCoroutine(GetComponent<slimeSpawner>().Spawnpowerup((POWERUPS)tospawn));

        }
    }

}
