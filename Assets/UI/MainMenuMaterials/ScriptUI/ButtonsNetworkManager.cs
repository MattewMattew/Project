using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;
using System;
using System.Text;

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
    void Awake() {


    }
    void Start()
    {


        InputName.text = DisplayName; ;
        TexB.text = DisplayName; ;


    }

    // Update is called once per frame
    void Update()
    {


    }
    public void ChangeIP() {
/*        InputIp.text.Remove(0);
        print(InputIp.text.Length);
        netManager.networkAddress = InputIp.text;*/
        StartCoroutine(update());

    }
    IEnumerator update()
    {
        while (1 != 0)
        {
            StringBuilder ip = new StringBuilder();
            /*var test = GUILayout.TextField(InputIp.text);*/
            for (int i = 0; i < InputIp.text.Length; i++)
            {
                print(InputIp.text[i]);
                if (InputIp.text[i] != '​')
                {
                    ip.Append(InputIp.text[i]);
                }
            }
            print($"{ip.ToString() == netManager.networkAddress}  InputIp.text {ip.Length} networkAddress {netManager.networkAddress.Length} " );
            netManager.networkAddress = ip.ToString();
            yield return new WaitForSeconds(0.1f);
        }
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
