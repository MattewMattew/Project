using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using Mirror;

public class MainMenuUI :  MonoBehaviour
{
    [Header("Name")]
    public string DisplayName;
    public Button SetNameButton;
    public TMP_InputField InputName;
    public TextMeshProUGUI TexB;
    [Header("Screen")]
    public TMP_Dropdown Dropdown;
    
    private void Start(){
        if (!PlayerPrefs.HasKey("ScreenSize"))
        PlayerPrefs.SetInt("ScreenSize", 0);
        
        Dropdown.value = PlayerPrefs.GetInt("ScreenSize", 0);



        if(!PlayerPrefs.HasKey("Name"))
        PlayerPrefs.SetString("Name", "NameChange");
        
        string defaultName = PlayerPrefs.GetString("Name");
        InputName.text = defaultName;
        DisplayName = defaultName;
        TexB.text = defaultName;
        SetName(defaultName);
    }
    public void SetName(string name){
        SetNameButton.interactable = !string.IsNullOrEmpty(name);
    }
    public void SaveName(){

        DisplayName = InputName.text;
        PlayerPrefs.SetString("Name", DisplayName);
        TexB.text = DisplayName;
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
        Application.Quit();
    }
    public void StartScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    

    

}
