using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class mainmenuFade : MonoBehaviour
{

    public float timer = 0.0f;
    public float stayTimer = 5.0f;
    public float fadeTimer = 2.0f;
    public List<Sprite> Splashes;
    public int SplashNo = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= stayTimer)
        {
            SplashNo += 1;
            if (SplashNo == 4)
            {
                SplashNo = 0;
            }


            if (SplashNo == 0 || SplashNo == 2)
            {
                timer = 0.0f;
                GameObject.Find("splashes").transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite = Splashes[SplashNo];
                StartCoroutine(fadeswap(true));

            }
            else
            {
                GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = Splashes[SplashNo];
                timer = 0.0f;
                StartCoroutine(fadeswap(false));
            }
        }
    }

    IEnumerator fadeswap(bool Back2Front)
    {

        for (float timer = 0.0f; timer < fadeTimer; timer += Time.deltaTime)
        {
            if (Back2Front == false)
            {
                GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.r, GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.g, GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(0.0f, 1.0f, timer / fadeTimer));
                GameObject.Find("splashes").transform.GetChild(1).transform.GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.r, GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.g, GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(1.0f, 0.0f, timer / fadeTimer));
                yield return null;
            }
            else
            {
                GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.r, GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.g, GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(1.0f, 0.0f, timer / fadeTimer));
                GameObject.Find("splashes").transform.GetChild(1).transform.GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.r, GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.g, GameObject.Find("splashes").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(0.0f, 1.0f, timer / fadeTimer));
                yield return null;
            }



        }

    }
}
