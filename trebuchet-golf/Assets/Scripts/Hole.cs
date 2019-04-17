using System.Collections;
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
        BoxCollider col = GetComponent<BoxCollider>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(col.size.x, col.size.y, col.size.z));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(col.size.x*.95f, col.size.y*.95f, col.size.z*.95f));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FollowCameraTarget>() != null) {
            GetComponentInChildren<ParticleSystem>().Emit(25);
        }
    }
}
