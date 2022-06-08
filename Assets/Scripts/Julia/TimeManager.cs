using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Variables
    public static TimeManager instance;

    [Header("Internal Clock")]
    [SerializeField] private GameTimestamp timestamp;
    public float timeScale = 1.0f;

    [Header("Day & Night Cycle")]
    public Transform sunTransform;
    public GameObject faroTransform;
    public Material sunNightMat; 
    
    //Variable para saber si es de día o de noche
    public bool isNight;

    private List<ITimeTracker> listeners = new List<ITimeTracker>();

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
        
        faroTransform.SetActive(false);
    }

    private void Start()
    {
        // Inicializamos el time stamp
        timestamp = new GameTimestamp(0, GameTimestamp.Season.Spring, 1, 6, 0);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while(true)
        {
            Tick();
            yield return new WaitForSeconds(1/timeScale);
        }
    }
    
    public void Tick()
    {
        timestamp.UpdateClock();

        // Informar a los listeners del nuevo estado del tiempo
        foreach (ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timestamp);
        }
        
        UpdateSunMovement();
    }
    
    // Método para que el sol se mueva en funcion del tiempo actual
    // Ciclo de dia y noche
    public void UpdateSunMovement()
    {
        // Conversion del tiempo actual a minutos
        int timeInMinutes = GameTimestamp.HoursToMinutes(timestamp.hour) + timestamp.minute;
        int timeInHours = GameTimestamp.DaysToHours(timestamp.day) - timestamp.hour;

        // El sol se mueve 15 grados en 1 hora
        // .25 grados en 1 minuto
        // A las 00:00, el angulo de sol debera ser -90
        float sunAngle = .25f * timeInMinutes - 90;
        
        // Aplicamos el angulo a la luz direccional
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);

       
        if (GetGameTimestamp().hour >= 19 || GetGameTimestamp().hour <= 5)
        {
            faroTransform.SetActive(true);
            faroTransform.transform.Rotate(0, 1.5f, 0);
            sunNightMat.mainTextureOffset = new Vector2(0.5f, 0);
            
            isNight = true;
        }

        else
        {
            faroTransform.SetActive(false);
            sunNightMat.mainTextureOffset = new Vector2(0.1f, 0);
            isNight = false;
        }
    }

    // Para obtener el GameTimestamp en otro script
    public GameTimestamp GetGameTimestamp()
    {
        // Devuelve una instancia nueva
        return new GameTimestamp(timestamp);
    }
    
    // Añadir un objeto a la lista de listeners
    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }

    // Eliminar un objeto de la lista de listeners
    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }
}
