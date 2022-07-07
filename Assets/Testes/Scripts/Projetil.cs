using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    float dano;
    public float Dano { get { return dano; } set { dano = value; } }
    [SerializeField] Rigidbody2D rig;
    [SerializeField] float gravidadeMaxima;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag(this.tag))
        {
            if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Inimigo"))
            {
                collision.gameObject.GetComponent<ITomarDano>().ReceberDano(dano);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void Update()
    {
        if(rig.gravityScale<=gravidadeMaxima)
        {
            rig.gravityScale += Time.deltaTime * 10;
        }
    }
}
