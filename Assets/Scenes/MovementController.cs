using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Transform target;
    public float moveForce = 10f;
    public float stoppingDistance = 1f;
    private float slowdownDistance;
    private int k = 1;

    private void Start()
    {
        float distance = Vector2.Distance(transform.position, target.position);
         slowdownDistance = (distance / 5)*4;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveForce * k * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < slowdownDistance)
        {
            k = 3;
        }

        if (Vector3.Distance(transform.position, target.position) < slowdownDistance / 4)
            k = 2;
    }
}