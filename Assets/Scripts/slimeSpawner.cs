using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeSpawner : MonoBehaviour
{
    public float timer = 0.0f;
    public List<int> slimeHP;
    public List<float> persentgaes;
    public List<GameObject> slimePrefab;
    public int maxSlimes;
    public int currentSlimes;

    public bool once = true;
    public GameObject LAZERNODEprefab;
    public float LAZERNODEheight;
    public Vector2 LAZERNODEdetail;
    public Vector2 startXYpos;
    public Vector2 finishXYpos;


    public List<Vector3> ValidPositionsFIRE;
    public List<Vector3> ValidPositionsICE;
    public List<Vector3> ValidPositionsNORMAL;
    public List<Vector3> InvalidPositions;




    void Update()
    {
        timer += Time.deltaTime;
        if (once == true)
        {
            once = false;
            StartCoroutine(LAZERscan());
        }

        

        int pointcount = Mathf.FloorToInt((finishXYpos.y - startXYpos.y) / LAZERNODEdetail.y) * Mathf.FloorToInt((finishXYpos.x - startXYpos.x) / LAZERNODEdetail.x);



        if (ValidPositionsFIRE.Count + ValidPositionsICE.Count + ValidPositionsNORMAL.Count + InvalidPositions.Count == pointcount)
        {
            Debug.Log("EPIC WIN");
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
