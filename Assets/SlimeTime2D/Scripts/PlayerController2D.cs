using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController2D : MonoBehaviour
{
    public enum PLAYER{
        UNASSIGNED,
        PLAYER1,
        PLAYER2,
        PLAYER3,
        PLAYER4
    }

    public PLAYER playerType = PLAYER.UNASSIGNED;

    public float speed = 10.0f;
    public float maxSpeed = 30.0f;
    public float fireForce = 0.1f;
    public float fireRate = 0.2f;
    public float aimDistance = 1.0f;
    public GameObject projectilePrefab;

    public bool facingRight = true;
    private bool canFire = true;

    void Start()
    {

        playerType = (PLAYER)GetComponent<PlayerManager>().playerNo + 1;

        //assign to correct player here

        //sanity checks here
        if (playerType == PLAYER.UNASSIGNED)
        {
            Debug.LogWarning("playerType of " + name + " is unassigned!");
        }
        else if (playerType == PLAYER.PLAYER1)
        {
            tag = "P1";
        }
        else if (playerType == PLAYER.PLAYER2)
        {
            tag = "P2";
        }
        else if (playerType == PLAYER.PLAYER3)
        {
            tag = "P3";
        }
        else if (playerType == PLAYER.PLAYER4)
        {
            tag = "P4";
        }
        //Reset vars
        facingRight = true;
        canFire = true;
    }


    void FixedUpdate()
    {
        //AIM UI
        Vector3 aimVec = new Vector3(0,0,0);
        if (playerType == PLAYER.PLAYER1)
        {
            aimVec = new Vector3(Input.GetAxisRaw("P1ATTACKHOZ"), -Input.GetAxisRaw("P1ATTACKVERT"), 0);
        }
        else if (playerType == PLAYER.PLAYER2)
        {
            aimVec = new Vector3(Input.GetAxisRaw("P2ATTACKHOZ"), -Input.GetAxisRaw("P2ATTACKVERT"), 0);
        }
        else if (playerType == PLAYER.PLAYER3)
        {
            aimVec = new Vector3(Input.GetAxisRaw("P3ATTACKHOZ"), -Input.GetAxisRaw("P3ATTACKVERT"), 0);
        }
        else if (playerType == PLAYER.PLAYER4)
        {
            aimVec = new Vector3(Input.GetAxisRaw("P4ATTACKHOZ"), -Input.GetAxisRaw("P4ATTACKVERT"), 0);
        }
        float x = Mathf.Sqrt((aimVec.x * aimVec.x) + (aimVec.y * aimVec.y));
        x = 1 / x;
        aimVec = new Vector3(aimVec.x * x, aimVec.y * x, aimVec.z);
        transform.GetChild(1).transform.position = transform.position + (aimVec * aimDistance);

    }

    void Update()
    {
        Vector3 movementVec = new Vector3(0,0,0);

        //
        //GAMEPLAY INPUT
        //
        //PLAYER1
        //VERT
        if (((playerType == PLAYER.PLAYER1) && (Input.GetAxisRaw("P1MOVEVERT") != 0)) || ((playerType == PLAYER.PLAYER1) && (Input.GetAxisRaw("DEBUGP1MOVEVERT") != 0)))
        {
            if (Input.GetAxisRaw("P1MOVEVERT") != 0)
            {
                movementVec += transform.up * -Input.GetAxisRaw("P1MOVEVERT");
            }
            else
            {
                movementVec += transform.up * -Input.GetAxisRaw("DEBUGP1MOVEVERT");
            }
            
        }

        //HOZ
        if (((playerType == PLAYER.PLAYER1) && (Input.GetAxisRaw("P1MOVEHOZ") != 0)) || ((playerType == PLAYER.PLAYER1) && (Input.GetAxisRaw("DEBUGP1MOVEHOZ") != 0)))
        {
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                if (Input.GetAxisRaw("P1MOVEHOZ") != 0)
                {
                    if (Input.GetAxisRaw("P1MOVEHOZ") < 0)
                    {
                        facingRight = false;
                    }
                    else
                    {
                        facingRight = true;
                    }
                }
                else
                {
                    if (Input.GetAxisRaw("DEBUGP1MOVEHOZ") < 0)
                    {
                        facingRight = false;
                    }
                    else
                    {
                        facingRight = true;
                    }
                }
            }
            movementVec += transform.right * Input.GetAxisRaw("DEBUGP1MOVEHOZ") + transform.right * Input.GetAxisRaw("P1MOVEHOZ");
        }

        //ATTACK

        if (((playerType == PLAYER.PLAYER1) && ((Input.GetAxisRaw("P1ATTACKVERT")) != 0 || Input.GetAxisRaw("P1ATTACKHOZ") != 0) && canFire && SceneManager.GetActiveScene().buildIndex != 1) || ((playerType == PLAYER.PLAYER1) && (Input.GetAxisRaw("DEBUGP1ATTACK")) != 0 && canFire && SceneManager.GetActiveScene().buildIndex != 1))
        {
            if (GetComponent<PlayerManager>().health > 0)
            {
                Vector3 shootVec;
                if ((Input.GetAxisRaw("P1ATTACKVERT")) != 0 || Input.GetAxisRaw("P1ATTACKHOZ") != 0)
                {
                    shootVec = ((transform.right * Input.GetAxisRaw("P1ATTACKHOZ")) + transform.up * -Input.GetAxisRaw("P1ATTACKVERT"));
                }
                else
                {
                    shootVec = ((transform.right * Input.GetAxisRaw("DEBUGP1MOVEHOZ")) + transform.up * -Input.GetAxisRaw("DEBUGP1MOVEVERT"));
                }
                Vector3 testVec = shootVec;
                if (testVec.x < 0)
                {
                    testVec.x *= -1;
                }
                if (testVec.y < 0)
                {
                    testVec.y *= -1;
                }
                if ((testVec.x + testVec.y) > 0.9)
                {
                    GameObject proRef = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    proRef.GetComponent<ProjectileController>().SetUp(gameObject, shootVec);
                    StartCoroutine(fireCoolDown());
                }
            }
        }

        //PLAYER2
        //VERT
        if (((playerType == PLAYER.PLAYER2) && (Input.GetAxisRaw("P2MOVEVERT") != 0)) || ((playerType == PLAYER.PLAYER2) && (Input.GetAxisRaw("DEBUGP2MOVEVERT") != 0)))
        {
            if (Input.GetAxisRaw("P2MOVEVERT") != 0)
            {
                movementVec += transform.up * -Input.GetAxisRaw("P2MOVEVERT");
            }
            else
            {
                movementVec += transform.up * -Input.GetAxisRaw("DEBUGP2MOVEVERT");
            }

        }

        //HOZ
        if (((playerType == PLAYER.PLAYER2) && (Input.GetAxisRaw("P2MOVEHOZ") != 0)) || ((playerType == PLAYER.PLAYER2) && (Input.GetAxisRaw("DEBUGP2MOVEHOZ") != 0)))
        {
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                if (Input.GetAxisRaw("P2MOVEHOZ") != 0)
                {
                    if (Input.GetAxisRaw("P2MOVEHOZ") < 0)
                    {
                        facingRight = false;
                    }
                    else
                    {
                        facingRight = true;
                    }
                }
                else
                {
                    if (Input.GetAxisRaw("DEBUGP2MOVEHOZ") < 0)
                    {
                        facingRight = false;
                    }
                    else
                    {
                        facingRight = true;
                    }
                }
            }
            movementVec += transform.right * Input.GetAxisRaw("DEBUGP2MOVEHOZ") + transform.right * Input.GetAxisRaw("P2MOVEHOZ");
        }

        //ATTACK

        if (((playerType == PLAYER.PLAYER2) && ((Input.GetAxisRaw("P2ATTACKVERT")) != 0 || Input.GetAxisRaw("P2ATTACKHOZ") != 0) && canFire && SceneManager.GetActiveScene().buildIndex != 1) || ((playerType == PLAYER.PLAYER2) && (Input.GetAxisRaw("DEBUGP2ATTACK")) != 0 && canFire && SceneManager.GetActiveScene().buildIndex != 1))
        {
            if (GetComponent<PlayerManager>().health > 0)
            {
                Vector3 shootVec;
                if ((Input.GetAxisRaw("P2ATTACKVERT")) != 0 || Input.GetAxisRaw("P2ATTACKHOZ") != 0)
                {
                    shootVec = ((transform.right * Input.GetAxisRaw("P2ATTACKHOZ")) + transform.up * -Input.GetAxisRaw("P2ATTACKVERT"));
                }
                else
                {
                    shootVec = ((transform.right * Input.GetAxisRaw("DEBUGP2MOVEHOZ")) + transform.up * -Input.GetAxisRaw("DEBUGP2MOVEVERT"));
                }
                Vector3 testVec = shootVec;
                if (testVec.x < 0)
                {
                    testVec.x *= -1;
                }
                if (testVec.y < 0)
                {
                    testVec.y *= -1;
                }
                if ((testVec.x + testVec.y) > 0.9)
                {
                    GameObject proRef = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    proRef.GetComponent<ProjectileController>().SetUp(gameObject, shootVec);
                    StartCoroutine(fireCoolDown());
                }
            }
        }

        //PLAYER3
        //VERT
        if (((playerType == PLAYER.PLAYER3) && (Input.GetAxisRaw("P3MOVEVERT") != 0)) || ((playerType == PLAYER.PLAYER3) && (Input.GetAxisRaw("DEBUGP3MOVEVERT") != 0)))
        {
            if (Input.GetAxisRaw("P3MOVEVERT") != 0)
            {
                movementVec += transform.up * -Input.GetAxisRaw("P3MOVEVERT");
            }
            else
            {
                movementVec += transform.up * -Input.GetAxisRaw("DEBUGP3MOVEVERT");
            }

        }

        //HOZ
        if (((playerType == PLAYER.PLAYER3) && (Input.GetAxisRaw("P3MOVEHOZ") != 0)) || ((playerType == PLAYER.PLAYER3) && (Input.GetAxisRaw("DEBUGP3MOVEHOZ") != 0)))
        {
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                if (Input.GetAxisRaw("P3MOVEHOZ") != 0)
                {
                    if (Input.GetAxisRaw("P3MOVEHOZ") < 0)
                    {
                        facingRight = false;
                    }
                    else
                    {
                        facingRight = true;
                    }
                }
                else
                {
                    if (Input.GetAxisRaw("DEBUGP3MOVEHOZ") < 0)
                    {
                        facingRight = false;
                    }
                    else
                    {
                        facingRight = true;
                    }
                }
            }
            movementVec += transform.right * Input.GetAxisRaw("DEBUGP3MOVEHOZ") + transform.right * Input.GetAxisRaw("P3MOVEHOZ");
        }

        //ATTACK

        if (((playerType == PLAYER.PLAYER3) && ((Input.GetAxisRaw("P3ATTACKVERT")) != 0 || Input.GetAxisRaw("P3ATTACKHOZ") != 0) && canFire && SceneManager.GetActiveScene().buildIndex != 1) || ((playerType == PLAYER.PLAYER3) && (Input.GetAxisRaw("DEBUGP3ATTACK")) != 0 && canFire && SceneManager.GetActiveScene().buildIndex != 1))
        {
            if (GetComponent<PlayerManager>().health > 0)
            {
                Vector3 shootVec;
                if ((Input.GetAxisRaw("P3ATTACKVERT")) != 0 || Input.GetAxisRaw("P3ATTACKHOZ") != 0)
                {
                    shootVec = ((transform.right * Input.GetAxisRaw("P3ATTACKHOZ")) + transform.up * -Input.GetAxisRaw("P3ATTACKVERT"));
                }
                else
                {
                    shootVec = ((transform.right * Input.GetAxisRaw("DEBUGP3MOVEHOZ")) + transform.up * -Input.GetAxisRaw("DEBUGP3MOVEVERT"));
                }
                Vector3 testVec = shootVec;
                if (testVec.x < 0)
                {
                    testVec.x *= -1;
                }
                if (testVec.y < 0)
                {
                    testVec.y *= -1;
                }
                if ((testVec.x + testVec.y) > 0.9)
                {
                    GameObject proRef = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    proRef.GetComponent<ProjectileController>().SetUp(gameObject, shootVec);
                    StartCoroutine(fireCoolDown());
                }
            }
        }
        //PLAYER4
        //VERT
        if (((playerType == PLAYER.PLAYER4) && (Input.GetAxisRaw("P4MOVEVERT") != 0)) || ((playerType == PLAYER.PLAYER4) && (Input.GetAxisRaw("DEBUGP4MOVEVERT") != 0)))
        {
            if (Input.GetAxisRaw("P4MOVEVERT") != 0)
            {
                movementVec += transform.up * -Input.GetAxisRaw("P4MOVEVERT");
            }
            else
            {
                movementVec += transform.up * -Input.GetAxisRaw("DEBUGP4MOVEVERT");
            }

        }

        //HOZ
        if (((playerType == PLAYER.PLAYER4) && (Input.GetAxisRaw("P4MOVEHOZ") != 0)) || ((playerType == PLAYER.PLAYER4) && (Input.GetAxisRaw("DEBUGP4MOVEHOZ") != 0)))
        {
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                if (Input.GetAxisRaw("P4MOVEHOZ") != 0)
                {
                    if (Input.GetAxisRaw("P4MOVEHOZ") < 0)
                    {
                        facingRight = false;
                    }
                    else
                    {
                        facingRight = true;
                    }
                }
                else
                {
                    if (Input.GetAxisRaw("DEBUGP4MOVEHOZ") < 0)
                    {
                        facingRight = false;
                    }
                    else
                    {
                        facingRight = true;
                    }
                }
            }
            movementVec += transform.right * Input.GetAxisRaw("DEBUGP4MOVEHOZ") + transform.right * Input.GetAxisRaw("P4MOVEHOZ");
        }

        //ATTACK

        if (((playerType == PLAYER.PLAYER4) && ((Input.GetAxisRaw("P4ATTACKVERT")) != 0 || Input.GetAxisRaw("P4ATTACKHOZ") != 0) && canFire && SceneManager.GetActiveScene().buildIndex != 1) || ((playerType == PLAYER.PLAYER4) && (Input.GetAxisRaw("DEBUGP4ATTACK")) != 0 && canFire && SceneManager.GetActiveScene().buildIndex != 1))
        {
            if (GetComponent<PlayerManager>().health > 0)
            {
                Vector3 shootVec;
                if ((Input.GetAxisRaw("P4ATTACKVERT")) != 0 || Input.GetAxisRaw("P4ATTACKHOZ") != 0)
                {
                    shootVec = ((transform.right * Input.GetAxisRaw("P4ATTACKHOZ")) + transform.up * -Input.GetAxisRaw("P4ATTACKVERT"));
                }
                else
                {
                    shootVec = ((transform.right * Input.GetAxisRaw("DEBUGP4MOVEHOZ")) + transform.up * -Input.GetAxisRaw("DEBUGP4MOVEVERT"));
                }
                Vector3 testVec = shootVec;
                if (testVec.x < 0)
                {
                    testVec.x *= -1;
                }
                if (testVec.y < 0)
                {
                    testVec.y *= -1;
                }
                if ((testVec.x + testVec.y) > 0.9)
                {
                    GameObject proRef = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                    proRef.GetComponent<ProjectileController>().SetUp(gameObject, shootVec);
                    StartCoroutine(fireCoolDown());
                }
            }
        }

        //Add the force onto the rigidbody so we move

        if (GetComponent<Rigidbody2D>().velocity.magnitude < maxSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(movementVec * speed);
        }

        //make sprite face correct direction
        if (facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        //Debug.Log("RAW: " + Input.GetAxisRaw("DEBUGP1MOVEVERT") + " " + Input.GetAxisRaw("DEBUGP1MOVEHOZ") + " " + Input.GetAxisRaw("DEBUGP1ATTACK"));    
    }

    IEnumerator fireCoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

}
