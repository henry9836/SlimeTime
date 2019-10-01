using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class redfade : MonoBehaviour
{
    
    public float fadeTime = 5.0f;

    public void Fadein()
    {
       StartCoroutine(fade());
    }

    public void Fadeout()
    {
        StartCoroutine(fadeout());
    }

    IEnumerator fade()
    {
        for (float timer = 0.0f; timer < fadeTime; timer += Time.deltaTime)
        {
            GameObject.Find("Canvas").transform.GetChild(1).transform.GetComponent<Image>().color = new Color(GameObject.Find("Canvas").transform.GetChild(1).transform.GetComponent<Image>().color.r, GameObject.Find("Canvas").transform.GetChild(1).transform.GetComponent<Image>().color.g, GameObject.Find("Canvas").transform.GetChild(1).transform.GetComponent<Image>().color.b, Mathf.Lerp(0.0f, 0.4f, timer / fadeTime));
             yield return null;
        }
    }

    IEnumerator fadeout()
    {
        for (float timer = 0.0f; timer < fadeTime; timer += Time.deltaTime)
        {
            GameObject.Find("Canvas").transform.GetChild(1).transform.GetComponent<Image>().color = new Color(GameObject.Find("Canvas").transform.GetChild(1).transform.GetComponent<Image>().color.r, GameObject.Find("Canvas").transform.GetChild(1).transform.GetComponent<Image>().color.g, GameObject.Find("Canvas").transform.GetChild(1).transform.GetComponent<Image>().color.b, Mathf.Lerp(0.4f, 0.0f, timer / fadeTime));
            yield return null;
        }
    }


}
