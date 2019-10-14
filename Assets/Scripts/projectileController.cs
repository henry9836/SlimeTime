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

    private float safeTime = 0.001f;
    private bool colLock = true;

    private void Start()
    {
        colLock = true;
        StartCoroutine(Activate());

        //Play fire sound according to proj type

        GetComponent<AudioSource>().clip = fireSounds[(int)type];
        GetComponent<AudioSource>().Play();

        if (type != PROJTYPES.ARROW)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        projObjects[(int)type].SetActive(true);
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
