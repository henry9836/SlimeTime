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

    public PLAYER playerType = PLAYER.UNASSIGNED;

    public float speed = 10.0f;
    public float maxSpeed = 30.0f;
    public float fireForce = 1.0f;
    public float fireCoolDown = 0.2f;
    public float aimDistance = 1.0f;
    public float health = 100.0f;

    public GameObject baseProjectile;
    public GameObject pickupSlot;

    public Pickups.POWERUPS powerupType;

    private bool canFire = true;
    private Vector3 aimVec;
    private Vector3 lastAimVec;
    private GameObject aimIndicator;

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

        aimIndicator = transform.GetChild(0).gameObject;

        //Reset vars
        canFire = true;

    }


    void Update()
    {
        //ALIVE
        if (health > 0)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
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
            transform.LookAt(transform.position + (aimVec * 100.0f));
            //SHOOTING

            if (Input.GetAxisRaw("P" + (int)playerType + "SHOOT") != 0 || Input.GetButton("P" + (int)playerType + "SHOOTALT"))
            {

                Debug.Log("Shoot");
                if (canFire)
                {
                    StartCoroutine(coolDown());
                    //If we do not have a pickup
                    if (pickupSlot == null)
                    {
                        GameObject refer = Instantiate(baseProjectile, transform.position, Quaternion.identity);
                        refer.GetComponent<Rigidbody>().AddForce(lastAimVec * fireForce);
                        refer.transform.LookAt(transform.position + (lastAimVec * 100.0f));
                        refer.GetComponent<projectileController>().travelDir = lastAimVec;
                    }
                    //If we have a pickup
                    else
                    {

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
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    IEnumerator coolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCoolDown);
        canFire = true;
    }

}
