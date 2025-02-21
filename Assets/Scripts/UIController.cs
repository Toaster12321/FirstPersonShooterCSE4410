using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] SettingsPopup settingsPopup;

    private int _score;

    //subscribes to enemy hit event
    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    //unsubscribes from event
    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }
    //increments score label by 1 when an enemy is hit(score counter)
    private void OnEnemyHit()
    {
        _score += 1; 
        scoreLabel.text = _score.ToString();
    }

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString();
        settingsPopup.Close();
    }

    // Update is called once per frame
    void Update()
    {
        //scoreLabel.text = Time.realtimeSinceStartup.ToString();
    }

    //called when we open settings
    public void OnOpenSettings()
    {
        //Debug.Log("Opening settings...");
        settingsPopup.Open();
    }


}
