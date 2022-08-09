using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // Reference declaration
	public Transform cameraTransform;
	public float cameraSensitivity;
	
	// Variable declaration
	int i;
	int leftFingerId;
	int rightFingerId;
	float cameraPitch;
	float halfScreenWidth;
	Vector2 lookInput;
	
	// Start is called before the first frame update
	void Start()
	{
		// initialize to -1, which means the finger is not being tracked
		leftFingerId = -1;
		rightFingerId = -1;
		
		halfScreenWidth = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update()
	{
		GetTouchInput();
		
		if (leftFingerId != -1)
		{
			LookAround();
		}
	}
	
	void GetTouchInput()
	{
		for (i = 0; i < Input.touchCount; i++)
		{
			Touch t = Input.GetTouch(i);
			
			switch (t.phase)
			{
				// Determines when a finger input is detected
				case TouchPhase.Began:
				
					if (t.position.x < halfScreenWidth && leftFingerId == -1)
					{
						// Debug.Log("Tracking left finger input.");
						leftFingerId = t.fingerId;
					}
					
					else if (t.position.x > halfScreenWidth && rightFingerId == -1)
					{
						// Debug.Log("Tracking right finger input.");
						rightFingerId = t.fingerId;
					}
					
				break;
				
				case TouchPhase.Ended:
				
				// Determines when a finger input is no longer detected
				case TouchPhase.Canceled:
					
					if (t.fingerId == leftFingerId)
					{
						// Debug.Log("Stopped tracking left finger input.");
						leftFingerId = -1;
					}
					
					else if (t.fingerId == rightFingerId)
					{
						// Debug.Log("Stopped tracking right finger input.");
						rightFingerId = -1;
					}
					
				break;
				
				// Determines when the finger moves
				case TouchPhase.Moved:
					
					if (t.fingerId == leftFingerId)
					{
						lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
					}
					
				break;
				
				// Determines when the finger is no longer moving
				case TouchPhase.Stationary:
					
					if (t.fingerId == leftFingerId)
					{
						lookInput = Vector2.zero;
					}
					
				break;
			}
		}
	}
	
	void LookAround()
	{
		// Vertical camera rotation
		cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
		cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
		
		// Horizontal camera rotation
		transform.Rotate(transform.up, lookInput.x);
	}
}
