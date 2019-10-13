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
    public Vector2 idleThresholdRange = new Vector2(1.0f, 5.0f);
    public float wanderRange = 5.0f;
    public List<GameObject> slimeObjects = new List<GameObject>();

    private float idleThreshold = 3.0f;
    private bool canAttack = true;
    private bool attacking = false;
    private bool jumpLock = false;
    private bool dectLock = false;
    private float idleTimer = 0.0f;
    private Collider detectionSphere;

    public void DamageSlime(float damage, Vector3 hitDir)
    {
        health -= damage;
        GetComponent<Rigidbody>().AddForce((hitDir * damage)* damageEffectMultiplyer);
    }

    private void Start()
    {

        //for (int i = 0; i < slimeObjects.Count; i++)
        //{
        //    if (i != (int)type)
        //    {
        //        slimeObjects[i].SetActive(false);
        //    }
        //}

        slimeObjects[(int)type].SetActive(true);

        jumpLock = false;
        canAttack = true;
        detectionSphere = transform.GetChild(0).gameObject.GetComponent<SphereCollider>();
        idleThreshold = Random.Range(idleThresholdRange.x, idleThresholdRange.y);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && GetComponent<Rigidbody>().velocity.y == 0 && !attacking && !jumpLock)
        {
            GetComponent<LaunchController>().Launch(other.transform.position);
            StartCoroutine(Attack());
            StartCoroutine(JumpCooldown());
            dectLock = true;
            idleTimer = 0;
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
                dectLock = false;
            }
        }
        //Wander Logic
        else
        {
            if (dectLock != true && GetComponent<Rigidbody>().velocity.y == 0)
            {
                idleTimer += Time.deltaTime;
                if (idleTimer > idleThreshold)
                {
                    StartCoroutine(Wander());
                }
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

    IEnumerator Wander()
    {
        idleTimer = 0;
        idleThreshold = Random.Range(idleThresholdRange.x, idleThresholdRange.y);
        GetComponent<LaunchController>().Launch(new Vector3(Random.Range(transform.position.x-wanderRange, transform.position.x + wanderRange), Random.Range(transform.position.y - wanderRange, transform.position.y + wanderRange), Random.Range(transform.position.z - wanderRange, transform.position.z + wanderRange)));
        yield return null;
    }

}
