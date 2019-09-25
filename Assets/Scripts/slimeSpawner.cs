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
    public Vector2Int LAZERNODEdetail;
    public Vector2Int startXYpos;
    public Vector2Int finishXYpos;


    public Vector3 ValidPositionFIRE;
    public Vector3 ValidPositionICE;
    public Vector3 ValidPositionNORMAL;



    void Update()
    {
        timer += Time.deltaTime;
        if (once == true)
        {
            once = false;
            StartCoroutine(LAZERscan());
        }


        if (timer >= 2.0f && currentSlimes < maxSlimes)
        {
            currentSlimes += 1;


            float rand = Random.Range(0.0f, 100.0f);
            int HPChosen = 0;

            for (int i = 0; i < slimeHP.Count; i++)
            {
                float temp = persentgaes[i];

                for (int j = 0; j < i; j++)
                {
                    temp += persentgaes[j];
                }
                if (rand < temp)
                {
                    HPChosen = slimeHP[i];
                    i = slimeHP.Count;
                }
            }
            if (HPChosen == 0)
            {
                HPChosen = 1;
            }
            timer = 0.0f;

           

            GameObject objRef = Instantiate(slimePrefab[Random.Range(0, slimePrefab.Count)], new Vector3(Random.Range(-7f, 7f), Random.Range(3.3f, -3.3f), 0.0f), Quaternion.identity);
            objRef.GetComponent<slime>().Init(HPChosen);
        }
    }

    IEnumerator LAZERscan()
    {
        for (int i = startXYpos.y; i < finishXYpos.y; i+= LAZERNODEdetail.y)
        {
            GameObject temp2 = Instantiate(LAZERNODEprefab, new Vector3(startXYpos.x, i, LAZERNODEheight), Quaternion.identity);
            temp2.AddComponent<LAZERNODE>();
            temp2.transform.SetParent(this.gameObject.transform);
        }

        for (int j = startXYpos.x; j < finishXYpos.x; j += LAZERNODEdetail.x)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                
               transform.GetChild(i).GetComponent<LAZERNODE>().Scan();
            }

            this.gameObject.transform.localPosition += new Vector3(LAZERNODEdetail.x, 0.0f, 0.0f);
        }


        int temp = transform.childCount;
        for (int i = 0; i < temp; i++)
        {
            transform.GetChild(i).transform.gameObject.SetActive(false);        
        }

        yield return null;

    }
}
