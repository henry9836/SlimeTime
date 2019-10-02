﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{

    public float damage = 1.0f;

    public Vector3 travelDir;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "Player")
        {

            if (other.tag == "Slime")
            {
                other.GetComponent<slimeController>().DamageSlime(damage, travelDir);

            }

            Destroy(this.gameObject);
        }

    }
}
