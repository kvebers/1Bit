using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + transform.TransformDirection(movement);
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotationX -= mouseY * rotationSpeed * Time.deltaTime;
        rotationY += mouseX * rotationSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}