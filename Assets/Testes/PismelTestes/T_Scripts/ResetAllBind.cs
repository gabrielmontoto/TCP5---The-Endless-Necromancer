using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetAllBind : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    public void ResetAllBindinds() 
    {
        foreach(InputActionMap map in inputActions.actionMaps) 
        {
            map.RemoveAllBindingOverrides();
        }
    }
}
