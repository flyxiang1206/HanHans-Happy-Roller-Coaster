using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCoaster : MonoBehaviour
{
    public Rail rail;
    public Playmode Playmode;
    private float Addspeed = 0.0f;

    [SerializeField]
    public static float speed = 0.0f;
    public static bool isStop = true;
    public static bool end = false;  //終點判斷
    public static bool RE = false;   //重跑判斷
    private float speedChange = 0.0f;

    public GameObject pause;
    public GameObject img;
    public GameObject panel;
    public AudioSource bubu;
    public AudioSource dandan;

    [SerializeField]
    private GameObject blackBox;


    private int currentSeg = 0;
    private float transition;
    private bool isCompleted = false;

    IEnumerator delay()
    {
        yield return new WaitForSeconds(5.0f);
    }
    private void Awake()
    {
        blackBox.SetActive(false);
    }

    private void Update()
    {
        //停車
        if (Input.GetKeyDown(KeyCode.Z) && time.time_int == -1)
        {
            dandan.Pause();
            if (isStop)
            {
                speed = speedChange;
                dandan.Play();
            }
            isStop = !isStop;
            bubu.volume = 0.0f;

        }

        //if(time_UI.text(OnEnable) )
        if (isStop == false)
        {
            bubu.volume = 0.3f;
            pause.SetActive(false);
            panel.SetActive(false);

            //加速
            if (Input.GetKeyDown(KeyCode.W))
                speed += 20;
            if (Input.GetKeyDown(KeyCode.S))
                speed -= 20;
        }

        else if (isStop == true & time.time_int == -1)
        {
            if (speed != 0)
                speedChange = speed;
            pause.SetActive(true);
            panel.SetActive(true);
            speed = 0.0f;
        }
        if (!rail)
            return;

        if (speed < 50 && !isStop)
            speed = 50f;

        if (speed > 175)
            speed = 175f;

        if (!isCompleted && !isStop)
            Play();

        if (currentSeg == 202)
            blackBox.SetActive(true);
        else if (currentSeg == 211)
        {
            speed = 85;
            blackBox.SetActive(false);
        }
        if (currentSeg == 359)
        {
            img.SetActive(true);
            panel.SetActive(true);
            bubu.volume = 0.0f;

            end = true;
            time.time_int = 6;
            isStop = true;
            speed = 0.0f;
            speedChange = 0;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("END");
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RE = true;
                end = false;
                currentSeg = 0;
                dandan.Stop();

            }
        }
    }

    private void Play(bool foward = true)
    {
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float Slope;
        if (currentSeg == 310)
        {
            Slope = 1.0f;
        }
        else
        {
            Slope = rail.nodes[currentSeg + 1].position.y - rail.nodes[currentSeg].position.y;
        }
        if (Slope * 10 <= 1.5f && Slope * 10 >= -1.5f)
        {
            Addspeed = 0;
        }
        else
        {
            if (Slope > 0)
            {
                Addspeed = Slope * 0.5f;
            }
            else
            {
                Addspeed = Slope * 1.7f;
            }
        }

        speed -= Addspeed / 100;

        float s = (Time.deltaTime * 1 / m) * speed;

        transition += (foward) ? s : -s;
        if (transition > 1)
        {
            transition = 0;
            currentSeg++;
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSeg--;
        }

        if (currentSeg >= rail.nodes.Length - 1)
            currentSeg = 0;

        transform.position = rail.PositionOnRail(currentSeg, transition, Playmode);
        transform.rotation = rail.Orientation(currentSeg, transition);

    }
}
