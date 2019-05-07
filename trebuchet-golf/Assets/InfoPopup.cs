
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
        titleText.text = "PE title here";
        topicText.text = "PE info here";
    }
    
    
    public void PrepareMassInfoPopup()
    {
        titleText.text = "Mass title here";
        topicText.text = "Mass info here";
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
