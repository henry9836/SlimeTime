using System.Collections;
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


    public void NextWave(int wave)
    {
        onceSpawnng = true;
        timer = 0.0f;
        isSpawning = true;
        toSpawn = wave;
        haveSpawn = 0;
        GameObject.Find("GameManager").GetComponent<GameManager>().remainingSpawn = toSpawn;
        stage1timer = 0.0f;
        stage2timer = 0.0f;
    }



    void Update()
    {
        timer += Time.deltaTime;

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

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < players.Length; i++)
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

                if (timer <= stage1time)
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
                else
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

                Instantiate(slimePrefab[0], new Vector3(ValidPositionsFIRE[j].x, ValidPositionsFIRE[j].y + 100, ValidPositionsFIRE[j].z), Quaternion.identity);


            }
        }
        for (int j = 0; j < ValidPositionsICE.Count; j++)
        {
            spawnPosRad -= ValidPositionsICE[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;

                Instantiate(slimePrefab[1], new Vector3(ValidPositionsICE[j].x, ValidPositionsICE[j].y + 100, ValidPositionsICE[j].z), Quaternion.identity);


            }
        }
        for (int j = 0; j < ValidPositionsNORMAL.Count; j++)
        {
            spawnPosRad -= ValidPositionsNORMAL[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;

                Instantiate(slimePrefab[2], new Vector3(ValidPositionsNORMAL[j].x, ValidPositionsNORMAL[j].y + 100, ValidPositionsNORMAL[j].z), Quaternion.identity);

            }
        }
        yield return null;
    }

    public IEnumerator Spawnpowerup(Pickups.POWERUPS temp)
    {

        Debug.Log(temp);
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

                //Instantiate
                //here


            }
        }
        for (int j = 0; j < ValidPositionsICE.Count; j++)
        {
            spawnPosRad -= ValidPositionsICE[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;


                //here

            }
        }
        for (int j = 0; j < ValidPositionsNORMAL.Count; j++)
        {
            spawnPosRad -= ValidPositionsNORMAL[j].w;
            if (spawnPosRad <= 0.0f)
            {
                spawnPosRad = Mathf.Infinity;

                //and here
            }
        }
        yield return null;
    }
}
