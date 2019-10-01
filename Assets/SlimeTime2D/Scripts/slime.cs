using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour
{
    public float size = 1.0f;

    public int startingHP;
    public int currentHP;
    public int score;
    public float moveSpeed = 0.1f;

    public GameObject explodeParticle;

    public Vector4 wanderArea;

    public List<AudioClip> HitSounds;
    public AudioClip SpawnSound;

    private Vector3 targetPos;
    private bool hurt = false;

    public void Init(int HP)
    {
        GetComponent<AudioSource>().clip = SpawnSound;
        GetComponent<AudioSource>().Play();
        startingHP = HP;
        currentHP = startingHP;
        this.transform.localScale = new Vector3(Mathf.Sqrt(startingHP) + 0.5f * size, Mathf.Sqrt(startingHP) + 0.5f * size, Mathf.Sqrt(startingHP) +0.5f * size);
        hurt = false;
        targetPos = new Vector3(Random.Range(wanderArea.x,wanderArea.y), Random.Range(wanderArea.z, wanderArea.w), 0);
    }

    public void FixedUpdate()
    {
        //Wander Time
        if (!hurt)
        {
            if (transform.position.x != targetPos.x && transform.position.y != targetPos.y)
            {
                Vector3 directionVec = targetPos - transform.position;
                directionVec = directionVec / directionVec.magnitude;
                transform.position += directionVec * moveSpeed;
            }
            else
            {
                targetPos = new Vector3(Random.Range(wanderArea.x, wanderArea.y), Random.Range(wanderArea.z, wanderArea.w), 0);
                Debug.Log("Made New Tar: " + targetPos);
            }
        }
    }

    public void SlimeHit(int playerno)
    {
        hurt = true;
        StartCoroutine(Recover());
        currentHP -= 1;
        this.transform.localScale = new Vector3(Mathf.Sqrt(currentHP) + 0.5f * size, Mathf.Sqrt(currentHP) + 0.5f * size, Mathf.Sqrt(currentHP) + 0.5f * size);

        GetComponent<Animator>().SetTrigger("Hit"); //start hit animation

        GetComponent<AudioSource>().clip = HitSounds[Random.Range(0, HitSounds.Count)];
        GetComponent<AudioSource>().Play();

        if (currentHP <= 0)
        {
          //  GameObject.Find("gamemanagerPrefab").GetComponent<slimeSpawner>().currentSlimes -= 1;

            Instantiate(explodeParticle, transform.position, Quaternion.identity);
            if (playerno == 0)
            {
                GameObject.FindWithTag("P1").GetComponent<PlayerManager>().updatescore(startingHP * 10);
                Destroy(this.gameObject);

            }
            if (playerno == 1)
            {
                GameObject.FindWithTag("P2").GetComponent<PlayerManager>().updatescore(startingHP * 10);
                Destroy(this.gameObject);

            }
            if (playerno == 2)
            {
                GameObject.FindWithTag("P3").GetComponent<PlayerManager>().updatescore(startingHP * 10);
                Destroy(this.gameObject);

            }
            if (playerno == 3)
            {
                GameObject.FindWithTag("P4").GetComponent<PlayerManager>().updatescore(startingHP * 10);
                Destroy(this.gameObject);

            }
        }
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(3.0f);
        hurt = false;
    }

    IEnumerator WaitAtWander()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));
        hurt = false;
    }
}
