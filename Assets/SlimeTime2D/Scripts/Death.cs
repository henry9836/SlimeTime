using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public List<AudioClip> deathSounds;
    void Start()
    {
        GetComponent<AudioSource>().clip = deathSounds[Random.Range(0, deathSounds.Count)];
        GetComponent<AudioSource>().Play();
        StartCoroutine(KillMe());
    }

    IEnumerator KillMe()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

}
