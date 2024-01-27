using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float moveSpeed = 5f;

	Vector2 moveDirection;


    // Start is called before the first frame update
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
