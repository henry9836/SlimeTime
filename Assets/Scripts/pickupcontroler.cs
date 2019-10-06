using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupcontroler : MonoBehaviour
{
    public float lifetime = 30.0f;
    private bool once = true;
    //public enum type = GameObject.Find("GameManager").GetComponent<Pickups>().POWERUPS;

    void FixedUpdate()
    {
        if (once == true)
        {
            once = false;
            
        }

        lifetime -= Time.deltaTime;

        if (lifetime <= 0.0f)
        {
            StartCoroutine(despawn());
        }
    }

    IEnumerator despawn()
    {


        yield return null;
    }

}
