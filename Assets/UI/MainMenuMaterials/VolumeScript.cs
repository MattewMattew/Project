using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VolumeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private TMP_Text VolumeTextUI = null;
    [SerializeField] private Slider mVolumeSlider = null;
    [SerializeField] private TMP_Text mVolumeTextUI = null;

    [SerializeField] private Slider sfxVolumeSlider = null;

    [SerializeField] private TMP_Text sfxVolumeTextUI = null;
    public AudioSource MusicVolume;
    public AudioSource SoundVolume;

    private void Start(){
        if (!PlayerPrefs.HasKey("VolumeValue"))
        PlayerPrefs.SetFloat("VolumeValue", 100);
        if (!PlayerPrefs.HasKey("mVolumeValue"))
        PlayerPrefs.SetFloat("mVolumeValue", 100);
        if (!PlayerPrefs.HasKey("sVolumeValue"))
        PlayerPrefs.SetFloat("sVolumeValue", 100);
        LoadValues();
        
    }
    public void sfxSlider(float volume){
        sfxVolumeTextUI.text= volume.ToString();
    }
    public void VolumeSlider(float volume){
        VolumeTextUI.text= volume.ToString();
        
    }
    public void SoundSlider(float volume){
        mVolumeTextUI.text= volume.ToString();
    }
    public void SaveVolumeButton(){
        float allvolumeValue = volumeSlider.value; 
        float musicvolumeValue = mVolumeSlider.value; 
        float svolumeValue = sfxVolumeSlider.value; 
        PlayerPrefs.SetFloat("VolumeValue", allvolumeValue);
        PlayerPrefs.SetFloat("mVolumeValue", musicvolumeValue);
        PlayerPrefs.SetFloat("sVolumeValue", svolumeValue);
        LoadValues();
    }
    void LoadValues(){
        float allvolumeValue = PlayerPrefs.GetFloat("VolumeValue"); 
        float musicvolumeValue = PlayerPrefs.GetFloat("mVolumeValue"); 
        float svolumeValue = PlayerPrefs.GetFloat("sVolumeValue"); 
        volumeSlider.value = allvolumeValue; 
        mVolumeSlider.value = musicvolumeValue; 
        sfxVolumeSlider.value = svolumeValue; 
        Debug.Log(allvolumeValue );
        Debug.Log(musicvolumeValue );
        Debug.Log(svolumeValue );
        MusicVolume.volume= musicvolumeValue/100;
        SoundVolume.volume= svolumeValue/100;
        AudioListener.volume = allvolumeValue/100;
    }
}
