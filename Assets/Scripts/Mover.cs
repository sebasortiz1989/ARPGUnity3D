using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    // Config
    [SerializeField] Transform target;

    // Cached Component References
    NavMeshAgent playerNavMeshAgent;

    // Initialize Variables
    Ray lastRay;

    // Start is called before the first frame update
    void Start()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.red);
        playerNavMeshAgent.destination = target.transform.position;
    }
}
