using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public enum PROJECTILETYPES
    {
        ARROW,
        MAGICSTRIKE,
        NOTE,
        AXE
    }

    private PlayerController.PLAYER playerCaster = PlayerController.PLAYER.UNASSIGNED;

    public PROJECTILETYPES type;
    public float spinSpeed = 10.0f;
    public float damage = 1.0f;
    public GameObject caster;
    public Vector3 direction;

    private int noteChoice = 0;

    public void SetUp(GameObject _caster, Vector3 dir)
    {
        caster = _caster;
        playerCaster = caster.GetComponent<PlayerController>().playerType;
        direction = dir;


        float angle = 0;
        angle = 0;

        angle = Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x);
        if (dir.x < 0)
        {
            angle += 180;
        }
        Debug.Log(angle);
        transform.Rotate(0,0,angle);

        if (_caster.GetComponent<PlayerManager>().character == 0) //mage
        {
            type = PROJECTILETYPES.MAGICSTRIKE;
        }
        else if (_caster.GetComponent<PlayerManager>().character == 1) //warr
        {
            type = PROJECTILETYPES.AXE;
        }
        else if (_caster.GetComponent<PlayerManager>().character == 2) //archer
        {
            type = PROJECTILETYPES.ARROW;
        }
        else if (_caster.GetComponent<PlayerManager>().character == 3) //barb
        {
            type = PROJECTILETYPES.NOTE;
            noteChoice = Random.Range(0, 2);
        }

        StartCoroutine(KillMe());
    }

    //when the projectile hits something
    public void OnChildTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != gameObject && collision.gameObject != caster) //Ignore ourselves as a collsion
        {
            //try and call something on the otherobject
            bool hitThing = false;
            if (collision.gameObject.tag == "Slime")
            {
                hitThing = true;
                collision.GetComponent<slime>().SlimeHit(((int)caster.GetComponent<PlayerController>().playerType)-1);
                collision.GetComponent<Rigidbody2D>().AddForce(direction * caster.GetComponent<PlayerController>().fireForce * 200000); //move when hit if not enough then change linear damper on rigidbody
            }
            else if (collision.gameObject.tag == "P1" || collision.gameObject.tag == "P2" || collision.gameObject.tag == "P3" || collision.gameObject.tag == "P4")
            {
                hitThing = true;
                collision.gameObject.GetComponent<PlayerManager>().hitMe(gameObject);
            }

            if (hitThing)
            {
                GameObject.Find("Main Camera").GetComponent<Screenshake>().ScreenBump(direction);
            }

            if (collision.gameObject.tag != "Wall")
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Update()
    {
        transform.position += direction * caster.GetComponent<PlayerController>().fireForce;
        //According to our projectile type to do behaviour
        if (type == PROJECTILETYPES.ARROW)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            //arrow does nothing fancy maybe a spin idk
        }
        else if (type == PROJECTILETYPES.AXE)
        {
            transform.GetChild(2).gameObject.SetActive(true);
            //spin
            transform.GetChild(2).transform.eulerAngles -= new Vector3(0, 0, spinSpeed);
        }
        else if (type == PROJECTILETYPES.MAGICSTRIKE)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            //spin
            //transform.GetChild(3).transform.eulerAngles += new Vector3(0, 0, spinSpeed);
        }
        else if (type == PROJECTILETYPES.NOTE)
        {
            transform.GetChild(4+noteChoice).gameObject.SetActive(true);
            //note does nothing fancy
        }

        //Sanity Checks
        if (direction == new Vector3(0, 0, 0))
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator KillMe()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
}
