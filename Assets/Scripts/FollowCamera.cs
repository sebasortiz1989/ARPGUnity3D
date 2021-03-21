using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Config
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.position;
    }
}
