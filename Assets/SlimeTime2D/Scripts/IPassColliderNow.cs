using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPassColliderNow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<ProjectileController>().OnChildTriggerEnter2D(collision);
    }
}
