using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript_Possuido : MonoBehaviour
{
    [SerializeField] int valor1, valor2, valor3, valor4, vida;
    // Start is called before the first frame update
    public static Action<int,Sprite,GameObject> EventoDePossessao;
   
    [SerializeField] Sprite sprite;
    void Start()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (EventoDePossessao != null)
        {
            EventoDePossessao(vida,sprite,this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

}
