using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
   
    public int playerNo; // 0-3

    //current score
    public int currentscore = 0;
    public bool damageable = false;
    public float health = 10.0f;
    public bool dead = false;

    public List<Sprite> Characters;
    public List<RuntimeAnimatorController> Animators;
    //0 = mage
    //1 = worrier
    //2 = archer
    //3 = bard

    public int character;

    public GameObject explosion;

    public float startHealth;

    public void hitMe(GameObject bullet)
    {
        if (damageable && !dead)
        {
            health -= bullet.GetComponent<ProjectileController>().damage;

            GetComponent<Rigidbody2D>().AddForce(bullet.GetComponent<ProjectileController>().direction * bullet.GetComponent<ProjectileController>().caster.GetComponent<PlayerController>().fireForce * 1000); //move when hit if not enough then change linear damper on rigidbody
            Instantiate(explosion, transform.position, Quaternion.identity);
            if (health <= 0)
            {
                bullet.GetComponent<ProjectileController>().caster.GetComponent<PlayerManager>().updatescore(150);
                updatescore(-150);
                StartCoroutine(DeadTimer());
            }
        }
    }

    void Start()
    {
        
        startHealth = health;
        damageable = false;
        dead = false;
        //fins what charter the plaer wanted to be and updates it
        SelectManager.selection = playerNo;
        character = SelectManager.playerchoice;
        //GetComponent<Animator>().runtimeAnimatorController = Animators[character];
        if (character == 0)
        {
            GetComponent<Animator>().runtimeAnimatorController = Animators[0];
            this.GetComponent<SpriteRenderer>().sprite = Characters[character];
            
        }
        if (character == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = Characters[character];
            GetComponent<Animator>().runtimeAnimatorController = Animators[1];

        }
        if (character == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = Characters[character];
            GetComponent<Animator>().runtimeAnimatorController = Animators[2];

        }
        if (character == 3)
        {
            this.GetComponent<SpriteRenderer>().sprite = Characters[character];
            GetComponent<Animator>().runtimeAnimatorController = Animators[3];

        }

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            //sets text to say what player they are 
            this.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "0";
        }

    }

    public void updatescore(int score)
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        { 
            currentscore += score;
            if (currentscore < 0)
            {
                currentscore = 0;
            }
            this.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = currentscore.ToString();
        }
    }

    IEnumerator DeadTimer()
    {
        dead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(3.0f);
        GetComponent<SpriteRenderer>().enabled = true;
        dead = false;
        health = startHealth;
    }

    IEnumerator FlashHurt()
    {
        for (int i = 0; i < 3; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f);
            yield return new WaitForSeconds(3.0f);
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
        }
        
    }
}
