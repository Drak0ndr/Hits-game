using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    public TMP_Dropdown qualityDropdown;
    public Slider renderDistance;
    public TMP_Text renderValue;
    public void Start() {
        loadSettings();
        setQuality();
        saveSettings();
    }
    public void setQuality() {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void setRenderDist() {
        renderValue.SetText(renderDistance.value.ToString());
    }
    public void saveSettings() {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("RenderVal", (int)renderDistance.value);
    }

    public void loadSettings() {
        if(PlayerPrefs.HasKey("QualitySettingPreference")) {
            qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        } else {
            qualityDropdown.value = 6;
            PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        }

        if(PlayerPrefs.HasKey("RenderVal")) {
            renderDistance.value = PlayerPrefs.GetInt("RenderVal");
        } else {
            renderDistance.value = 9;
            PlayerPrefs.SetInt("RenderVal", (int)renderDistance.value);
        }

        setRenderDist();
    }
}
