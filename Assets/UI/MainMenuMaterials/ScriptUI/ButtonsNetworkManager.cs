using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;

public class ButtonsNetworkManager : MonoBehaviour
{
    [Header("NetworkButtons")]
    public TextMeshProUGUI InputIp;
    public NetworkManager netManager;

    [Header("Name")]
    public string DisplayName;
    public Button SetNameButton;
    public TMP_InputField InputName;
    public TextMeshProUGUI TexB;
    // Start is called before the first frame update
    void Awake(){

        
    }
    void Start()
    {
    
        
        InputName.text = DisplayName;;
        TexB.text = DisplayName;;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeIP(){
        netManager.networkAddress= InputIp.text ;
    }
    public void SetName(string name){
        SetNameButton.interactable = !string.IsNullOrEmpty(name);
    }
    public void SaveName(){

        DisplayName = InputName.text;
        TexB.text = DisplayName;
    }
    public void NextScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
