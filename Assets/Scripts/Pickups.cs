using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public float timer;
    public float spanwRate;
    public float spawnRateModifier;
    public List<GameObject> powerups;


    public enum POWERUPS
    {
        test,
        test2, 
        pizzatime,

    }


    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= spanwRate && GetComponent<slimeSpawner>().onceSpawnng == true)
        {
            spanwRate *= spawnRateModifier;
            timer = 0.0f;

            Debug.Log("call");
            int tospawn = Random.Range(0, sizeof(POWERUPS));
            StartCoroutine(GetComponent<slimeSpawner>().Spawnpowerup((POWERUPS)tospawn));
        }
    }


    public void pew(POWERUPS temp)
    {
        if (temp == POWERUPS.test)
        {
            //put stuff here
        }
    }
}
