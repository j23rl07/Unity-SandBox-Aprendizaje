using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpHeight;
    private float velX, velY;
    Rigidbody2D rb;
    public Transform groundcheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    Animator anim;

    
    // Start is called before the first frame update
    void Start(){

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        //comprobar si esta tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);

        CharacterOrientation();

        if(isGrounded){
            anim.SetBool("Jump", false);
        } else {
            anim.SetBool("Jump", true);
        }

    }

    private void FixedUpdate(){

        Movement();
        Jump();

    }

    public void Movement(){
        //Caminar
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;
        rb.velocity = new Vector2(velX * speed, velY);

        if(rb.velocity.x != 0){
            anim.SetBool("Walk", true);
        } else {
            anim.SetBool("Walk", false);
        }

    }

    public void Jump(){
        //Saltar
        if(Input.GetButton("Jump") && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        
    }

    public void CharacterOrientation(){

        //Que mire a la derecha o izquierda dependiendo de acia donde camine
        if(rb.velocity.x>0){
            transform.localScale = new Vector3(1,1,1);
        }else if(rb.velocity.x<0){
            transform.localScale = new Vector3(-1,1,1);
        }

    }
}
