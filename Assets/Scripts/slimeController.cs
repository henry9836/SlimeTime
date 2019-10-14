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

    public float maxJumpDistance = 10.0f;
    public float health = 0;
    public float damageEffectMultiplyer = 1.0f;
    public float attackDamage = 1.0f;
    public float attackCooldown = 3.0f;
    public float attackEffectMultiplyer = 100.0f;
    public Vector2 jumpCooldown = new Vector2(0.0f, 0.0f);
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
    private GameObject tarPlayer;
    private float closestDistance;

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
        jumpCooldown = GameObject.Find("GameManager").GetComponent<slimeSpawner>().jumpCooldown;
        health = GameObject.Find("GameManager").GetComponent<slimeSpawner>().health;
        detectionSphere = transform.GetChild(0).gameObject.GetComponent<SphereCollider>();
        idleThreshold = Random.Range(idleThresholdRange.x, idleThresholdRange.y);
    }

    bool CheckLaunchPos(Vector3 pos)
    {
        RaycastHit hit;

        if (Physics.Raycast(pos, Vector3.down, out hit, Mathf.Infinity))
        {
            //if we didn't hit the camera layer
            if (hit.collider.gameObject.layer != 12)
            {
                //We found a valid spot
                return true;
            }
        }
        return false;
    }

    Vector3 FindValidTargetPosition()
    {
        Vector3 result = Vector3.zero;

        Vector3 targetDir = new Vector3(tarPlayer.transform.position.x - transform.position.x, tarPlayer.transform.position.y - transform.position.y, tarPlayer.transform.position.z - transform.position.z).normalized;

        //Can we go straight in player direction?

        if (CheckLaunchPos(transform.position + (targetDir * maxJumpDistance)))
        {
            result = transform.position + (targetDir * maxJumpDistance);
        }
        //If we cannot go in a straight direction?
        else
        {
            for (int i = 0; i < 180; i++)
            {
                if (CheckLaunchPos(transform.position + (targetDir * maxJumpDistance)))
                {

                }
            }
        }
        return result;
    }

    void Attack()
    {
        attacking = true;

        if (Vector3.Distance(tarPlayer.transform.position, transform.position) > maxJumpDistance)
        {
            //Player is too far away find a nearby position that is safe to get closer to player
            GetComponent<LaunchController>().Launch(FindValidTargetPosition());
        }
        else
        {
            //Player is here so floor is also present we are safe to jump
            GetComponent<LaunchController>().Launch(tarPlayer.transform.position);
        }
    }

    private void FixedUpdate()
    {

        slimeObjects[(int)type].SetActive(true);

        //Health Logic
        if (health <= 0)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SlimeKilled();
            Destroy(this.gameObject);
        }

        //Kill floor

        if (transform.position.y < -10)
        {
            health -= 999.0f;
        }

        //Are we on the ground?
        if (GetComponent<Rigidbody>().velocity.y == 0)
        {
            attacking = false;
            //Is the jump cooldown over?
            if (!jumpLock)
            {
                //restart the jumpcooldown
                StartCoroutine(JumpCooldown());
                //Find the closest player
                closestDistance = Mathf.Infinity;
                tarPlayer = null;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < players.Length-1; i++)
                {
                    //Check health
                    if (players[i].GetComponent<PlayerController>().health > 0)
                    {
                        //Check distance
                        if (Vector3.Distance(players[i].transform.position, transform.position) < closestDistance)
                        {
                            //Set player to target
                            tarPlayer = players[i];
                            closestDistance = Vector3.Distance(players[i].transform.position, transform.position);
                        }
                    }
                }

                //Launch at closest player
                if (tarPlayer != null)
                {
                    Attack();
                }

            }
        }

    }
    
    IEnumerator JumpCooldown()
    {
        jumpLock = true;
        yield return new WaitForSeconds(Random.Range(jumpCooldown.x, jumpCooldown.y));
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

            if (hit.collider.gameObject.layer != 12)
            {
                //Launch if it does
                GetComponent<LaunchController>().Launch(launchPos);
                idleTimer = 0;
            }
        }



        //Otherwise do not reset timer and let it loop

        yield return null;
    }

}
