using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalChageScript : MonoBehaviour
{   
    private void Start(){

        if (!PlayerPrefs.HasKey("LocalId"))
        PlayerPrefs.SetInt("LocalId", 0);

        int id = PlayerPrefs.GetInt("LocalId",0 );
        ChangeLocale(id);
    }
    private bool active = false;
    public void ChangeLocale(int localeID){
        if (active)
          return; 
        StartCoroutine(SetLocale(localeID));
        localeID = PlayerPrefs.GetInt("LocalId");
    }

    IEnumerator SetLocale(int _localeID){
        active = true;
        print(LocalizationSettings.SelectedLocale); // NE Trogat'!!!!!!!!!!!
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        yield return LocalizationSettings.InitializationOperation;
        print(LocalizationSettings.SelectedLocale);// NE Trogat'!!!!!!!!!!!
        PlayerPrefs.SetInt("LocalId", _localeID);
        active = false;
    }
}