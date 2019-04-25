using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI; //使用Unity UI程式庫。

public class time : MonoBehaviour
{
    public GameObject img;
    public GameObject panel;
    public static int time_int = 6;
    public AudioSource dandan;
    public Text time_UI;

    //private bool hitButton = false;
    // public GameObject car;

    void Update()
    {
        if (RollerCoaster.end == true)
        {
            time_UI.text = "Thank For You're Playing\nPress 'Space' To Restart\nPress 'Esc' To Exit";
            //time_UI.text.
        }
        if (RollerCoaster.end == false && RollerCoaster.RE == true)
        {
            time_UI.text = "Press 'G' To Start";
            RollerCoaster.RE = false;
        }
        if (time_int == 6)
        {

            if (Input.GetKeyDown(KeyCode.G))
            {
                InvokeRepeating("timer", 1, 1);

            }
        }
        
    }
  
    void timer()
    {
  
        time_int -= 1;

        time_UI.text = time_int + "";

        if (time_int == 0)
        {
            dandan.Play();
            time_UI.text = "";
            RollerCoaster.speed = 100;
            Debug.Log("aaaaaa");
            CancelInvoke("timer");
            RollerCoaster.isStop = !RollerCoaster.isStop;
            time_int = -1;
            img.SetActive(false);
            panel.SetActive(false);


        }

    }

}
