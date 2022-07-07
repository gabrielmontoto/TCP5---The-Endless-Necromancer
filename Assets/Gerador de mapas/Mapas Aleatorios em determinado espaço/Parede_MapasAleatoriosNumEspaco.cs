using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parede_MapasAleatoriosNumEspaco : MonoBehaviour
{
  //  [SerializeField] Sprite[] imagens; //retirar os comentarios pra isso servir como criaçao de salas
  
    // Start is called before the first frame update
    void Start()
    {
        
       // int rand = Random.Range(0, imagens.Length);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = ControladorParedes.Imagens;
        }
    }

}
