using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Animator headAnimator;
    [SerializeField] Animator bodyAnimator;
    [SerializeField] GameObject deathScreen;

    GetScaredRange getScaredRange;

    Rigidbody2D rb;
    private Interactable interactable;
    Vector2 moveDirection;

    public bool canMove = true;

    void Start()
    {
        canMove = true;
        getScaredRange = GetComponent<GetScaredRange>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!canMove ) { return; }
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactable>())
        {
            interactable = collision.GetComponent<Interactable>();
        }
    }


    public void GetScared()
    {
        headAnimator.SetTrigger("scared");
    }
    public void GetHappy()
    {
        headAnimator.SetTrigger("happy");
    }

    #region Input Methods

    public void OnMove(InputValue input)
    {
        if (!canMove) { return; }

        moveDirection = input.Get<Vector2>();

        Debug.Log(moveDirection);

        headAnimator.SetFloat("Blend", moveDirection.magnitude);
        bodyAnimator.SetFloat("Blend", moveDirection.magnitude);

        rb.velocity = moveDirection * moveSpeed;
    }


    public void OnWhistle()
    {
        if (!canMove) { return; }

        Debug.Log("WHISTLE!");
        getScaredRange.ScareHyenas();
    }

    public void OnInteract()
    {
        if (!canMove) { return; }

        Debug.Log("Interact Triggered");

        if(interactable != null )
        {
            interactable.Interact();
        }
    }

    internal void Die()
    {
        if(canMove == true)
        {
            deathScreen.SetActive(true);
            canMove = false;
        }
        
    }

    #endregion
}
