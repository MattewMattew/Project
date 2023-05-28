using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalChageScript : MonoBehaviour
{
    private bool active = false;
    private void Start(){
        int ID = PlayerPrefs.GetInt("LocalId", 0);
        ChangeLocale(ID);
        
    }
    public void ChangeLocale(int localeID){
        if (active == true)
          return; 
        StartCoroutine(SetLocale(localeID));
    }

IEnumerator SetLocale(int _localeID){
    active = true;
    yield return LocalizationSettings.InitializationOperation;
    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID]; 
    PlayerPrefs.SetInt("LocalId", _localeID);
    active = false;
}
}