using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste_Inimigos : Individuo
{
    public TipoInimigo tipo;
    public static Action<float, Sprite, GameObject, TipoInimigo> EventoDePossessao;
    [SerializeField] Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        Ataque(tipo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MovimentacaoInimigo()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (EventoDePossessao != null && collision.gameObject.CompareTag("Player"))
        {
            EventoDePossessao(vida, sprite, this.gameObject, tipo);
           // this.gameObject.SetActive(false);
        }
    }
    protected override void Ataque(TipoInimigo tipo)
    {
        print("teste");
        //ataque arqueiro
        //ataque guerreiro
        //ataque mago
    }
}
