using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class UNetUIManaget : MonoBehaviour
{
  [SerializeField] private Button StartHost;
  [SerializeField] private Button StartServer;
  [SerializeField] private Button StartClient;

  [SerializeField] private GameObject panel;
//   [SerializeField] private GameObject cameraUI;

    void Start(){
        StartClient.onClick.AddListener(() =>{
            if (NetworkManager.Singleton.StartClient()){
                Debug.Log("Client Done!");
                SuccessConnection();
            }
            else{
                Debug.Log("Client fail :(");
            }
        });
        StartServer.onClick.AddListener(() =>{
            if (NetworkManager.Singleton.StartServer()){
                Debug.Log("Server Done!");
                SuccessConnection();
            }
            else{
                Debug.Log("Server fail :(");
            }
        });
        StartHost.onClick.AddListener(() =>{
            if (NetworkManager.Singleton.StartHost()){
                Debug.Log("Host Done!");
                SuccessConnection();
            }
            else{
                Debug.Log("Host fail :(");
            }
        });
    }
    private void SuccessConnection(){
        panel.SetActive(false);
        // cameraUI.SetActive(false);
    }
}
