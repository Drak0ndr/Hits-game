using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;

    public void Start() {
        loadSettings();
        setQuality();
        saveSettings();
    }
    public void setQuality() {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void saveSettings() {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
    }

    public void loadSettings() {
        if(PlayerPrefs.HasKey("QualitySettingPreference")) {
            qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        } else {
            qualityDropdown.value = 6;
            PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        }
    }
}
