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

    void Update()
    {
        timer += Time.deltaTime;



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
}
