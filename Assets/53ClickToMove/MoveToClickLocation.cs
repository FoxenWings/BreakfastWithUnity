﻿using UnityEngine;
using System.Collections;

public class MoveToClickLocation : MonoBehaviour
{
	public float movementSpeed = 1.0f;
	public float rotationSpeed = 30.0f;
	public LayerMask movementLayer = -1;

	private Vector3 targetPosition;
	private Quaternion targetRotation;
	private bool atTarget = true;
	private bool facingTarget = false;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, movementLayer))
			{
				Debug.DrawLine(transform.position, targetPosition,Color.white,1.0f);
				targetPosition = hit.point;
				targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
				StopAllCoroutines();
				atTarget = false;
			}
		}

		Vector3 newPosition = transform.position;

		if(!atTarget)
		{
			Vector3 moveDirection = transform.forward*movementSpeed*Time.deltaTime;

			newPosition += moveDirection;

			if(Vector3.Dot(moveDirection, targetPosition - newPosition) <= 0.0f) //We aren't facing target
			{
				if(facingTarget)
				{
					transform.position = targetPosition;
					atTarget = true;
					facingTarget = false;
				}
			}
			else //We are facing target
			{
				transform.position = newPosition;
				facingTarget = true;
			}
		}

		transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(targetPosition - newPosition),rotationSpeed*Time.deltaTime);
	}

}
