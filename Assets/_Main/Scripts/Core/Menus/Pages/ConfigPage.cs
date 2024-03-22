using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfigPage : MenuPage
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private UI_Items ui;
    private GameObject activePanel;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        activePanel = panels[0];

        SetAvailableResolutions();
    }
    public void OpenPanel(string panelName)
    {
        GameObject panel = panels.First(panel => panel.name.ToLower() == panelName.ToLower());

        if (panel == null)
        {
            Debug.LogWarning($"Did not find panel called '{panelName}' in config menu");
            return;
        }

        if (activePanel != null && activePanel != panel)
            activePanel.SetActive(false);

        panel.SetActive(true);
        activePanel = panel;
    }

    private void SetAvailableResolutions()
    {
        Resolution[] resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        for (int i = resolutions.Length - 1; i >= 0; i++)
        {
            options.Add($"{resolutions[i].width}x{resolutions[i].height}");
        }

        ui.resolutions.ClearOptions();
        ui.resolutions.AddOptions();
    }

    [System.Serializable]
    public class UI_Items
    {
        [Header("General")]
        public Button fullscreen;
        public Button windowed;
        public TMP_Dropdown resolutions;
        public Button skippingContinue, skippingStop;
        public Slider architectSpeed, autoreaderSpeed;

        [Header("Audio")]
        public Slider musicVolume;
        public Slider sfxVolume;
        
    }
}
