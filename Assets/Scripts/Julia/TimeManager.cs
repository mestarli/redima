using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //https://www.youtube.com/watch?v=jNbOXAGJbZ4
    //19:09
    
    // Variables
    public static TimeManager instance;

    [SerializeField] private GameTimestamp timestamp;

    public float timeScale = 75f;

    public Transform sunTransform;
    
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

        // Conversion del tiempo actual a minutos
        int timeInMinutes = GameTimestamp.HoursToMinutes(timestamp.hour) + timestamp.minute;

        // El sol se mueve 15 grados en 1 hora
        // .25 grados en 1 minuto
        // A las 00:00, el angulo de sol debera ser -90
        float sunAngle = .25f * timeInMinutes - 90;
        
        // Aplicamos el angulo a la luz direccional
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }
}
