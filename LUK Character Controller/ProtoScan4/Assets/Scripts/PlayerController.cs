using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //SerializedField
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float moveSpeed;

    //Private
    Rigidbody2D myRb;
    Animator myAnim;
    Vector2 moveInput;
    Vector2 lastMove;
    bool playerMoving = false;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        //rotates the player following the cursor's position at 'rotationSpeed'
        /*
        Vector2 playerDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        Quaternion playerRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
        */

        playerMoving = false;

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (moveInput != Vector2.zero)
        {
            myRb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
            lastMove = moveInput;
            playerMoving = true;
        }
        else
        {
            myRb.velocity = Vector2.zero;
        }

        myAnim.SetBool("PlayerMoving", playerMoving);
        myAnim.SetFloat("MoveX", moveInput.x);
        myAnim.SetFloat("MoveY", moveInput.y);
        myAnim.SetFloat("LastMoveX", lastMove.x);
        myAnim.SetFloat("LastMoveY", lastMove.y);
    }
}