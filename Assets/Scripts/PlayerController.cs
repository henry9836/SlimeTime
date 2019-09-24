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

    void Start()
    {
        
    }

}
