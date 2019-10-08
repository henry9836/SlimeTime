using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSphereController : MonoBehaviour
{

    private float attackCooldown;
    private bool canAttack;
    private void Start()
    {
        canAttack = true;
        attackCooldown = transform.parent.GetComponent<slimeController>().attackCooldown;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && canAttack)
        {
            other.GetComponent<PlayerController>().health -= transform.parent.gameObject.GetComponent<slimeController>().attackDamage;
            other.GetComponent<Rigidbody>().AddForce(((other.transform.position - this.transform.position).normalized * transform.parent.gameObject.GetComponent<slimeController>().attackDamage) * transform.parent.gameObject.GetComponent<slimeController>().attackEffectMultiplyer);
            Debug.Log("HIT! " + other.name);
            StartCoroutine(attackCoolDown());
        }
    }

    IEnumerator attackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

}
