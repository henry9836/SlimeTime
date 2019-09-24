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

    void Start()
    {
        if (playerType == PLAYER.UNASSIGNED)
        {
            Debug.LogWarning("playerType of " + name + " is unassigned!");
        }

        //Reset vars
        canFire = true;

    }


    void Update()
    {
        //AIM UI
        Vector3 aimVec = new Vector3(0, 0, 0);
        aimVec = new Vector3(Input.GetAxisRaw("P" + (int)playerType + "AIMHOZ"), -Input.GetAxisRaw("P" + (int)playerType + "AIMVERT"), 0);
        Debug.Log(aimVec);
    }

    IEnumerator coolDown()
    {
        yield return new WaitForSeconds(fireCoolDown);
    }

}
