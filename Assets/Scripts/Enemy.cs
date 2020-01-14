using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    private Vector3 targetPosition;

    [SerializeField]
    //[Range(0,1f)]
    private float moveSpeed;

    private int waypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, .5f * moveSpeed);

        if (Vector3.Distance(transform.position, targetPosition) < .25f)
        {
            if (waypointIndex >= waypoints.Length - 1)
            {
                waypointIndex = 0;
            }
            else
            {
                waypointIndex++;
            }
            targetPosition = waypoints[waypointIndex].position;
        }
    }
}
