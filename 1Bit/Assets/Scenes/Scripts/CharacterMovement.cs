using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
	public float	walkSpeed;
	public float	gravity;
	public float	velocityY;
	public float	mouseSensitivity = 2f;
	public float	InteractionRange;

	public bool	lockCamera = false;

	public Vector2	mouseDelta = new Vector2(0,0);

	public string	inp_moX = "Mouse X";
	public string	inp_moY = "Mouse Y";
	public string	inp_mvX = "Horizontal";
	public string	inp_mvY = "Vertical";

	public Transform	playerCamera;
	public SoundSorce	soundSource;

	CharacterController	controller;
	float	cameraPitch = 0.0f;
	private bool	lockCursor = true;
	private GameObject	lookingAt;


	private void Start()
	{
		try
		{
			Input.GetAxis(inp_moX);
			Input.GetAxis(inp_moY);
			Input.GetAxisRaw(inp_mvX);
			Input.GetAxisRaw(inp_mvY);
		}
		catch
		{
			print("Something in the InputManager is not working!!! Character Movement disabled.");
			enabled = false;
			return ;
		}
		if (lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		if (!playerCamera)
		{
			print("No camura.\nbaka");
			enabled = false;
			return ;
		}
		controller = GetComponent<CharacterController>();
		print("Press 'Space' for a white cylinder.\nPress 'L' to un-/lock mouse movement.\n" +
			"Press 'esc' to make mouse visible.");
	}

	void Update()
	{
		if (!lockCamera)
			UpdateMouseLook();
		UpdateMovement();
		CheckInteractables();
		CheckClick();
	}

	void	CheckClick()
	{
		if (Input.GetAxis("Click Button") > 0)
			print("CLICKED");
	}

	void UpdateMouseLook()
	{
		mouseDelta = new Vector2(Input.GetAxis(inp_moX), Input.GetAxis(inp_moY));
		cameraPitch = Mathf.Clamp(cameraPitch - ( mouseDelta.y * mouseSensitivity ), -90.0f, 90.0f);

		playerCamera.localEulerAngles = Vector3.right * cameraPitch;
		transform.Rotate(mouseDelta.x * mouseSensitivity * Vector3.up);
	}

	void UpdateMovement()
	{
		Vector2 inputDir = new Vector2(Input.GetAxisRaw(inp_mvX), Input.GetAxisRaw(inp_mvY));

		velocityY += gravity * Time.deltaTime;
		if (controller.isGrounded && velocityY < 0)
			velocityY = 0.0f;

		Vector3 velocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * walkSpeed + transform.up * velocityY;
		controller.Move(velocity * Time.deltaTime);
	}

	void CheckInteractables()
	{
		RaycastHit[] hit = Physics.RaycastAll(playerCamera.transform.position, playerCamera.transform.forward, InteractionRange);
		int important = -1;
		for (int i = 0; i < hit.Length; i++)
		{
			if (!hit[i].transform.gameObject.CompareTag("Player"))
			{
				if (important == -1 || hit[important].distance > hit[i].distance)
					important = i;
			}
		}

		GameObject gogo = (important >= 0) ? hit[important].transform.gameObject : null;
		if (lookingAt && lookingAt != gogo)
		{
			print($"Player STOPPED looking at collider named <{lookingAt.name}>");
			lookingAt = null;
		}
		if (gogo)
		{
			if (!lookingAt)
			{
				lookingAt = gogo;
				print($"Player STARTED now looking at collider named <{lookingAt.name}>");
			}
			print($"Player is CURRENTLY looking at collider named <{lookingAt.name}>");
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * InteractionRange);
	}

	public void OnGUI()
	{
		Event e = Event.current;
		if (e.isKey && e.type == EventType.KeyDown)
		{
			if (e.keyCode == KeyCode.L)
				lockCamera = !(lockCamera);
			else if (e.keyCode == KeyCode.Space && soundSource != null)
				soundSource.SpawnButtonClicked();
		}
	}
}
