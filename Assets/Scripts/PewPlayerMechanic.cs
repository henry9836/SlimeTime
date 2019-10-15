using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPlayerMechanic : MonoBehaviour
{

    private GameObject playerRef;
    private float burstCoolDownTime = 0.1f;
    private float multicoolDownTime = 0.3f;
    private float spreadcoolDownTime = 0.3f;
    private float wallcoolDownTime = 0.3f;

    private bool canFire = true;

    private void Start()
    {
        //Set anything to correct values that need to
        multicoolDownTime = GetComponent<PlayerController>().fireCoolDown - 0.1f;
        spreadcoolDownTime = GetComponent<PlayerController>().fireCoolDown - 0.1f;
        wallcoolDownTime = GetComponent<PlayerController>().fireCoolDown - 0.1f;
    }

    public void pew(Pickups.POWERUPS powerup, GameObject _playerRef)
    {

        playerRef = _playerRef;

        Debug.Log("PEW");

        if (powerup == Pickups.POWERUPS.RAPIDFIRE)
        {
            if (canFire)
            {
                GameObject refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position, Quaternion.identity);
                refer.GetComponent<projectileController>().type = _playerRef.GetComponent<PlayerController>().projType;
                refer.GetComponent<Rigidbody>().AddForce(playerRef.GetComponent<PlayerController>().lastAimVec * playerRef.GetComponent<PlayerController>().fireForce);
                refer.transform.LookAt(transform.position + (playerRef.GetComponent<PlayerController>().lastAimVec * 100.0f));
                refer.GetComponent<projectileController>().travelDir = playerRef.GetComponent<PlayerController>().lastAimVec;
                playerRef.GetComponent<PlayerController>().pickupAmmoCount--;

                StartCoroutine(burstCoolDown());
            }
        }

        if (powerup == Pickups.POWERUPS.MULTISHOTT1 || powerup == Pickups.POWERUPS.MULTISHOTT2)
        {
            if (canFire)
            {

                //Middle

                GameObject refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position, Quaternion.identity);
                refer.GetComponent<projectileController>().type = _playerRef.GetComponent<PlayerController>().projType;
                refer.GetComponent<Rigidbody>().AddForce(playerRef.GetComponent<PlayerController>().lastAimVec * playerRef.GetComponent<PlayerController>().fireForce);
                refer.transform.LookAt(transform.position + (playerRef.GetComponent<PlayerController>().lastAimVec * 100.0f));
                refer.GetComponent<projectileController>().travelDir = playerRef.GetComponent<PlayerController>().lastAimVec;

                //Right

                refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position, Quaternion.identity);
                refer.GetComponent<projectileController>().type = _playerRef.GetComponent<PlayerController>().projType;
                refer.GetComponent<Rigidbody>().AddForce((playerRef.GetComponent<PlayerController>().lastAimVec + (transform.right * 0.13f)) * playerRef.GetComponent<PlayerController>().fireForce);
                refer.transform.LookAt(transform.position + ((playerRef.GetComponent<PlayerController>().lastAimVec + (transform.right * 0.13f)) * 100.0f));
                refer.GetComponent<projectileController>().travelDir = playerRef.GetComponent<PlayerController>().lastAimVec + (transform.right * 0.13f);

                //Left

                refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position, Quaternion.identity);
                refer.GetComponent<projectileController>().type = _playerRef.GetComponent<PlayerController>().projType;
                refer.GetComponent<Rigidbody>().AddForce((playerRef.GetComponent<PlayerController>().lastAimVec + (-transform.right * 0.13f)) * playerRef.GetComponent<PlayerController>().fireForce);
                refer.transform.LookAt(transform.position + ((playerRef.GetComponent<PlayerController>().lastAimVec + (-transform.right * 0.13f)) * 100.0f));
                refer.GetComponent<projectileController>().travelDir = playerRef.GetComponent<PlayerController>().lastAimVec + (-transform.right * 0.13f);

                if (powerup == Pickups.POWERUPS.MULTISHOTT2) {

                    //RightT2

                    refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position, Quaternion.identity);
                    refer.GetComponent<projectileController>().type = _playerRef.GetComponent<PlayerController>().projType;
                    refer.GetComponent<Rigidbody>().AddForce((playerRef.GetComponent<PlayerController>().lastAimVec + (transform.right * 0.25f)) * playerRef.GetComponent<PlayerController>().fireForce);
                    refer.transform.LookAt(transform.position + ((playerRef.GetComponent<PlayerController>().lastAimVec + (transform.right * 0.25f)) * 100.0f));
                    refer.GetComponent<projectileController>().travelDir = playerRef.GetComponent<PlayerController>().lastAimVec + (transform.right * 0.25f);

                    //LeftT2

                    refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position, Quaternion.identity);
                    refer.GetComponent<projectileController>().type = _playerRef.GetComponent<PlayerController>().projType;
                    refer.GetComponent<Rigidbody>().AddForce((playerRef.GetComponent<PlayerController>().lastAimVec + (-transform.right * 0.25f)) * playerRef.GetComponent<PlayerController>().fireForce);
                    refer.transform.LookAt(transform.position + ((playerRef.GetComponent<PlayerController>().lastAimVec + (-transform.right * 0.25f)) * 100.0f));
                    refer.GetComponent<projectileController>().travelDir = playerRef.GetComponent<PlayerController>().lastAimVec + (-transform.right * 0.25f);
                }

                playerRef.GetComponent<PlayerController>().pickupAmmoCount--;


                StartCoroutine(multiCoolDown());
            }
        }

        if (powerup == Pickups.POWERUPS.SPREAD)
        {
            if (canFire)
            {

                int spawnOnAngle = 10;
                int directionOffset = 1000;

                for (int i = 0; i < 360; i++)
                {
                    if (i % spawnOnAngle == 0)
                    {
                        Vector3 directionVec;
                        directionVec.x = (playerRef.transform.position.x + directionOffset) * Mathf.Sin(i * Mathf.Deg2Rad);
                        directionVec.y = playerRef.transform.position.y;
                        directionVec.z = (playerRef.transform.position.z + directionOffset) * Mathf.Cos(i * Mathf.Deg2Rad);

                        GameObject refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position, Quaternion.identity);
                        refer.GetComponent<projectileController>().type = _playerRef.GetComponent<PlayerController>().projType;
                        refer.GetComponent<Rigidbody>().AddForce(directionVec.normalized * playerRef.GetComponent<PlayerController>().fireForce);
                        refer.transform.LookAt(directionVec);
                        refer.GetComponent<projectileController>().travelDir = directionVec.normalized;

                    }
                }

                playerRef.GetComponent<PlayerController>().pickupAmmoCount--;


                StartCoroutine(spreadCoolDown());
            }
        }
        if (powerup == Pickups.POWERUPS.WALLOFDEATH)
        {
            if (canFire)
            {
                int amountToSpawn = 30;

                for (int i = 0; i < amountToSpawn/2; i++)
                {
                    GameObject refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position + (playerRef.transform.right * i/2), Quaternion.identity);
                    refer.GetComponent<projectileController>().type = _playerRef.GetComponent<PlayerController>().projType;
                    refer.GetComponent<Rigidbody>().AddForce(playerRef.GetComponent<PlayerController>().lastAimVec * playerRef.GetComponent<PlayerController>().fireForce);
                    refer.transform.LookAt(transform.position + (playerRef.GetComponent<PlayerController>().lastAimVec * 100.0f));
                    refer.GetComponent<projectileController>().travelDir = playerRef.GetComponent<PlayerController>().lastAimVec;
                }
                for (int i = 0; i < amountToSpawn / 2; i++)
                {
                    GameObject refer = Instantiate(playerRef.GetComponent<PlayerController>().baseProjectile, transform.position + (-playerRef.transform.right * i/2), Quaternion.identity);
                    refer.GetComponent<projectileController>().type = _playerRef.GetComponent<PlayerController>().projType;
                    refer.GetComponent<Rigidbody>().AddForce(playerRef.GetComponent<PlayerController>().lastAimVec * playerRef.GetComponent<PlayerController>().fireForce);
                    refer.transform.LookAt(transform.position + (playerRef.GetComponent<PlayerController>().lastAimVec * 100.0f));
                    refer.GetComponent<projectileController>().travelDir = playerRef.GetComponent<PlayerController>().lastAimVec;
                }

                playerRef.GetComponent<PlayerController>().pickupAmmoCount--;
                StartCoroutine(wallCoolDown());
            }
        }
    }

    IEnumerator burstCoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(burstCoolDownTime);
        canFire = true;
    }

    IEnumerator multiCoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(multicoolDownTime);
        canFire = true;
    }

    IEnumerator spreadCoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(spreadcoolDownTime);
        canFire = true;
    }

    IEnumerator wallCoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(wallcoolDownTime);
        canFire = true;
    }
}
