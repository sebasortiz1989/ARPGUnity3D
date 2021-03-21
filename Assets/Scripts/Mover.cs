using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    // Config

    // Cached Component References
    NavMeshAgent playerNavMeshAgent;

    // Initialize Variables


    // Start is called before the first frame update
    void Start()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToCursor();
    }

    private void MoveToCursor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInformation;
            bool hasHit = Physics.Raycast(ray, out hitInformation);
            if (hasHit)
            {
                playerNavMeshAgent.destination = hitInformation.point;
            }
        }
    }
}
