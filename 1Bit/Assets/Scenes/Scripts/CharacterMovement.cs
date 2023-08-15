using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
	public float walkSpeed;
	public float gravity;
	public float velocityY;

	CharacterController controller;

	public Transform playerCamera = null;
	public float mouseSensitivity = 2f;

	private bool lockCursor = true;

	public bool lockCamera = false;

	float cameraPitch = 0.0f;
	public Vector2 mouseDelta = new Vector2(0,0);

	public string inp_moX = "Mouse X";
	public string inp_moY = "Mouse Y";
	public string inp_mvX = "Horizontal";
	public string inp_mvY = "Vertical";

	public SoundSorce soundSource;


	private void Start()
	{
		if (lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		controller = GetComponent<CharacterController>();
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
		}
		print("Press 'Space' for a white cylinder.\nPress 'L' to un-/lock mouse movement.\n" +
			"Press 'esc' to make mouse visible.");
	}

	void Update()
	{
		if (!lockCamera)
			UpdateMouseLook();
		UpdateMovement();

	}

	void UpdateMouseLook()
	{
		mouseDelta = new Vector2(Input.GetAxis(inp_moX), Input.GetAxis(inp_moY));
		cameraPitch = Mathf.Clamp(cameraPitch - ( mouseDelta.y * mouseSensitivity ), -90.0f, 90.0f);

		playerCamera.localEulerAngles = Vector3.right * cameraPitch;
		transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
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

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//Do something, as soon Collider hits something
		//(It will also hit the floor while normally walking)
	}

	public void OnGUI()
	{
		Event e = Event.current;
		if (e.isKey && e.type == EventType.KeyDown)
		{
			if (e.keyCode == KeyCode.L)
				lockCamera = !( lockCamera );
			else if (e.keyCode == KeyCode.Space && soundSource != null)
				soundSource.SpawnButtonClicked();
		}
	}
}
