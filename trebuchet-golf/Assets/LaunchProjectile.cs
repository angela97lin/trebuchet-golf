using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public ProjectileSlider proj;
    [SerializeField]
    private bool canLaunch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ProjectileReady()
    {
        this.canLaunch = true;
        return this.canLaunch;
    }

    public void Reset()
    {
        this.canLaunch = false;
    }
}
