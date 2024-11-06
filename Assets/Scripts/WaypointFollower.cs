using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float speed = 1.0f; // pr�dko�� ruchu platformy
    [SerializeField] public Transform[] waypoints; // punkty docelowe
    private int currentWaypoint = 0; // indeks bie��cego punktu docelowego
    private float reachThreshold = 0.1f; // pr�g dla odleg�o�ci do punktu docelowego

    private void Start()
    {
       
    }

    private void Update()
    {

        // Sprawd�, czy platforma jest blisko bie��cego punktu docelowego
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position) < reachThreshold)
        {
            // Przejd� do nast�pnego punktu docelowego
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }

        // Przesu� platform� w kierunku bie��cego punktu docelowego
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].transform.position,
            speed * Time.deltaTime
        );
    }
}
