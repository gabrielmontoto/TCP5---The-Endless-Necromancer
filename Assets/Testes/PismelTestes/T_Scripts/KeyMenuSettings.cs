using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class KeyMenuSettings : MonoBehaviour
{
    [SerializeField] GameObject gamepadButtons, keyboardButtons;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] Teste_Player player;

    public void GamepadActive() 
    {
        player.SetDevice(false);
        gamepadButtons.active = true;
        keyboardButtons.active = false;
    }

    public void KeyboadActive() 
    {
        player.SetDevice(true);
        gamepadButtons.active = false;
        keyboardButtons.active = true;
    }

    public void ResetAllBindinds()
    {
        foreach (InputActionMap map in inputActions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
        //PlayerPrefs.DeleteKey("rebinds");
    }

    //public void OnEnable() 
    //{
    //    var rebinds = PlayerPrefs.GetString("rebinds");
    //    if(!string.IsNullOrEmpty(rebinds))
    //    {
    //        inputActions.LoadFromJson(rebinds);
    //    }
    //}

    //public void OnDisable()
    //{
    //    var rebinds = inputActions.ToJson();
    //    PlayerPrefs.SetString("rebinds", rebinds);

    //    Debug.Log(rebinds);
    //}
}
