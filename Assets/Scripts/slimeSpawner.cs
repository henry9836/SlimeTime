using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeSpawner : MonoBehaviour
{
    public float timer = 0.0f;
    public List<int> slimeHP;
    public List<float> persentgaes;
    public List<GameObject> slimePrefab;
    public int toSpawn;
    public int haveSpawn;
    public bool isSpawning = true;

    public GameObject radtest;


    private float stage1;
    public float stage1timer;
    private float stage1Delay;
    private float stage2;
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

    public Vector2 spawnSaftyXY;
    public List<Vector4> ValidPositionsFIRE;
    public List<Vector4> ValidPositionsICE;
    public List<Vector4> ValidPositionsNORMAL;
    public List<Vector4> InvalidPositions;
    public List<Vector4> TempInvalidFIRE;
    public List<Vector4> TempInvalidICE;
    public List<Vector4> TempInvalidNORMAL;


    public void NextWave(int wave)
    {
        isSpawning = true;
        toSpawn = wave;
        GameObject.Find("GameManager").GetComponent<GameManager>().remainingSpawn = toSpawn;
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

        if (ValidPositionsFIRE.Count + ValidPositionsICE.Count + ValidPositionsNORMAL.Count + InvalidPositions.Count + TempInvalidFIRE.Count + TempInvalidICE.Count + TempInvalidNORMAL.Count == pointcount && isSpawning == true)
        {
            if (onceRAD == true)
            {
                onceRAD = false;
                //initial calculations

                for (int i = 0; i < ValidPositionsFIRE.Count; i++)
                {
                    float temp = Mathf.Sqrt(((Mathf.Pow(ValidPositionsFIRE[i].x - diffCentreXY.x, 2)) + (Mathf.Pow(ValidPositionsFIRE[i].z - diffCentreXY.y, 2))));
                    ValidPositionsFIRE[i] = new Vector4(ValidPositionsFIRE[i].x, ValidPositionsFIRE[i].y, ValidPositionsFIRE[i].z, temp);

                    GameObject test = Instantiate(radtest, new Vector3(ValidPositionsFIRE[i].x, ValidPositionsFIRE[i].y, ValidPositionsFIRE[i].z), Quaternion.identity);
                    test.transform.localScale = new Vector3(0.1f, (ValidPositionsFIRE[i].w), 0.1f);
                }
                for (int i = 0; i < ValidPositionsICE.Count; i++)
                {
                    float temp = Mathf.Sqrt((Mathf.Pow(ValidPositionsICE[i].x - diffCentreXY.x, 2) + Mathf.Pow(ValidPositionsICE[i].z - diffCentreXY.y, 2)));
                    ValidPositionsICE[i] = new Vector4(ValidPositionsICE[i].x, ValidPositionsICE[i].y, ValidPositionsICE[i].z, temp);

                    GameObject test = Instantiate(radtest, new Vector3(ValidPositionsICE[i].x, ValidPositionsICE[i].y, ValidPositionsICE[i].z), Quaternion.identity);
                    test.transform.localScale = new Vector3(0.1f, (ValidPositionsICE[i].w), 0.1f);
                }
                for (int i = 0; i < ValidPositionsNORMAL.Count; i++)
                {
                    float temp = Mathf.Sqrt((Mathf.Pow(ValidPositionsNORMAL[i].x - diffCentreXY.x, 2) + Mathf.Pow(ValidPositionsNORMAL[i].z - diffCentreXY.y, 2)));
                    ValidPositionsNORMAL[i] = new Vector4(ValidPositionsNORMAL[i].x, ValidPositionsNORMAL[i].y, ValidPositionsNORMAL[i].z, temp);

                    GameObject test = Instantiate(radtest, new Vector3(ValidPositionsNORMAL[i].x, ValidPositionsNORMAL[i].y, ValidPositionsNORMAL[i].z), Quaternion.identity);
                    test.transform.localScale = new Vector3(0.1f, (ValidPositionsNORMAL[i].w), 0.1f);
                }
                for (int i = 0; i < InvalidPositions.Count; i++)
                {
                    float temp = Mathf.Sqrt((Mathf.Pow(InvalidPositions[i].x, 2) + Mathf.Pow(InvalidPositions[i].z, 2)));
                    InvalidPositions[i] = new Vector4(InvalidPositions[i].x, InvalidPositions[i].y, InvalidPositions[i].z, temp);
                }


            }

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < players.Length; i++)
            {
                Vector2 position = players[i].gameObject.transform.localPosition;
                Vector2 maxXY = position + (spawnSaftyXY / 2);
                Vector2 minXY = position - (spawnSaftyXY / 2);

                for (int j = 0; j < ValidPositionsFIRE.Count; j++)
                {
                    if (ValidPositionsFIRE[j].x > minXY.x && ValidPositionsFIRE[j].x < maxXY.x)
                    {
                        if (ValidPositionsFIRE[j].z > minXY.y && ValidPositionsFIRE[j].z < maxXY.y)
                        {
                            Debug.Log("chnage");
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
                        if (ValidPositionsICE[j].z > minXY.y && ValidPositionsICE[j].z < maxXY.y)
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
                        if (ValidPositionsNORMAL[j].z > minXY.y && ValidPositionsNORMAL[j].z < maxXY.y)
                        {
                            TempInvalidNORMAL.Add(ValidPositionsNORMAL[j]);
                            ValidPositionsNORMAL.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }

            //spawn slimes
            if (toSpawn == haveSpawn)
            {
                isSpawning = false;
            }

            if (isSpawning == true)
            {
                if (onceSpawnng == true)
                {
                    stage1 = 0.75f * toSpawn;
                    stage2 = toSpawn - stage1;

                    stage1Delay = stage1timer / stage1;
                    stage2Delay = stage2timer / stage2;

                    onceSpawnng = false;
                }
                if (timer <= stage1timer)
                {

                    //spawn 75%

                }
                else if (timer <= stage2timer)
                {
                    //the rest

                    isSpawning = false;
                }
                //if (timer >= 2.0f && currentSlimes < maxSlimes)
                //{
                //    currentSlimes += 1;
                //    float rand = Random.Range(0.0f, 100.0f);
                //    int HPChosen = 0;
                //    for (int i = 0; i < slimeHP.Count; i++)
                //    {
                //        float temp = persentgaes[i];
                //        for (int j = 0; j < i; j++)
                //        {
                //            temp += persentgaes[j];
                //        }
                //        if (rand < temp)
                //        {
                //            HPChosen = slimeHP[i];
                //            i = slimeHP.Count;
                //        }
                //    }
                //    if (HPChosen == 0)
                //    {
                //        HPChosen = 1;
                //    }
                //    timer = 0.0f;
                //    GameObject objRef = Instantiate(slimePrefab[Random.Range(0, slimePrefab.Count)], new Vector3(Random.Range(-7f, 7f), Random.Range(3.3f, -3.3f), 0.0f), Quaternion.identity);
                //    objRef.GetComponent<slime>().Init(HPChosen);
                //}
            }



            //reset valid positions
            for (int i = 0; i < TempInvalidFIRE.Count; i++)
            {
                Debug.Log("fire chnage"); 
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
}
