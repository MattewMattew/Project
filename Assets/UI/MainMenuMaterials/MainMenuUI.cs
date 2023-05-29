using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MainMenuUI : MonoBehaviour
{
    
    public TMPro.TMP_Dropdown Dropdown;
    
    private void Start(){
        if (!PlayerPrefs.HasKey("ScreenSize"))
        PlayerPrefs.SetInt("ScreenSize", 0);
        
        Dropdown.value = PlayerPrefs.GetInt("ScreenSize", 0);
    }
    void Awake(){
         Dropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt("ScreenSize", Dropdown.value);
            
        }));
    }
    public void Change(){
          
        if(Dropdown.value == 0){
            Screen.SetResolution(1920, 1080, true);
            
        }
        else if (Dropdown.value == 1){
            Screen.SetResolution(1366, 768, true);
            
        }
        else if (Dropdown.value == 2)
        {
            Screen.SetResolution(1280, 1024, true);
            
        }
        else if (Dropdown.value == 3){
            Screen.SetResolution(800, 600, true);
            
        }
        
    }
    public void CloseGame(){
         Application.Quit();}

}
