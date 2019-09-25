using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_animation : MonoBehaviour
{
    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
           if (Input.GetKey(KeyCode.D))
        {
            m_Animator.ResetTrigger("Idle");

            m_Animator.SetTrigger("pressD");
        }

   
    }
}
