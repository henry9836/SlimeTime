using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{

    public enum PROJTYPES
    {
        ARROW,
        AXE,
        FIREBALL,
        NOTE,
    };

    public float damage = 1.0f;
    public PROJTYPES type = PROJTYPES.ARROW;
    public List<AudioClip> fireSounds = new List<AudioClip>();
    public Vector3 travelDir;

    private float safeTime = 0.001f;
    private bool colLock = true;

    private void Start()
    {
        colLock = true;
        StartCoroutine(Activate());

        //Play fire sound according to proj type

        GetComponent<AudioSource>().clip = fireSounds[(int)type];
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!colLock)
        {
            if (other.tag != "Player" && other.tag != "BULLETIGNORE" && other.tag != "BULLETIGNORESLIME" && other.tag != "NORMAL" && other.tag != "ICE" && other.tag != "FIRE")
            {

                if (other.tag == "Slime")
                {
                    other.transform.parent.GetComponent<slimeController>().DamageSlime(damage, travelDir);
                }

                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(safeTime);
        colLock = false;
    }

}
