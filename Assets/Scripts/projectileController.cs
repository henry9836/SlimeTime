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
    public GameObject destroyParticle;
    public PROJTYPES type = PROJTYPES.ARROW;
    public List<AudioClip> fireSounds = new List<AudioClip>();
    public List<GameObject> projObjects = new List<GameObject>();
    public Vector3 travelDir;
    public GameObject playerRef;

    private float safeTime = 0.001f;
    private bool colLock = true;

    private void Start()
    {
        colLock = true;
        StartCoroutine(Activate());


        if (type != PROJTYPES.ARROW)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        projObjects[(int)type].SetActive(true);


        //Play fire sound according to proj type

        playerRef.GetComponent<AudioSource>().clip = fireSounds[(int)type];
        playerRef.GetComponent<AudioSource>().Play();
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

                Vector3 hitPos = other.ClosestPoint(transform.position);

                Instantiate(destroyParticle, hitPos + (Vector3.forward), Quaternion.identity);

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
