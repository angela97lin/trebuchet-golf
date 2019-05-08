
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InfoPopup : MonoBehaviour
{
    public Button closeButton;
    
    public TMP_Text titleText;

    public TMP_Text topicText;

    // Start is called before the first frame update
    void Start()
    {
        this.closeButton.onClick.AddListener(ClosePopup);
    }


    public void PreparePEInfoPopup()
    {
        titleText.text = "Energy of the Projectile";
        topicText.fontSize = 15.0f;
        topicText.text = "Potential Energy \n The energy possessed by a body " +
        	"due to its position or configuration is called potential energy. " +
        	"The potential energy equation of a body having mass m, and height h" +
            " is PE = mgh. \n\n Kinetic Energy \n The energy possessed by a " +
            "body due to its motion is called kinetic energy. Kinetic energy " +
            "equation (KE) of a body having mass m, and velocity v, is  \n KE = 1/2mv\xB2 " +
            "\n\n How do the potential and kinetic energy change as the projectile " +
            "travels through the air?";

    }
    
    
    public void PrepareMassInfoPopup()
    {
        titleText.text = "Counterweight Mass";
        topicText.fontSize = 20.0f;
        topicText.text = "Change this meter to increase the mass of the " +
        	"counterweight. If you increase the mass of the counterweight, this " +
        	"will increase the potential energy that will be converted into the " +
            "kinetic energy of the ball. \n \n How does changing the mass of the" +
            " counterweight effect how far the ball travels?";
    }
    public void OpenPopup()
    {
        this.transform.gameObject.SetActive(true);        
    }

    public void ClosePopup()
    {
        this.transform.gameObject.SetActive(false);        
    }

    
   
}
