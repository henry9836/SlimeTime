//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI; 

//public class timer : MonoBehaviour
//{
//  //  private GameManager manager;
//    private bool once = true;
//    void Start()
//    {
//        manager = GameObject.Find("gamemanagerPrefab").GetComponent<GameManager>();
//        this.GetComponent<Image>().fillAmount = 0.0f;

//    }


//    void Update()
//    {
//        if (manager.killPhaseActive != true)
//        {
//            this.GetComponent<Image>().fillAmount = (manager.toggleRoundTime - manager.gameTime) / manager.toggleRoundTime;
//        }
//        else
//        {
//            if (once == true)
//            {
//                once = false;
//                this.GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f, this.GetComponent<Image>().color.a);
//            }
//            this.GetComponent<Image>().fillAmount = (manager.gameTime - manager.toggleRoundTime) / (manager.maxGameTime - manager.toggleRoundTime);

//        }
//    }
//}
