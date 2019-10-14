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
    public List<AudioClip> slimeHurtSounds = new List<AudioClip>();

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
        GetComponent<AudioSource>().clip = slimeHurtSounds[Random.Range(0, slimeHurtSounds.Count - 1)];
        GetComponent<AudioSource>().Play();
    }

    private void Start()
    {
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

        slimeObjects[(int)type].SetActive(true);

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
        idleThreshold = Random.Range(idleThresholdRange.x, idleThresholdRange.y);


        //Generate a launch position
        Vector3 launchPos = new Vector3(Random.Range(transform.position.x - wanderRange, transform.position.x + wanderRange), Random.Range(transform.position.y - wanderRange, transform.position.y + wanderRange), Random.Range(transform.position.z - wanderRange, transform.position.z + wanderRange));

        //Check if position has ground

        RaycastHit hit;

        if (Physics.Raycast(launchPos, Vector3.down, out hit, Mathf.Infinity))
        {

            if (hit.collider.gameObject.tag != "DETECTIONIDSAVULTIMATE360.msi")
            {
                //Launch if it does
                Debug.Log("Jumping because " + hit.collider.gameObject.name + " is a floor");
                GetComponent<LaunchController>().Launch(launchPos);
                idleTimer = 0;
            }
        }



        //Otherwise do not reset timer and let it loop

        yield return null;
    }

}
