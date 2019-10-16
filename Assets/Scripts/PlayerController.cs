using System.Collections;
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

    public Material wallMaterial;
    public AudioClip hurtSound;
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
    private float lastHealth;
    public projectileController.PROJTYPES projType = projectileController.PROJTYPES.ARROW;
    private Material initalMaterial;

    //Ragdoll Effects
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


        charcterType = (CHARACTER)characterSetter.playerSelections[((int)playerType) - 1];

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


       

        //set projType
        if (charcterType == CHARACTER.ARCHER)
        {
            projType = projectileController.PROJTYPES.ARROW;
        }
        else if (charcterType == CHARACTER.BARD)
        {
            projType = projectileController.PROJTYPES.NOTE;
        }
        else if (charcterType == CHARACTER.MAGE)
        {
            projType = projectileController.PROJTYPES.FIREBALL;
        }
        else if (charcterType == CHARACTER.WARRIOR)
        {
            projType = projectileController.PROJTYPES.AXE;
        }

        initalMaterial = playerMesh.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material;
    }


    void Update()
    {

        //Set material based on if we can find a camera

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");

        Vector3 camDir = new Vector3(cam.transform.position.x - transform.position.x, cam.transform.position.y - transform.position.y, cam.transform.position.z - transform.position.z).normalized;

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 0;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, camDir, out hit, Mathf.Infinity, layerMask))
        {

            if (hit.collider.gameObject.tag == "MainCamera")
            {
                Debug.DrawRay(transform.position, camDir * hit.distance, Color.green);
                playerMesh.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = initalMaterial;
            }
            else
            {
                Debug.DrawRay(transform.position, camDir * hit.distance, Color.red);
                playerMesh.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = wallMaterial;
            }
        }

        //ALIVE
        if (health > 0)
        {
            playerMesh.SetActive(true);
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;

            //AIM
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
                        refer.GetComponent<projectileController>().type = projType;
                        refer.GetComponent<projectileController>().playerRef = gameObject;
                        refer.GetComponent<Rigidbody>().AddForce(lastAimVec * fireForce);
                        refer.transform.LookAt(transform.position + (lastAimVec * 100.0f));
                        refer.GetComponent<projectileController>().travelDir = lastAimVec;
                        FlingYourArmsFromSideToSide();
                    }

                }
            }

            //MOVEMENT

            Vector3 movementVec = Vector3.zero;

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

            if ( movementVec == Vector3.zero)
            {
                //stop
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity / 1.2f;
            }

            if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
            {

                if (float.IsNaN((Mathf.Log(GetComponent<Rigidbody>().velocity.magnitude) + speed)) || float.IsInfinity((Mathf.Log(GetComponent<Rigidbody>().velocity.magnitude) + speed)))
                {
                    Debug.Log(" waoha you did a funky wunky");
                    GetComponent<Rigidbody>().AddForce(movementVec * speed);

                }
                else
                {
                    float dot = Vector3.Dot(GetComponent<Rigidbody>().velocity.normalized, movementVec.normalized);
                    if (dot > 0)
                    {
                        GetComponent<Rigidbody>().AddForce(movementVec * (5 * (Mathf.Exp(-(GetComponent<Rigidbody>().velocity.magnitude)) + 10)));
                    }
                    else
                    {
                        GetComponent<Rigidbody>().AddForce(movementVec * (((1 + (-dot))) * 5 * (Mathf.Exp(-(GetComponent<Rigidbody>().velocity.magnitude)) + 10)));
                    }


                }

            }

            //Hurt Sound

            if (health < lastHealth)
            {
                GetComponent<AudioSource>().clip = hurtSound;
                GetComponent<AudioSource>().Play();
            }

            lastHealth = health;
        }
        else
        {
            playerMesh.SetActive(false);
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;

            if (GameObject.Find("GameManager").GetComponent<GameManager>().CanRespawn == true)
            {
                health = 100f;
                transform.position = GameObject.Find("GameManager").GetComponent<GameManager>().respawnpos;
            }
        }

        if (transform.position.y <= -10)
        {
            transform.position = GameObject.Find("GameManager").GetComponent<GameManager>().respawnpos;
        }

        if (controllerNotBound) {
            health = -9999f;
        }
    }

    IEnumerator coolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCoolDown);
        canFire = true;
    }

}
