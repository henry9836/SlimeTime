using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeController : MonoBehaviour
{
    public enum slimeTypes
    {
        NORMAL,
        FIRE,
        ICE
    }

    public slimeTypes type = slimeTypes.NORMAL;

    public float health = 1.0f;
    public float damageEffectMultiplyer = 1.0f;
    public float attackDamage = 1.0f;
    public float attackCooldown = 3.0f;
    public float attackRange = 1.0f;
    public float attackEffectMultiplyer = 100.0f;

    private bool canAttack = true;
    private bool tmpAttack = false;

    public void DamageSlime(float damage, Vector3 hitDir)
    {
        health -= damage;
        GetComponent<Rigidbody>().AddForce((hitDir * damage)* damageEffectMultiplyer);
    }

    private void Start()
    {
        canAttack = true;
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (canAttack)
        {
            StartCoroutine(SwingInTheAir());
        }

        //Kill floor

        if (transform.position.y < -10)
        {
            health -= 999.0f;
        }

    }

    IEnumerator SwingInTheAir()
    {
        canAttack = false;
        tmpAttack = false;
        //Check for player
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        for (int i =0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Player")
            {
                hitColliders[i].gameObject.GetComponent<PlayerController>().health -= attackDamage;
                hitColliders[i].gameObject.GetComponent<Rigidbody>().AddForce(((hitColliders[i].gameObject.transform.position - this.transform.position).normalized * attackDamage) * attackEffectMultiplyer);
                Debug.Log("HIT! " + hitColliders[i].gameObject.name);
                tmpAttack = true;
            }
            i++;
        }

        if (tmpAttack)
        {
            yield return new WaitForSeconds(attackCooldown);
        }
        yield return null;
        canAttack = true;
    }

}
