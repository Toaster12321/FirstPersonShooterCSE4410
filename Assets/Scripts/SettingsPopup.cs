using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    //shows settings menu
    public void Open()
    {
        gameObject.SetActive(true);
    }

    //closes settings menu
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnSubmitName(string name)
    {
        Debug.Log(name);
    }

    public void OnSpeedValue(float speed)
    {
        Debug.Log($"Speed: {speed}");
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }
}
