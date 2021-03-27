using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjects : MonoBehaviour
{
    // Config
    [SerializeField] GameObject player;

    // String const
    private const string PLAYER_TAG = "Player";

    // Initialize variables
    List<RaycastHit> invisibleObjects = new List<RaycastHit>();
    bool objectsDissapeared;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MakeObstacleObjectsInvisible();       
    }

    private void MakeObstacleObjectsInvisible()
    {
        Ray ray = Camera.main.ScreenPointToRay(player.transform.position + new Vector3(0,1,0));
        RaycastHit[] hits = Physics.RaycastAll(ray);

        if (hits.Length > 1 && !objectsDissapeared)
        {
            for (int i = 0; i < hits.Length - 1; i++)
            {
                invisibleObjects.Add(hits[i]);
                hits[i].transform.GetComponent<MeshRenderer>().enabled = false;
            }
            objectsDissapeared = true;
        }

        if (hits.Length == 1 && objectsDissapeared)
        {
            for (int i = 0; i < invisibleObjects.Count; i++)
            {
                invisibleObjects[i].transform.GetComponent<MeshRenderer>().enabled = true;
            }
            objectsDissapeared = false;
            invisibleObjects = new List<RaycastHit>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, player.transform.position + new Vector3(0,1,0));
    }
}
