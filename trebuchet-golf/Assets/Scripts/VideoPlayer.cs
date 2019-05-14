using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class VideoPlayer : MonoBehaviour
{
    Camera cam;

    private UnityEngine.Video.VideoPlayer vid;
    void Start()
    {
       
        vid = this.GetComponent<UnityEngine.Video.VideoPlayer> ();
        vid.url = System.IO.Path.Combine (Application.streamingAssetsPath,"treba2.mp4");
        vid.Play();

    }



    
}
