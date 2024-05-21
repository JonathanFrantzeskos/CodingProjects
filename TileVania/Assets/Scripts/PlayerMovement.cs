using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    void Start()
    {
        Application.targetFrameRate = 60 ;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value){
        if (value.isPressed){
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run(){
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y); //y value says to just stay the same, thus obey the gravity we put (no changing y)
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; //bool to know if player is moving
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed); //update isRunning to that same bool which tells if either running or idling
    }

    void FlipSprite(){
        //Mathf.Epsilon is a better version of 0 since 0 is techincally 0.000001 so epsilon avoids that issue
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed){
            //Mathf.Sign: returns sign of what the x velocity is (left = negative, right positive)
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x) , 1f);
        }
    }
}
