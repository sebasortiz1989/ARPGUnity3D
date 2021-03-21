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

    // Start is called before the first frame update
    void Start()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerNavMeshAgent.destination = target.transform.position;
    }
}
