using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade : MonoBehaviour
{
    public float speed = 1.0f;
    public bool up = true;
    public float timer = 0.0f;
    void Update()
    {
        if (timer >= 1.0f || !up)
        {
            timer -= Time.deltaTime * speed;
            up = false;
        }
        if (timer < 0.1f || up)
        {
            timer += Time.deltaTime * speed;
            up = true;
        }

        this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Clamp(timer + 0.2f, 0.2f, 1.0f));

    }

}
