
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InfoPopupButton : MonoBehaviour
{
    
    public InfoPopup infoPopup;
    public Button button;
    public string topic;

    public void Start()
    {
        this.button.onClick.AddListener(OpenInfoPopup);

    }
    
    public void OpenInfoPopup()
    {
        Debug.Log("hellooooo1");
        if (topic == "PE")
        {
            infoPopup.PreparePEInfoPopup();
        }
        
        if (topic == "Mass")
        {
            infoPopup.PrepareMassInfoPopup();
        }        
        
        infoPopup.OpenPopup();
    }
    
   
}
