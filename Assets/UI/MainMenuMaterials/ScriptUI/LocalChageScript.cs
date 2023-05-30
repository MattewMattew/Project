using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalChageScript : MonoBehaviour
{
    private void Start(){
        if (!PlayerPrefs.HasKey("LocalId"))
        PlayerPrefs.SetInt("LocalId", 0);
        int ID = PlayerPrefs.GetInt("LocalId");
        ChangeLocale(ID);
    }
    private bool active = false;
    public void ChangeLocale(int localeID){
        if (active)
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