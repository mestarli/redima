using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Variables
    public static TimeManager instance;

    [SerializeField] private GameTimestamp timestamp;

    public float timeScale = 1.0f;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        // Inicializamos el time stamp
        timestamp = new GameTimestamp(0, GameTimestamp.Season.Spring, 1, 10, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1/timeScale);
            Tick();
        }
    }
    
    public void Tick()
    {
        timestamp.UpdateClock();
    }
}
