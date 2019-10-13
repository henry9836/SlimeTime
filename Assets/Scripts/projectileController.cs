using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{

    public float damage = 1.0f;

    public Vector3 travelDir;

    private float safeTime = 0.01f;
    private bool colLock = true;

    private void Start()
    {
        colLock = true;
        StartCoroutine(Activate());
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
