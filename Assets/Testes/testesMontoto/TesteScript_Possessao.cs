using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteScript_Possessao : MonoBehaviour
{
    [SerializeField] int valor1, valor2, valor3, valor4, vida, vidaPossuido;
    [SerializeField] Sprite sprite;
    [SerializeField] GameObject possuido;
    [SerializeField] SpriteRenderer sr;

    public static Action<int> ContagemInimigosPossuidos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        testScript_Possuido.EventoDePossessao += EventoDePossessao;
    }

    private void EventoDePossessao(int obj, Sprite sprite, GameObject possuido)//, Transform transform)
    {
        vidaPossuido = obj;
        sr.sprite = sprite;
        this.possuido = possuido;
        possuido.transform.SetParent(this.transform);
        if (ContagemInimigosPossuidos != null)
        {
            ContagemInimigosPossuidos(1);
           
        }
        // this.transform.position = transform.position;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && possuido!=null)
        {
            possuido.transform.SetParent(null);
            possuido.GetComponent<BoxCollider2D>().enabled = false;
            possuido.SetActive(true);
            possuido.transform.position = this.transform.position;
            vidaPossuido = 0;
            possuido = null;
            this.GetComponent<SpriteRenderer>().sprite = null;
            
        }
    }
    private void OnDisable()
    {
        testScript_Possuido.EventoDePossessao -= EventoDePossessao;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
      //  this.sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
      //  vidaPossuido = collision.gameObject.GetComponent<testScript_Possuido>().Vida;
      //  sr.sprite = sprite;
       // collision.gameObject.SetActive(false);
       // possuido = collision.gameObject;
      //  collision.gameObject.transform.SetParent(this.gameObject.transform);
    }
}
