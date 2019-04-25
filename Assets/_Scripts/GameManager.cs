using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private RollerCoaster _rollerCoaster;
    private Rail _rail;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //Load();
    }

    private void Load()
    {
        _rollerCoaster.GetComponentInChildren<RollerCoaster>();
        _rail.GetComponentInChildren<Rail>();

        if (_rollerCoaster == null)
            Debug.Log("RollerCoaster did not found");

        if (_rail == null)
            Debug.Log("Rail did not found");
    }
}
