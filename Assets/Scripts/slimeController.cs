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
    public float attackEffectMultiplyer = 100.0f;
    public float jumpCooldown = 1.0f;

    private bool canAttack = true;
    private bool attacking = false;
    private bool jumpLock = false;
    private Collider detectionSphere;

    public void DamageSlime(float damage, Vector3 hitDir)
    {
        health -= damage;
        GetComponent<Rigidbody>().AddForce((hitDir * damage)* damageEffectMultiplyer);
    }

    private void Start()
    {
        jumpLock = false;
        canAttack = true;
        detectionSphere = transform.GetChild(0).gameObject.GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && GetComponent<Rigidbody>().velocity.y == 0 && !attacking && !jumpLock)
        {
            GetComponent<LaunchController>().Launch(other.transform.position);
            StartCoroutine(Attack());
            attacking = false;
            StartCoroutine(JumpCooldown());
        }
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SlimeKilled();
            Destroy(this.gameObject);
        }

        //Attacking Logic
        if (attacking)
        {
            if (GetComponent<Rigidbody>().velocity.y == 0)
            {
                attacking = false;
            }
        }

        //Kill floor

        if (transform.position.y < -10)
        {
            health -= 999.0f;
        }

    }

    public IEnumerator Attack()
    {
        attacking = true;
        yield return null;
    }
    
    IEnumerator JumpCooldown()
    {
        jumpLock = true;
        yield return new WaitForSeconds(jumpCooldown);
        jumpLock = false;
    }

}
