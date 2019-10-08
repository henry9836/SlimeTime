using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPlayerMechanic : MonoBehaviour
{

    private GameObject playerRef;
    private float burstCoolDownTime = 0.1f;
    private float dashCoolDownTime = 0.5f;

    private bool canFire = true;

    public void pew(Pickups.POWERUPS powerup, GameObject _playerRef)
    {

        playerRef = _playerRef;

        Debug.Log("PEW");

        if (powerup == Pickups.POWERUPS.DASH)
        {
            if (canFire)
            {
                playerRef.GetComponent<Rigidbody>().AddForce(playerRef.transform.forward * 1000);
                playerRef.GetComponent<PlayerController>().pickupAmmoCount--;

                StartCoroutine(dashCoolDown());
            }
        }
        else if (powerup == Pickups.POWERUPS.SPRAY)
        {
            if (canFire)
            {
                GameObject refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position, Quaternion.identity);
                refer.GetComponent<Rigidbody>().AddForce(playerRef.GetComponent<PlayerController>().lastAimVec * playerRef.GetComponent<PlayerController>().fireForce);
                refer.transform.LookAt(transform.position + (playerRef.GetComponent<PlayerController>().lastAimVec * 100.0f));
                refer.GetComponent<projectileController>().travelDir = playerRef.GetComponent<PlayerController>().lastAimVec;
                playerRef.GetComponent<PlayerController>().pickupAmmoCount--;

                StartCoroutine(burstCoolDown());
            }
        }
    }

    IEnumerator burstCoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(burstCoolDownTime);
        canFire = true;
    }

    IEnumerator dashCoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(burstCoolDownTime);
        canFire = true;
    }

}
