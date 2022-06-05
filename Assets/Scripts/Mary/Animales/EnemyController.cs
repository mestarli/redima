using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject animal_trigger_01;
    [SerializeField] private GameObject animal_trigger_02;
    [SerializeField] private GameObject animal_trigger_03;
    private Vector3 position;

    [SerializeField] private Animator _animator;
    [SerializeField] private bool isWaiting;

    
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Update()
    {
        MoveAnimal();
    }

    private void MoveAnimal()
    {
        if(animal_trigger_01.activeSelf && !isWaiting)
        {
            position = animal_trigger_01.transform.position;
            _animator.SetBool("IsWalking",true);
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 0.15f);
        }
        if(animal_trigger_02.activeSelf  && !isWaiting)
        {
            position = animal_trigger_02.transform.position;
            _animator.SetBool("IsWalking",true);
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 0.15f);
        }
        if(animal_trigger_03.activeSelf && !isWaiting)
        {
            position = animal_trigger_03.transform.position;
            _animator.SetBool("IsWalking",true);
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 0.15f);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("Trigger_move_01"))
            {
                animal_trigger_01.SetActive(false);
                animal_trigger_02.SetActive(true);
                animal_trigger_03.SetActive(false);
                transform.LookAt(animal_trigger_02.transform);
                StartCoroutine(Waiting());
                
            }
            if (other.gameObject.CompareTag("Trigger_move_02"))
            {
                animal_trigger_01.SetActive(false);
                animal_trigger_02.SetActive(false);
                animal_trigger_03.SetActive(true);
                transform.LookAt(animal_trigger_03.transform);
                StartCoroutine(Waiting());
              
            }
            if (other.gameObject.CompareTag("Trigger_move_03"))
            {
                animal_trigger_01.SetActive(true);
                animal_trigger_02.SetActive(false);
                animal_trigger_03.SetActive(false);
                transform.LookAt(animal_trigger_01.transform);
                StartCoroutine(Waiting());
            }
     }
    IEnumerator Waiting()
    {
        isWaiting = true;
        _animator.SetBool("IsWalking",false); 
        yield return new WaitForSeconds(4f);
        isWaiting = false;

    }
}
