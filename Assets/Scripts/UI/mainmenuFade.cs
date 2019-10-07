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
    public List<Transform> SplashPOS;

    public int SplashNo = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= stayTimer)
        {
            SplashNo += 1;
            if (SplashNo >= Splashes.Count)
            {
                SplashNo = 0;
            }


            if (SplashNo == 0 || SplashNo == 2)
            {
                GameObject.Find("Backgrounds").transform.GetChild(1).transform.GetComponent<SpriteRenderer>().sprite = Splashes[SplashNo];
                GameObject.Find("Backgrounds").transform.GetChild(1).transform.localPosition = SplashPOS[SplashNo].localPosition;
                GameObject.Find("Backgrounds").transform.GetChild(1).transform.localRotation = SplashPOS[SplashNo].localRotation;
                GameObject.Find("Backgrounds").transform.GetChild(1).transform.localScale = SplashPOS[SplashNo].localScale;
                timer = 0.0f;
                StartCoroutine(fadeswap(true));
            }
            else
            {
                GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = Splashes[SplashNo];
                GameObject.Find("Backgrounds").transform.GetChild(0).transform.localPosition = SplashPOS[SplashNo].localPosition;
                GameObject.Find("Backgrounds").transform.GetChild(0).transform.localRotation = SplashPOS[SplashNo].localRotation;
                GameObject.Find("Backgrounds").transform.GetChild(0).transform.localScale = SplashPOS[SplashNo].localScale;
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
                GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.r, GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.g, GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(0.0f, 1.0f, timer / fadeTimer));
                GameObject.Find("Backgrounds").transform.GetChild(1).transform.GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.r, GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.g, GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(1.0f, 0.0f, timer / fadeTimer));
                yield return null;
            }
            else
            {
                GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.r, GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.g, GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(1.0f, 0.0f, timer / fadeTimer));
                GameObject.Find("Backgrounds").transform.GetChild(1).transform.GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.r, GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.g, GameObject.Find("Backgrounds").transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(0.0f, 1.0f, timer / fadeTimer));
                yield return null;
            }
        }
    }
}
