using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float speed = 1.0f; // prêdkoœæ ruchu platformy
    [SerializeField] public Transform[] waypoints; // punkty docelowe
    private int currentWaypoint = 0; // indeks bie¿¹cego punktu docelowego
    private float reachThreshold = 0.1f; // próg dla odleg³oœci do punktu docelowego

    private void Start()
    {
       
    }

    private void Update()
    {

        // SprawdŸ, czy platforma jest blisko bie¿¹cego punktu docelowego
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position) < reachThreshold)
        {
            // PrzejdŸ do nastêpnego punktu docelowego
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }

        // Przesuñ platformê w kierunku bie¿¹cego punktu docelowego
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].transform.position,
            speed * Time.deltaTime
        );
    }
}
