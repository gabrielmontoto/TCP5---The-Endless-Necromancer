using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste_Giro : MonoBehaviour
{
    Gamepad_Input controls;
    [SerializeField] Vector3 move;
    [SerializeField] float angulo,ultimoAngulo;
    [SerializeField] GameObject objetoQueVaiGirar;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new Gamepad_Input();
        controls.InputCalls.DashControl.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.InputCalls.DashControl.canceled += ctx => move = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        angulo = (Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg) + 90;
        if (angulo != ultimoAngulo)
        {
            if (move.x != 0 || move.y != 0)
            {
                ultimoAngulo = angulo;
                // objetoQueVaiGirar.transform.rotation = new Quaternion(0,0,move.x * move.y,0);// = new Quaternion(move.x,move.y,0,0) ;
                objetoQueVaiGirar.transform.eulerAngles = new Vector3(0, 180, angulo);
            }
        }
    }
    private void OnEnable()
    {
        controls.InputCalls.Enable();
    }

    private void OnDisable()
    {
        controls.InputCalls.Disable();
    }
}
