using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float life = 100f;
    [SerializeField] private float maxLife = 100f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float speedRun = 2.5f;
    private float initialSpeed;
    private CharacterController _characterController;
    
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private float jumHeight = 3.5f;
    private Vector3 playerVelocity;

    private Animator _animator;
    private Transform _modelTransform;
    
    //For check if its toching ground
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 inputPlayerMovement;
    
    
    // For gliding palomo
    [SerializeField] private float isGliding;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        initialSpeed = speed;
        _characterController = GetComponent<CharacterController>();
        _modelTransform = GetComponent<Transform>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // comprobamos que esta tocando suelo, si no es así, es que está saltando
        isGrounded  = Physics.CheckSphere(groundCheck.position,0.15f,groundLayer);
        Movement();
    }
   
    private void Movement()
    {

      
        
        
        inputPlayerMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.Move(  inputPlayerMovement * Time.deltaTime * speed);

        if (inputPlayerMovement.x !=0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(0,0,inputPlayerMovement.x));
            _modelTransform.rotation = newRotation;
        }
        
        if (inputPlayerMovement != Vector3.zero)
        {
            gameObject.transform.forward = inputPlayerMovement;
        }
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            inputPlayerMovement.y = 0f;
        }
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumHeight * -3.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);
        Run();
    }

    /// <summary>
    /// Método para correr
    /// </summary>
    private void Run()
    {
        bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if(isShiftKeyDown)
        {
            speed = speedRun;
            //_animator.SetBool("IsRunning", isShiftKeyDown);
           
        }
        else
        {
            speed = initialSpeed;
        }
    }
}
