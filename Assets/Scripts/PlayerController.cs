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

    private bool canFire = true;
    private Vector2 aimVec;
    private GameObject aimIndicator;

    void Start()
    {
        if (playerType == PLAYER.UNASSIGNED)
        {
            Debug.LogWarning("playerType of " + name + " is unassigned!");
        }

        aimIndicator = transform.GetChild(0).gameObject;

        //Reset vars
        canFire = true;

    }


    void Update()
    {
        //AIMMING
        Vector3 aimVec = new Vector3(0, 0, 0);
        aimVec = new Vector3(Input.GetAxisRaw("P" + (int)playerType + "AIMHOZ"), 0, -Input.GetAxisRaw("P" + (int)playerType + "AIMVERT"));

        float x = Mathf.Sqrt((aimVec.x * aimVec.x) + (aimVec.z * aimVec.z));
        x = 1 / x;
        aimVec = new Vector3(aimVec.x * x, aimVec.y, aimVec.z * x);

        Debug.Log(aimVec);

        aimIndicator.transform.localPosition = aimVec * aimDistance;

        //MOVEMENT

        Vector3 movementVec = new Vector3(0, 0, 0);

        if (Input.GetAxisRaw("P" + (int)playerType + "VERT") != 0)
        {
            movementVec += transform.forward * -Input.GetAxisRaw("P" + (int)playerType + "VERT");
        }

        if (Input.GetAxisRaw("P" + (int)playerType + "HOZ") != 0)
        {
            movementVec += transform.right * Input.GetAxisRaw("P" + (int)playerType + "HOZ");
        }

        if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
        {
            GetComponent<Rigidbody>().AddForce(movementVec * speed);
        }

    }

    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(fireCoolDown);
    }

}
