using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTextBackAndForth : MonoBehaviour
{
    public float startPositionZ = 10f;
    public float endPositionZ = 20f;
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingToEnd = true;
    private void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y, startPositionZ);
        endPos = new Vector3(transform.position.x, transform.position.y, endPositionZ);
    }
    private void Update()
    {
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);

            if (transform.position.z >= endPos.z)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);

            if (transform.position.z <= startPos.z)
            {
                movingToEnd = true;
            }
        }
    }
}
