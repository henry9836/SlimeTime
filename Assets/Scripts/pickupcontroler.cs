using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupcontroler : MonoBehaviour
{
    public float lifetimeStart = 30.0f;
    public Pickups.POWERUPS type;
    public float amplitudeRate;

    private int ammoCount = 0;
    private float lifetime = 30.0f;
    private float landingypos;
    private bool once = true;

    void Start()
    {
        lifetime = lifetimeStart;

        //Set ammo counts
        if (type == Pickups.POWERUPS.NULL)
        {
            Debug.LogWarning("Pickup has type NULL assigned: " + name);
        }
        else if (type == Pickups.POWERUPS.RAPIDFIRE)
        {
            ammoCount = 150;
        }
        else if (type == Pickups.POWERUPS.MULTISHOTT1 || type == Pickups.POWERUPS.MULTISHOTT2)
        {
            ammoCount = 150;
        }
        else if (type == Pickups.POWERUPS.SPREAD)
        {
            ammoCount = 150;
        }
        else if (type == Pickups.POWERUPS.HEAL)
        {
            ammoCount = 0;
        }
        else if (type == Pickups.POWERUPS.WALLOFDEATH)
        {
            ammoCount = 100;
        }
        else
        {
            Debug.LogWarning("Pickup has an unknown type assigned: " + type + " Object: " + name);
        }


    }
    void FixedUpdate()
    {
        this.gameObject.transform.Rotate(0.0f, 50 * Time.deltaTime,  0.0f);
        
        lifetime -= Time.deltaTime;

        if (lifetime <= 0.0f)
        {
            StartCoroutine(despawn());
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (type == Pickups.POWERUPS.HEAL)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].GetComponent<PlayerController>().health > 0)
                    {
                        players[i].GetComponent<PlayerController>().health = 100;
                    }
                }
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().powerupType = type;
                collision.gameObject.GetComponent<PlayerController>().pickupAmmoCount = ammoCount;
                StartCoroutine(despawn());
            }
        }

        if (once == true)
        {
            once = false;
            landingypos = gameObject.transform.localPosition.y + 0.5f;
            lifetime = lifetimeStart;

            StartCoroutine(bob());
        }

    }

    IEnumerator despawn()
    {
        Destroy(this.gameObject);
        yield return null;
    }

    IEnumerator bob()
    {
        for (float timer = 0.0f; timer < amplitudeRate; timer += Time.deltaTime)
        {
            float ypos = Mathf.Lerp(landingypos, landingypos + 1.0f, Mathf.Pow(Mathf.Sin((5 * (timer / amplitudeRate)) / Mathf.PI), 2));
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, ypos , gameObject.transform.localPosition.z);
            yield return null;

        }
        for (float timer = 0.0f; timer < amplitudeRate; timer += Time.deltaTime)
        {
            float ypos = Mathf.Lerp(landingypos + 1.0f, landingypos, Mathf.Pow(Mathf.Sin((5 * (timer / amplitudeRate)) / Mathf.PI), 2)); 
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, ypos , gameObject.transform.localPosition.z);
            yield return null;

        }

        StartCoroutine(bob());
    }

}
