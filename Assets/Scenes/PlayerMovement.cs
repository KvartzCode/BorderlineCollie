using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float moveSpeed = 5f;

	[SerializeField] Animator animator;

	Vector2 moveDirection;


    void Start()
    {
		
    }

	void Update()
	{
		transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
	}

	#region Input Methods

	public void OnMove(InputValue input)
	{
		moveDirection = input.Get<Vector2>();
		animator.SetFloat("Blend", moveDirection.magnitude);
	}

	public void OnWhistle()
	{
		Debug.Log("WHISTLE!");
	}

	public void OnInteract()
	{
		Debug.Log("Interact Triggered");
	}

	#endregion
}
