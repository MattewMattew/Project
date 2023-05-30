using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalChageScript : MonoBehaviour
{
    private void Start(){
        int ID = PlayerPrefs.GetInt("LocalId", 0);
        ChangeLocale(ID);
    }
    private bool active = false;
    public void ChangeLocale(int localeID){
        if (active == true)
          return; 
        StartCoroutine(SetLocale(localeID));
    }

IEnumerator SetLocale(int _localeID){
    active = true;
    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
    yield return LocalizationSettings.InitializationOperation;
    PlayerPrefs.SetInt("LocalId", _localeID);
    active = false;
}
}