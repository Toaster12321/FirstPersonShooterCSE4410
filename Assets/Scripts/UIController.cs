using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] SettingsPopup settingsPopup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        settingsPopup.Close();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = Time.realtimeSinceStartup.ToString();
    }

    //called when we open settings
    public void OnOpenSettings()
    {
        //Debug.Log("Opening settings...");
        settingsPopup.Open();
    }
}
