using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float life = 100f;
    [SerializeField] private float maxLife = 100f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float speed = 8.5f;
    [SerializeField] private float speedRun = 15.5f;
    private float initialSpeed;
    private Rigidbody _rigidbody;
    
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private float jumHeight = 3.5f;

    private Animator _animator;
    
    //For check if its toching ground
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 inputRot;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        initialSpeed = speed;
        _rigidbody = GetComponent<Rigidbody>();
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
        Jump();
    }
    
    void FixedUpdate()
    {
        // Llamamos funcionalidades para moverse, correr, saltar...
        
        //_animator.SetBool("IsRunning", false);
       
        Movement();


    }
    
    private void Movement()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); 
        float zMove = Input.GetAxisRaw("Vertical");
       // _animator.SetFloat("Walking", Mathf.Abs(xMove));
       // _animator.SetFloat("Walking", Mathf.Abs(zMove));
        
        //Movement of player
        _rigidbody.velocity = transform.forward * speed *  zMove // Forward, Backward movement of player
                              + transform.right * speed * xMove   // Left, Right Movement of player;
        Run();
    }
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            
            _rigidbody.AddForce(jumHeight * Vector3.up, ForceMode.VelocityChange);
            //_animator.SetTrigger("IsJumping");
        }

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
