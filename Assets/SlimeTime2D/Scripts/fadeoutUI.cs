using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fadeoutUI : MonoBehaviour
{
    public float fadeduration = 5.0f;
    public float waittime = 2.0f;



    public void Fadein()
    {
        StartCoroutine(fade());
    }

    IEnumerator fade()
    {
        /*Henry is extra thin*/
        //thin = fat btw 
        for (float timer = 0.0f; timer < (fadeduration/3); timer += Time.deltaTime)
        {
            Debug.Log("in");

            this.transform.GetComponent<Image>().color = new Color(this.transform.GetComponent<Image>().color.r, this.transform.GetComponent<Image>().color.g, this.transform.GetComponent<Image>().color.b, Mathf.Lerp(0.0f, 0.8f, timer / (fadeduration/3)));
            yield return null;
        }

        Debug.Log("wait");

        yield return new WaitForSeconds(waittime);
        
        for (float timer = 0.0f; timer < fadeduration; timer += Time.deltaTime)
        {
            Debug.Log("out");

            this.transform.GetComponent<Image>().color = new Color(this.transform.GetComponent<Image>().color.r, this.transform.GetComponent<Image>().color.g, this.transform.GetComponent<Image>().color.b, Mathf.Lerp(0.8f, 0.0f, timer / fadeduration));
            yield return null;
        }
    }
}
