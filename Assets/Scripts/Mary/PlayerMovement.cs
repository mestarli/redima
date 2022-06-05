using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedRun = 25f;
    private float initialSpeed;
    public CharacterController _characterController;
    
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumHeight = 3.5f;
    private Vector3 playerVelocity;

    public Animator _animator;
    private Transform _modelTransform;
    [SerializeField] private Transform cam;
    
    //For check if its toching ground
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 inputPlayerMovement;
    
    
    // For gliding palomo
    [SerializeField] private float isGliding;
    
    
    // Componentes para interactuar
    private PlayerInteraction _playerInteraction;
    
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float groundDistance = 0.4f;
    
    void Awake()
    {
        Instance = this;
        _animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        initialSpeed = speed;
        _characterController = GetComponent<CharacterController>();
        _modelTransform = GetComponent<Transform>();
        _playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // comprobamos que esta tocando suelo, si no es así, es que está saltando
        isGrounded  = Physics.CheckSphere(groundCheck.position,0.01f,groundLayer);
        Movement();
        
        // Llamamos al metodo Interact para interactuar con los cultivos
        Interact();
    }
   
    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        //_animator.SetBool("IsWalking",false);
        _animator.SetBool("IsRunning",false);
        _animator.SetBool("IsWalking",false);
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _characterController.Move(moveDir.normalized * speed * Time.deltaTime);
            _animator.SetBool("IsWalking",true);
        }

        //Detección de suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        //Gravedad
        playerVelocity.y += gravity  * Time.deltaTime;
        
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -1.86f;
        }
        _characterController.Move(playerVelocity * Time.deltaTime);
        
        //Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumHeight * -2 * gravity);
        }
        // Animacion de saltar
        if (!isGrounded)
        {
            _animator.SetBool("IsJumping",true);
        }
        else
        {
            _animator.SetBool("IsJumping",false);
        }
        Run();
    }

    /// <summary>
    /// Método para correr
    /// </summary>
    private void Run()
    {
        bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if(isShiftKeyDown && isGrounded)
        {
            speed = speedRun;
            //_animator.SetBool("IsRunning", isShiftKeyDown);
            _animator.SetBool("IsRunning",true);
           
        }
        else
        {
            speed = initialSpeed;
        }
    }

    ///<summary>
    /// Método para interactuar con los cultivos mediante una tecla
    /// </summary>
    public void Interact()
    {
        if (Input.GetMouseButton(0))
        {
            // Interactua
            _playerInteraction.Interact();
        }
    }
}
