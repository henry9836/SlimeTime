using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupcontroler : MonoBehaviour
{
    public float lifetimeStart = 30.0f;
    private float lifetime = 30.0f;
    private bool once = true;
    public Pickups.POWERUPS type;
    public float amplitudeRate;
    private float landingypos;


    void Start()
    {
        lifetime = lifetimeStart;
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


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().powerupType = type;
            StartCoroutine(despawn());

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
