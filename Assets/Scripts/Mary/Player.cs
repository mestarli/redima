using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float hambre;
    private float maxHambre = 100;
    public bool puedeAlimentarse;
    public bool cursorEnabled;

    [SerializeField] private bool isInBoat;
    [SerializeField] private bool SetActiveBoat;
    [SerializeField] private GameObject Boat;
    [SerializeField] private GameObject BoatColliders;

    private float diferencia;

    private float x;
    private float y;
    private float z;
    
    [SerializeField] private Vector3 posicionPlayer;
    [SerializeField] private Quaternion rotationPlayer;
    [SerializeField] private Vector3 posicionBarco;
    [SerializeField] private Quaternion rotationBarco;
    [SerializeField] private bool isInBag;
    public bool isInBagPaloma;
    [SerializeField] private GameObject PalomaHead;
    [SerializeField] private GameObject Bag;
    [SerializeField] private GameObject AnimalesGranja;
    [SerializeField] private GameObject AnimalesAviario;
    
    
    [SerializeField] private Image countHunger;
    private void Awake()
    {
        Instance = this;
        hambre = maxHambre;
        posicionPlayer = gameObject.transform.position;
        posicionBarco = Boat.transform.position;
        rotationPlayer = gameObject.transform.rotation;
        rotationBarco = Boat.transform.rotation;
        //to hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        InvokeRepeating("decreaseHunger", 1.0f, 1.0f);
    }
    private void Update()
    {
        PuedeAlimentarse();
        if (isInBoat && Input.GetKeyDown(KeyCode.G) && SetActiveBoat)
        {

            Boat.GetComponent<ShipMovement>().enabled = false;
            Boat.gameObject.GetComponent<ShipFloatMovement>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            gameObject.transform.parent = null;
            PlayerMovement.Instance._animator.SetLayerWeight(1, 0);
            BoatColliders.SetActive(false);
            StartCoroutine(ResetExitBoat(false));
        }

        if (isInBoat && Input.GetKeyDown(KeyCode.G) && !SetActiveBoat)
        {
            Boat.GetComponent<ShipMovement>().enabled = true;
            Boat.gameObject.GetComponent<ShipFloatMovement>().enabled = true;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.transform.parent = Boat.transform;
            gameObject.transform.position = Boat.transform.GetChild(1).transform.position;
            gameObject.transform.rotation = Boat.transform.GetChild(1).transform.rotation;
            BoatColliders.SetActive(true);
            PlayerMovement.Instance._animator.SetLayerWeight(1, 1);
            StartCoroutine(ResetExitBoat(true));
        }

        // Habilitaremos el cursor con la tecla alt izquierda
        if (!cursorEnabled && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameObject.GetComponent<CameraController>().enabled = false;
            StartCoroutine(ResetCursor(true));
        }else if (cursorEnabled && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.GetComponent<CameraController>().enabled = true;
            StartCoroutine(ResetCursor(false));
            cursorEnabled = false;
        }
        
    }

    private void PuedeAlimentarse()
    {
        if (hambre < maxHambre)
        {
            puedeAlimentarse = true;
        }
        
        else
        {
            puedeAlimentarse = false;
        }
    }

    /// <summary>
    /// Metodo para controlar el tamaño del player al coger las 2 setas diferentes que hay en el mapa
    /// </summary>
    /// <param name="tamaño">Variable modificada por los scriptable objects de las setas</param>
    public void Tamaño(float tamaño)
    {
        x = tamaño;
        y = tamaño;
        z = tamaño;
        transform.localScale = new Vector3(x, y, z);

        AudioManager.instance.PlaySong("Zarzo_Medida");
        
        if (tamaño == 0.1f)
        {
            PlayerMovement.Instance._characterController.stepOffset = 0;
            PlayerMovement.Instance.speed = 3f;
            PlayerMovement.Instance.speedRun = 5f;
        }
        else
        {
            PlayerMovement.Instance._characterController.stepOffset = 1;
            PlayerMovement.Instance.speed = 8f;
            PlayerMovement.Instance.speedRun = 12f;
        }
    }
    
    public void alimentarse(float cantidad)
    {
        diferencia = maxHambre - hambre;
        if (hambre < maxHambre)
        {
            if (cantidad < diferencia)
            {
                hambre = hambre + cantidad;
            }
        
            else
            {
                hambre = hambre + diferencia; 
            }
        }
        
        else
        {
            return;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        isInBoat = false;
        if (hit.gameObject.tag ==  "Water")
        {
            PlayerMovement.Instance._animator.SetTrigger("IsDrawned");
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(ResetPlayerToHomeFromWater());
        }
        if (hit.gameObject.tag ==  "Ship" && !isInBoat)
        {
            isInBoat = true;
        }
        if (hit.gameObject.tag ==  "Paloma" && Input.GetKeyDown(KeyCode.R))
        {
            isInBagPaloma = true;
            //Para la pokedex
            if ( hit.transform.parent.transform.GetChild(0).GetComponent<AnimalController>().enablePokedex.transform.GetChild(1).gameObject.tag != "Active")
            {
                hit.transform.parent.transform.GetChild(0).GetComponent<AnimalController>().enablePokedex.transform.GetChild(1).gameObject.transform.GetComponent<Image>().color = new Color32(255,255,225,100);
                hit.transform.parent.transform.GetChild(0).GetComponent<AnimalController>().enablePokedex.transform.GetChild(1).gameObject.tag = "Active"; 
            }
            Destroy(hit.gameObject.transform.parent.gameObject);
            PalomaHead.SetActive(true);
        }
        if (hit.gameObject.tag ==  "AddToBag" && !isInBag && Input.GetKeyDown(KeyCode.R))
        {
            String tagAnimal = hit.gameObject.transform.parent.gameObject.tag;
            //Para la pokedex

            if ( hit.transform.parent.transform.GetChild(0).GetComponent<AnimalController>().enablePokedex.transform.GetChild(1).gameObject.tag != "Active")
            {
                hit.transform.parent.transform.GetChild(0).GetComponent<AnimalController>().enablePokedex.transform.GetChild(1).gameObject.transform.GetComponent<Image>().color = new Color32(255,255,225,100);
                hit.transform.parent.transform.GetChild(0).GetComponent<AnimalController>().enablePokedex.transform.GetChild(1).gameObject.tag = "Active"; 
            }
            Destroy(hit.gameObject.transform.parent.gameObject);
            foreach (Transform animal in Bag.transform)
            {
                if (animal.tag == tagAnimal)
                {
                    animal.gameObject.SetActive(true);
                    
                    
                }
            }
            isInBag = true;
        }
        if (hit.gameObject.tag ==  "SendToGranja" && isInBag && Input.GetKeyDown(KeyCode.R))
        {
            String tagAnimal = "";
            foreach (Transform animal in Bag.transform)
            {
                if (animal.gameObject.active)
                {
                    tagAnimal = animal.gameObject.tag; ;
                }
            }
            foreach (Transform animal in AnimalesGranja.transform)
            {
                if (animal.tag == tagAnimal)
                {
                    animal.gameObject.SetActive(true);
                    foreach (Transform animal2 in Bag.transform)
                    {
                        if (animal2.gameObject.active)
                        {
                            isInBag = false;
                            animal2.gameObject.SetActive(false);
                        }
                    }
                }
            }
            
        }
        if (hit.gameObject.tag ==  "SendToAviario" && isInBag && Input.GetKeyDown(KeyCode.R))
        {
            String tagAnimal = "";
            foreach (Transform animal in Bag.transform)
            {
                if (animal.gameObject.active)
                {
                    tagAnimal = animal.gameObject.tag; ;
                }
            }
            foreach (Transform animal in AnimalesAviario.transform)
            {
                if (animal.tag == tagAnimal)
                {
                    animal.gameObject.SetActive(true);
                    foreach (Transform animalr in Bag.transform)
                    {
                        if (animalr.gameObject.active)
                        {
                            isInBag = false;
                            animalr.gameObject.SetActive(false);
                        }
                    }
                }
            }
            
        }
    }
    
    IEnumerator ResetPlayerToHomeFromWater()
    {
      
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = posicionPlayer;
        Boat.transform.position = posicionBarco;
        gameObject.transform.rotation = rotationPlayer;
        Boat.transform.rotation = rotationBarco;
        PlayerMovement.Instance._animator.ResetTrigger("IsDrawned");
        PlayerMovement.Instance._animator.SetTrigger("Default");
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        PlayerMovement.Instance._animator.ResetTrigger("Default");
    }
    
    IEnumerator ResetExitBoat(bool estaBarco)
    {
      
        yield return new WaitForSeconds(2f);
        SetActiveBoat = estaBarco;
    }
    
    IEnumerator ResetCursor(bool enable)
    {
      
        yield return new WaitForSeconds(0.2f);
        cursorEnabled  = enable;
    }
    
    void decreaseHunger(){
        if(hambre > 0) {
            hambre -= 1;
            countHunger.fillAmount = hambre / maxHambre;
            PlayerMovement.Instance.speed = 8f;
            PlayerMovement.Instance.speedRun = 12f;
        }

        if (hambre <= 0)
        {
            PlayerMovement.Instance.speed = 3f;
            PlayerMovement.Instance.speedRun = 3f;
        }
    }
}
