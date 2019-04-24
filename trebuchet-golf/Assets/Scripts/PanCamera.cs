using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanCamera : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    private Transform camera;

    private bool canPan = true;
    private float mouseX;
    private float mouseY;
    private float panSpeed = 3.5f;

    public void OnSelect(BaseEventData eventData)
    {
        this.canPan = false;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.canPan = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.canPan)
        {
            if (Input.GetMouseButton(0))
            {
                this.camera.Rotate(new Vector3(Input.GetAxis("Mouse Y") * this.panSpeed, -Input.GetAxis("Mouse X") * this.panSpeed, 0));
                this.mouseX = this.camera.rotation.eulerAngles.x;
                this.mouseY = this.camera.rotation.eulerAngles.y;
                this.camera.rotation = Quaternion.Euler(this.mouseX, this.mouseY, 0);
            }
        }
    }
}
