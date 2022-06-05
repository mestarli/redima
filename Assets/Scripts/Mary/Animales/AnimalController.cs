using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    [SerializeField] private GameObject animal_trigger_01;
    [SerializeField] private GameObject animal_trigger_02;
    [SerializeField] private GameObject animal_trigger_03;
    private float positionZ;
    
    // Start is called before the first frame update
    void Update()
    {
        MoveAnimal();
    }

    private void MoveAnimal()
    {
        if(animal_trigger_01.activeSelf)
        {
            positionZ = animal_trigger_01.transform.position.z;
            Vector3 newPos = new Vector3(transform.position.x,transform.position.y, positionZ );
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 0.2f);
        }
        if(animal_trigger_02.activeSelf)
        {
            positionZ = animal_trigger_02.transform.position.z;
            Vector3 newPos = new Vector3(transform.position.x,transform.position.y, positionZ );
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 0.2f);
        }
    }
}
