﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x*5, transform.localScale.y, transform.localScale.z*5));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x*5*.95f, transform.localScale.y*.95f, transform.localScale.z*5*.95f));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FollowCameraTarget>() != null) {
            GetComponentInChildren<ParticleSystem>().Emit(25);
        }
    }
}
