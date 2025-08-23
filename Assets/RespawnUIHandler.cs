using System;
using UnityEngine;
using UnityEngine.UI;

public class RespawnUIHandler : MonoBehaviour
{
    public Button respawnButton;

    void Start()
    {
        respawnButton.onClick.AddListener(OnRespawnButtonPressed);
        gameObject.SetActive(false);
    }

    private void OnRespawnButtonPressed()
    {
        throw new NotImplementedException();
    }
}
