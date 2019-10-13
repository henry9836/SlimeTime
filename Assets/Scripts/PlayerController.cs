﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public enum PLAYER
    {
        UNASSIGNED,
        PLAYER1,
        PLAYER2,
        PLAYER3,
        PLAYER4
    }

    public enum CHARACTER
    {
        ARCHER,
        MAGE,
        WARRIOR,
        BARD
    };

    public PLAYER playerType = PLAYER.UNASSIGNED;

    public float speed = 10.0f;
    public float maxSpeed = 30.0f;
    public float fireForce = 1.0f;
    public float fireCoolDown = 0.2f;
    public float aimDistance = 1.0f;
    public float health = 100.0f;
    public int pickupAmmoCount = 0;

    public GameObject baseProjectile;
    public GameObject playerMesh;
    public Vector2 ragEffectRange = new Vector2(-10.0f, 10.0f);
    public List<GameObject> playerEffectBonesARCHER = new List<GameObject>();
    public List<GameObject> playerEffectBonesMAGE = new List<GameObject>();
    public List<GameObject> playerEffectBonesWARRIOR = new List<GameObject>();
    public List<GameObject> playerEffectBonesBARB = new List<GameObject>();
    public List<GameObject> playerRagdolls = new List<GameObject>();

    public CHARACTER charcterType = CHARACTER.ARCHER;
    public Pickups.POWERUPS powerupType = Pickups.POWERUPS.NULL;

    public bool controllerNotBound = false;

    private bool canFire = true;
    private Vector3 aimVec;
    public Vector3 lastAimVec;
    private GameObject aimIndicator;

    public void FlingYourArmsFromSideToSide()
    {
        if (charcterType == CHARACTER.ARCHER)
        {
            for (int i = 0; i < playerEffectBonesARCHER.Count; i++)
            {
                playerEffectBonesARCHER[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(ragEffectRange.x, ragEffectRange.y), Random.Range(ragEffectRange.x, ragEffectRange.y), Random.Range(ragEffectRange.x, ragEffectRange.y));
            }
        }
        else if (charcterType == CHARACTER.MAGE)
        {
            for (int i = 0; i < playerEffectBonesMAGE.Count; i++)
            {
                playerEffectBonesMAGE[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(ragEffectRange.x, ragEffectRange.y), Random.Range(ragEffectRange.x, ragEffectRange.y), Random.Range(ragEffectRange.x, ragEffectRange.y));
            }
        }
        else if (charcterType == CHARACTER.WARRIOR)
        {
            for (int i = 0; i < playerEffectBonesWARRIOR.Count; i++)
            {
                playerEffectBonesWARRIOR[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(ragEffectRange.x, ragEffectRange.y), Random.Range(ragEffectRange.x, ragEffectRange.y), Random.Range(ragEffectRange.x, ragEffectRange.y));
            }
        }
        else if (charcterType == CHARACTER.BARD)
        {
            for (int i = 0; i < playerEffectBonesBARB.Count; i++)
            {
                playerEffectBonesBARB[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(ragEffectRange.x, ragEffectRange.y), Random.Range(ragEffectRange.x, ragEffectRange.y), Random.Range(ragEffectRange.x, ragEffectRange.y));
            }
        }
    }

    void Start()
    {
        if (playerType == PLAYER.UNASSIGNED)
        {
            Debug.LogWarning("playerType of " + name + " is unassigned!");
        }

        if (baseProjectile == null)
        {
            Debug.LogWarning("Base Projectile not set on player: " + name);
        }

        if (playerRagdolls[(int)charcterType] != null)
        {
            playerRagdolls[(int)charcterType].SetActive(true);
            playerMesh = playerRagdolls[(int)charcterType];
            Debug.Log(charcterType);
        }
        else
        {
            Debug.LogWarning("Could not find a gameobject for character on player: " + name);
            playerRagdolls[0].SetActive(true);
            playerMesh = playerRagdolls[0];
        }

        aimIndicator = transform.GetChild(0).gameObject;

        //Reset vars
        canFire = true;

        lastAimVec = transform.forward;

    }


    void Update()
    {

        //ALIVE
        if (health > 0)
        {
            playerMesh.SetActive(true);
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;

            //AIMMING
            Vector3 aimVec = new Vector3(0, 0, 0);
            aimVec = new Vector3(Input.GetAxisRaw("P" + (int)playerType + "AIMHOZ"), 0, -Input.GetAxisRaw("P" + (int)playerType + "AIMVERT"));

            float x = Mathf.Sqrt((aimVec.x * aimVec.x) + (aimVec.z * aimVec.z));
            x = 1 / x;
            aimVec = new Vector3(aimVec.x * x, aimVec.y, aimVec.z * x);

            // Debug.Log(aimVec);

            //Check for NaN
            if (float.IsNaN(aimVec.x) || float.IsNaN(aimVec.z))
            {
                aimVec = Vector3.zero;
            }
            else
            {
                lastAimVec = aimVec;
            }

            //aimIndicator.transform.localPosition = aimVec * aimDistance;
            transform.LookAt(transform.position + (lastAimVec * 100.0f));
            //SHOOTING

            if (Input.GetAxisRaw("P" + (int)playerType + "SHOOT") != 0 || Input.GetButton("P" + (int)playerType + "SHOOTALT"))
            {

                Debug.Log("Shoot");
                //If we have a pickup (pickup does cooldown)
                if (powerupType != Pickups.POWERUPS.NULL)
                {
                    if (pickupAmmoCount > 0)
                    {
                        GetComponent<PewPlayerMechanic>().pew(powerupType, gameObject);
                    }
                    else
                    {
                        powerupType = Pickups.POWERUPS.NULL; //Unassign powerup since we have run out of ammo and then use normal weapon
                    }
                }
                if (canFire)
                {
                    StartCoroutine(coolDown());
                    //If we do not have a pickup
                    if (powerupType == Pickups.POWERUPS.NULL)
                    {
                        GameObject refer = Instantiate(baseProjectile, transform.position, Quaternion.identity);
                        refer.GetComponent<Rigidbody>().AddForce(lastAimVec * fireForce);
                        refer.transform.LookAt(transform.position + (lastAimVec * 100.0f));
                        refer.GetComponent<projectileController>().travelDir = lastAimVec;
                        FlingYourArmsFromSideToSide();
                    }

                }
            }

            //MOVEMENT

            Vector3 movementVec = new Vector3(0, 0, 0);

            if (Input.GetAxisRaw("P" + (int)playerType + "VERT") != 0)
            {
                //movementVec += transform.forward * -Input.GetAxisRaw("P" + (int)playerType + "VERT");
                movementVec += Vector3.forward * -Input.GetAxisRaw("P" + (int)playerType + "VERT");
            }

            if (Input.GetAxisRaw("P" + (int)playerType + "HOZ") != 0)
            {
                //movementVec += transform.right * Input.GetAxisRaw("P" + (int)playerType + "HOZ");
                movementVec += Vector3.right * Input.GetAxisRaw("P" + (int)playerType + "HOZ");
            }

            if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
            {
                GetComponent<Rigidbody>().AddForce(movementVec * speed);
            }
        }
        else
        {
            playerMesh.SetActive(false);
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            if (GameObject.Find("GameManager").GetComponent<GameManager>().CanRespawn == true)
            {
                health = 100;
                transform.position = GameObject.Find("GameManager").GetComponent<GameManager>().respawnpos;
            }
        }

        if (transform.position.y <= -10)
        {
            transform.position = GameObject.Find("GameManager").GetComponent<GameManager>().respawnpos;
        }

        if (controllerNotBound) {
            health = -9999;
        }
    }

    IEnumerator coolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCoolDown);
        canFire = true;
    }

}
