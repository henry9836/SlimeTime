using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int remainingSpawn;
    public float grace = 5.0f;
    public float gracetimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingSpawn == 0) // no slimes left (need to manage that here)
        {
            gracetimer -= Time.deltaTime;
            if (gracetimer <= 0.0f)
            {
                gracetimer = grace;
                GameObject.Find("GameManager").GetComponent<slimeSpawner>().timer = 0.0f;
                GameObject.Find("GameManager").GetComponent<slimeSpawner>().isSpawning = true;
                GameObject.Find("GameManager").GetComponent<slimeSpawner>().onceSpawnng = true;
            }
        }
    }
}


