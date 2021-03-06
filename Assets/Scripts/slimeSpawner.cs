﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeSpawner : MonoBehaviour
{
    public float timer = 0.0f;
    public List<GameObject> slimePrefab;
    public int toSpawn;
    public int haveSpawn;
    public bool isSpawning = true;

    public GameObject radtest;
    public bool radTest = false;

    public bool isdone = false; 

    private float stage1;
    public float stage1time;
    public float stage1timer;
    private float stage1Delay;
    private float stage2;
    public float stage2time;
    public float stage2timer;
    private float stage2Delay;

    private bool onceRAD = true;
    public bool onceSpawnng = true;
    private bool onceLAZER = true;
    public GameObject LAZERNODEprefab;
    public float LAZERNODEheight;
    public Vector2 LAZERNODEdetail;
    public Vector2 startXYpos;
    public Vector2 finishXYpos;

    public Vector2 diffCentreXY;
    public Vector3 spawnSaftyXY;

    public List<Vector4> ValidPositionsFIRE;
    public List<Vector4> ValidPositionsICE;
    public List<Vector4> ValidPositionsNORMAL;
    public List<Vector4> InvalidPositions;
    public List<Vector4> TempInvalidFIRE;
    public List<Vector4> TempInvalidICE;
    public List<Vector4> TempInvalidNORMAL;

    public List<GameObject> players = new List<GameObject>();
    public GameObject[] playerList;

    public Vector2 jumpCooldown = new Vector2(2.0f, 3.0f);
    public float health = 0;

    public float MAXslimes = 50;

    public void NextWave(int wave)
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        players.Clear();

        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].GetComponent<PlayerController>().controllerNotBound == false)
            {
                players.Add(playerList[i]);
            }
        }

        toSpawn = wavetospawnammount(wave, players.Count);

        stage1time = (0.05f * (float)Mathf.Pow(wave, 2) + 10f) / 2.0f;
        stage2time = (0.05f * (float)Mathf.Pow(wave, 2) + 10f);

        //untested
        jumpCooldown = new Vector2(Mathf.Exp(-(wave + 1 / 6)) * 5 , Mathf.Exp(-(wave / 6)) * 5);

        if (wave <= 4)
        {
            health = wave;
        }
        else
        {
            health += Mathf.CeilToInt(Mathf.Pow(wave , 2) * 0.12f) + 2;
        }


        onceSpawnng = true;
        timer = 0.0f;
        isSpawning = true;
        haveSpawn = 0;
        GameObject.Find("GameManager").GetComponent<GameManager>().remainingSpawn = toSpawn;
        stage1timer = 0.0f;
        stage2timer = 0.0f;


    }



    void Update()
    {

        if (onceLAZER == true)
        {
            onceLAZER = false;
            StartCoroutine(LAZERscan());
        }

        int pointcount = Mathf.FloorToInt((finishXYpos.y - startXYpos.y) / LAZERNODEdetail.y) * Mathf.FloorToInt((finishXYpos.x - startXYpos.x) / LAZERNODEdetail.x);

        if (ValidPositionsFIRE.Count + ValidPositionsICE.Count + ValidPositionsNORMAL.Count + InvalidPositions.Count + TempInvalidFIRE.Count + TempInvalidICE.Count + TempInvalidNORMAL.Count == pointcount)
        {
            isdone = true;
            InvalidPositions.Clear();
        }   

        if (isdone == true && isSpawning == true)
        {
            if (onceRAD == true)
            {
                onceRAD = false;
                //initial calculations

                for (int i = 0; i < ValidPositionsFIRE.Count; i++)
                {
                    float temp = Mathf.Sqrt(((Mathf.Pow(ValidPositionsFIRE[i].x - diffCentreXY.x, 2)) + (Mathf.Pow(ValidPositionsFIRE[i].z - diffCentreXY.y, 2))));
                    ValidPositionsFIRE[i] = new Vector4(ValidPositionsFIRE[i].x, ValidPositionsFIRE[i].y, ValidPositionsFIRE[i].z, temp);

                    if (radTest == true)
                    {
                        GameObject test = Instantiate(radtest, new Vector3(ValidPositionsFIRE[i].x, ValidPositionsFIRE[i].y, ValidPositionsFIRE[i].z), Quaternion.identity);
                        test.transform.localScale = new Vector3(0.1f, (ValidPositionsFIRE[i].w), 0.1f);
                    }

                }
                for (int i = 0; i < ValidPositionsICE.Count; i++)
                {
                    float temp = Mathf.Sqrt((Mathf.Pow(ValidPositionsICE[i].x - diffCentreXY.x, 2) + Mathf.Pow(ValidPositionsICE[i].z - diffCentreXY.y, 2)));
                    ValidPositionsICE[i] = new Vector4(ValidPositionsICE[i].x, ValidPositionsICE[i].y, ValidPositionsICE[i].z, temp);

                    if (radTest == true)
                    {
                        GameObject test = Instantiate(radtest, new Vector3(ValidPositionsICE[i].x, ValidPositionsICE[i].y, ValidPositionsICE[i].z), Quaternion.identity);
                        test.transform.localScale = new Vector3(0.1f, (ValidPositionsICE[i].w), 0.1f);
                    }

                }
                for (int i = 0; i < ValidPositionsNORMAL.Count; i++)
                {
                    float temp = Mathf.Sqrt((Mathf.Pow(ValidPositionsNORMAL[i].x - diffCentreXY.x, 2) + Mathf.Pow(ValidPositionsNORMAL[i].z - diffCentreXY.y, 2)));
                    ValidPositionsNORMAL[i] = new Vector4(ValidPositionsNORMAL[i].x, ValidPositionsNORMAL[i].y, ValidPositionsNORMAL[i].z, temp);

                    if (radTest == true)
                    {
                        GameObject test = Instantiate(radtest, new Vector3(ValidPositionsNORMAL[i].x, ValidPositionsNORMAL[i].y, ValidPositionsNORMAL[i].z), Quaternion.identity);
                        test.transform.localScale = new Vector3(0.1f, (ValidPositionsNORMAL[i].w), 0.1f);
                    }

                }
            }


            for (int i = 0; i < players.Count; i++)
            {
                Vector3 position = players[i].gameObject.transform.localPosition;
                Vector3 maxXY = position + (spawnSaftyXY / 2);
                Vector3 minXY = position - (spawnSaftyXY / 2);

                for (int j = 0; j < ValidPositionsFIRE.Count; j++)
                {
                    if (ValidPositionsFIRE[j].x > minXY.x && ValidPositionsFIRE[j].x < maxXY.x)
                    {
                        if (ValidPositionsFIRE[j].z > minXY.z && ValidPositionsFIRE[j].z < maxXY.z)
                        {
                            TempInvalidFIRE.Add(ValidPositionsFIRE[j]);
                            ValidPositionsFIRE.RemoveAt(j);
                            j--;
                        }
                    }
                }
                for (int j = 0; j < ValidPositionsICE.Count; j++)
                {
                    if (ValidPositionsICE[j].x > minXY.x && ValidPositionsICE[j].x < maxXY.x)
                    {
                        if (ValidPositionsICE[j].z > minXY.z && ValidPositionsICE[j].z < maxXY.z)
                        {
                            TempInvalidICE.Add(ValidPositionsICE[j]);
                            ValidPositionsICE.RemoveAt(j);
                            j--;
                        }
                    }
                }
                for (int j = 0; j < ValidPositionsNORMAL.Count; j++)
                {
                    if (ValidPositionsNORMAL[j].x > minXY.x && ValidPositionsNORMAL[j].x < maxXY.x)
                    {
                        if (ValidPositionsNORMAL[j].z > minXY.z && ValidPositionsNORMAL[j].z < maxXY.z)
                        {
                            TempInvalidNORMAL.Add(ValidPositionsNORMAL[j]);
                            ValidPositionsNORMAL.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }

            //spawn slimes
            if (toSpawn <= haveSpawn)
            {
                isSpawning = false;
            }

            if (isSpawning == true)
            {
                if (onceSpawnng == true)
                {
                    stage1 = Mathf.CeilToInt(0.75f * toSpawn);
                    stage2 = (toSpawn - stage1);
                    onceSpawnng = false;
                }

                GameObject[] slimes = GameObject.FindGameObjectsWithTag("BULLETIGNORESLIME");

                bool spawn = true;
                if (slimes.Length > MAXslimes)
                {
                    Debug.Log("max reached");
                    spawn = false;
                }
                else
                {
                    timer += Time.deltaTime;
                }

                if (timer <= stage1time && spawn ==true)
                {
                    //spawn 75%
                    stage1timer += Time.deltaTime;
                    float ShouldHaveSpawned = Mathf.CeilToInt((stage1timer / stage1time) * stage1);
                    float tospawn = ShouldHaveSpawned - haveSpawn - 1;
                    for (int i = 0; i < tospawn; i++)
                    {
                        StartCoroutine(Spawn());
                    }

                }
                else if (spawn == true)
                {
                    //Spawn 25%
                    stage2timer += Time.deltaTime;
                    float ShouldHaveSpawned = Mathf.CeilToInt((stage2timer / (stage2time)) * stage2);
                    float tospawn = ShouldHaveSpawned - (haveSpawn - stage1) - 1;
                    for (int i = 0; i < tospawn; i++)
                    {
                        StartCoroutine(Spawn());
                    }
                }
            }

            //reset valid positions
            for (int i = 0; i < TempInvalidFIRE.Count; i++)
            {
                ValidPositionsFIRE.Add(TempInvalidFIRE[i]);
                TempInvalidFIRE.RemoveAt(i);
                i--;
            }
            for (int i = 0; i < TempInvalidICE.Count; i++)
            {
                ValidPositionsICE.Add(TempInvalidICE[i]);
                TempInvalidICE.RemoveAt(i);
                i--;
            }
            for (int i = 0; i < TempInvalidNORMAL.Count; i++)
            {
                ValidPositionsNORMAL.Add(TempInvalidNORMAL[i]);
                TempInvalidNORMAL.RemoveAt(i);
                i--;
            }
        }
    }



    IEnumerator LAZERscan()
    {
        for (float i = startXYpos.y; i < finishXYpos.y; i+= LAZERNODEdetail.y)
        {
            GameObject temp2 = Instantiate(LAZERNODEprefab, new Vector3(startXYpos.x, LAZERNODEheight, i), Quaternion.identity);
            temp2.AddComponent<LAZERNODE>();
            temp2.GetComponent<LineRenderer>().startWidth = 0.05f;
            temp2.GetComponent<LineRenderer>().endWidth = 0.05f;
            temp2.GetComponent<LineRenderer>().enabled = false;

            temp2.transform.SetParent(this.gameObject.transform);
            temp2.GetComponent<LAZERNODE>().startXYpos = startXYpos;
            temp2.GetComponent<LAZERNODE>().finXYpos = finishXYpos;
            temp2.GetComponent<LAZERNODE>().LAZERNODEdetail = LAZERNODEdetail;

        }


        for (int i = 0; i < transform.childCount; i++)
        {
           transform.GetChild(i).GetComponent<LAZERNODE>().Scan();
        }


        yield return null;

    }


    public IEnumerator Spawn()
    {
        haveSpawn += 1;
        float spawnPosRad = 0.0f;
        for (int j = 0; j < ValidPositionsFIRE.Count; j++)
        {
            spawnPosRad += ValidPositionsFIRE[j].w;
        }
        for (int j = 0; j < ValidPositionsICE.Count; j++)
        {
            spawnPosRad += ValidPositionsICE[j].w;
        }
        for (int j = 0; j < ValidPositionsNORMAL.Count; j++)
        {
            spawnPosRad += ValidPositionsNORMAL[j].w;
        }

        spawnPosRad = Random.Range(0.0f, spawnPosRad);

        for (int j = 0; j < ValidPositionsFIRE.Count; j++)
        {
            spawnPosRad -= ValidPositionsFIRE[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;
                GameObject refr = Instantiate(slimePrefab[0], new Vector3(ValidPositionsFIRE[j].x, ValidPositionsFIRE[j].y + 100, ValidPositionsFIRE[j].z), Quaternion.identity);
                refr.GetComponent<slimeController>().type = slimeController.slimeTypes.FIRE;
            }
        }
        for (int j = 0; j < ValidPositionsICE.Count; j++)
        {
            spawnPosRad -= ValidPositionsICE[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;
                GameObject refr = Instantiate(slimePrefab[1], new Vector3(ValidPositionsICE[j].x, ValidPositionsICE[j].y + 100, ValidPositionsICE[j].z), Quaternion.identity);
                refr.GetComponent<slimeController>().type = slimeController.slimeTypes.ICE;
            }
        }
        for (int j = 0; j < ValidPositionsNORMAL.Count; j++)
        {
            spawnPosRad -= ValidPositionsNORMAL[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;
                GameObject refr = Instantiate(slimePrefab[2], new Vector3(ValidPositionsNORMAL[j].x, ValidPositionsNORMAL[j].y + 100, ValidPositionsNORMAL[j].z), Quaternion.identity);
                refr.GetComponent<slimeController>().type = slimeController.slimeTypes.NORMAL;
            }
        }
        yield return null;
    }



    public IEnumerator Spawnpowerup(Pickups.POWERUPS temp)
    {

        for (int i = 0; i < players.Count; i++)
        {
            Vector3 position = players[i].gameObject.transform.localPosition;
            Vector3 maxXY = position + (spawnSaftyXY / 2);
            Vector3 minXY = position - (spawnSaftyXY / 2);

            for (int j = 0; j < ValidPositionsFIRE.Count; j++)
            {
                if (ValidPositionsFIRE[j].x > minXY.x && ValidPositionsFIRE[j].x < maxXY.x)
                {
                    if (ValidPositionsFIRE[j].z > minXY.z && ValidPositionsFIRE[j].z < maxXY.z)
                    {
                        TempInvalidFIRE.Add(ValidPositionsFIRE[j]);
                        ValidPositionsFIRE.RemoveAt(j);
                        j--;
                    }
                }
            }
            for (int j = 0; j < ValidPositionsICE.Count; j++)
            {
                if (ValidPositionsICE[j].x > minXY.x && ValidPositionsICE[j].x < maxXY.x)
                {
                    if (ValidPositionsICE[j].z > minXY.z && ValidPositionsICE[j].z < maxXY.z)
                    {
                        TempInvalidICE.Add(ValidPositionsICE[j]);
                        ValidPositionsICE.RemoveAt(j);
                        j--;
                    }
                }
            }
            for (int j = 0; j < ValidPositionsNORMAL.Count; j++)
            {
                if (ValidPositionsNORMAL[j].x > minXY.x && ValidPositionsNORMAL[j].x < maxXY.x)
                {
                    if (ValidPositionsNORMAL[j].z > minXY.z && ValidPositionsNORMAL[j].z < maxXY.z)
                    {
                        TempInvalidNORMAL.Add(ValidPositionsNORMAL[j]);
                        ValidPositionsNORMAL.RemoveAt(j);
                        j--;
                    }
                }
            }
        }


        float spawnPosRad = 0.0f;
        for (int j = 0; j < ValidPositionsFIRE.Count; j++)
        {
            spawnPosRad += ValidPositionsFIRE[j].w;
        }
        for (int j = 0; j < ValidPositionsICE.Count; j++)
        {
            spawnPosRad += ValidPositionsICE[j].w;
        }
        for (int j = 0; j < ValidPositionsNORMAL.Count; j++)
        {
            spawnPosRad += ValidPositionsNORMAL[j].w;
        }

        spawnPosRad = Random.Range(0.0f, spawnPosRad);

        for (int j = 0; j < ValidPositionsFIRE.Count - 1; j++)
        {
            spawnPosRad -= ValidPositionsFIRE[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;
                GameObject power =  Instantiate(GetComponent<Pickups>().powerup, new Vector3(ValidPositionsFIRE[j].x, ValidPositionsFIRE[j].y + 100, ValidPositionsFIRE[j].z), Quaternion.identity);
                power.GetComponent<pickupcontroler>().type = temp;
            }
        }
        for (int j = 0; j < ValidPositionsICE.Count - 1; j++)
        {
            spawnPosRad -= ValidPositionsICE[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;
                GameObject power = Instantiate(GetComponent<Pickups>().powerup, new Vector3(ValidPositionsICE[j].x, ValidPositionsICE[j].y + 100, ValidPositionsICE[j].z), Quaternion.identity);
                power.GetComponent<pickupcontroler>().type = temp;
            }
        }
        for (int j = 0; j < ValidPositionsNORMAL.Count - 1; j++)
        {
            spawnPosRad -= ValidPositionsNORMAL[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;
                GameObject power = Instantiate(GetComponent<Pickups>().powerup, new Vector3(ValidPositionsNORMAL[j].x, ValidPositionsNORMAL[j].y + 100, ValidPositionsNORMAL[j].z), Quaternion.identity);
                power.GetComponent<pickupcontroler>().type = temp;
            }
        }
        yield return null;
    }

    public int wavetospawnammount(int round, int players)
    {
         int tospawn = 0;

         if (players == 1)
         {
             if (round <= 9)
             {
                int[] zombs = new int[10] { 0, 6, 8, 13, 18, 24, 27, 28, 28, 29 };
                 tospawn = zombs[round];
             }
             if (round >= 10)
             {
                  tospawn = Mathf.FloorToInt(24 + ((3 * ((round / 5) * (round * 0.15f)))));
             }                   
         }
         else
         {
             if (round <= 9)
             {
                 if (players == 2)
                 {
                     int[] zombs = new int[10] { 0, 7, 9, 15, 21, 27, 31, 32, 33, 34 };
                     tospawn = zombs[round];
                 }
             
                 if (players == 3)
                 {
                    int[] zombs = new int[10] { 0, 9, 10, 18, 25, 32, 38, 40, 43, 45 };
                     tospawn = zombs[round];
                 }
                 if (players == 4)
                 {
                    int[] zombs = new int[10] { 0, 10, 12, 21, 29, 37, 45, 49, 52, 56 };
                     tospawn = zombs[round];
                 }
             }
             if (round >= 10)
             {
                 tospawn = Mathf.FloorToInt(24 + (((players - 1) * 6) * ((round / 5) * (round * 0.15f))));
             }

         }
                    
        return (tospawn * 4);
    }
}
