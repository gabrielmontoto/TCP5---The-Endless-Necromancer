using MiscUtil.Xml.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteMovimentacaoInimigo : MonoBehaviour
{
  //  [SerializeField] Transform pos1, pos2;
    float velocidade, velocidadeNeg, velocidadePos;
    // [SerializeField] Transform posInicial;
 //   [SerializeField] Vector3 ProxPos;
  //  [SerializeField] Vector3 ultimaPos;
    [SerializeField] bool PerseguirJogador, velocidadeAplicada;

    [SerializeField] Transform detectorDireita;//, detectorEsquerda;
    [SerializeField] float distancia;
    [SerializeField] LayerMask layerMask;
    int layer_mask;


    Rigidbody2D rb;
    // [SerializeField] SpriteRenderer sr;
    [Space] [Header("Vinculacoes internas")] [SerializeField] Teste_Player player;
    Inimigos inimigo;
    [SerializeField] float rangeQuad;
    [SerializeField] Collider2D colisorDireita;//, colisorEsquerda;
    //[SerializeField] float movimentacao;
    // Start is called before the first frame update
    void Start()
    {
        layer_mask = layerMask.value;
        print(layer_mask);
     //   ProxPos = pos2.position;
     //   ultimaPos = pos2.position;
        //   sr = GetComponent<SpriteRenderer>();
        inimigo = GetComponent<Inimigos>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Teste_Player>();
        this.velocidade = inimigo.Velocidade;
        velocidadeNeg = -velocidade;
        velocidadePos = velocidade;
    }

    // Update is called once per frame
    void Update()
    {
        //print("Player pos local: " + player.transform.localPosition + " / pos normal: " + player.transform.position);
     //   print("Inimigo pos local: " + rb.transform.localPosition + " / pos normal: " + rb.transform.position);
        if (inimigo.EstadosInimigo == EstadosIaInimigo.Patrulhando)
        {
            novaMovimentacao();


            if (rb.velocity.x > 0)
            {
                inimigo.ViradoDireita = true;
                  transform.rotation = Quaternion.identity;
            }
            else if (rb.velocity.x < 0)
            {
                inimigo.ViradoDireita = false;
                  transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
        else if (inimigo.EstadosInimigo == EstadosIaInimigo.Especial)
        {
            if ( player.transform.position.x > rb.transform.position.x)
            {
                inimigo.ViradoDireita = true;
                 transform.rotation = Quaternion.identity;
                
            }
            else if ( player.transform.position.x < rb.transform.position.x)
            {
                inimigo.ViradoDireita = false;
                  transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                print("????????????");
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if (inimigo.EstadosInimigo == EstadosIaInimigo.Patrulhando)
        {
            rb.velocity = Vector2.right * velocidade;
            velocidadeAplicada = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(colisorDireita.IsTouching(collision) && collision.CompareTag("Player"))//|| colisorEsquerda.IsTouching(collision) && collision.CompareTag("Player"))
        {
            PerseguirJogador = true;



        }
    }


    private void AntigaMovimentacao()
    {
     /*   if (player.transform.position.x <= pos2.transform.position.x && player.transform.position.x >= pos1.transform.position.x)
        {
            if (player.transform.position.y <= rb.transform.position.y + rangeQuad && player.transform.position.y >= rb.transform.position.y - rangeQuad)
            {

                ProxPos = player.transform.position;
                PerseguirJogador = true;
                if (player.transform.position.x < rb.transform.position.x)
                {
                    velocidade = velocidadeNeg;
                }
                else if (player.transform.position.x > rb.transform.position.x)
                {
                    velocidade = velocidadePos;
                }


            }
            else
            {
                ProxPos = ultimaPos;
                // ultimaPos = Vector3.one;
                PerseguirJogador = false;
            }
        }
        else
        {
            ProxPos = ultimaPos;
            // ultimaPos = Vector3.one;
            PerseguirJogador = false;
        }
        if (PerseguirJogador == false) //&& ProxPos != ultimaPos)
        {
            if (rb.transform.position.x - pos1.transform.position.x <= 0f)
            {
                ProxPos = pos2.position;
                ultimaPos = ProxPos;
                velocidade = velocidadePos;
                //   print("pos1");

            }
            else if (rb.transform.position.x - pos2.transform.position.x >= 0f)
            {
                ProxPos = pos1.position;
                ultimaPos = ProxPos;
                velocidade = velocidadeNeg;
                //   print("pos2");
            }

        }
        if (rb.velocity.x > 0)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (rb.velocity.x < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
     */
    }
    void novaMovimentacao()
    {
        RaycastHit2D ray;
      //  if (inimigo.ViradoDireita)
         ray = Physics2D.Raycast(detectorDireita.position, Vector2.down, distancia, layer_mask);
     /*   else
        {
            ray = Physics2D.Raycast(detectorEsquerda.position, Vector2.down, distancia, layer_mask);
        }*/
       
       // print(ray.collider.name);
        if (rb.velocity.x == 0)
        {
            if(velocidade>0)
            {
                velocidade = velocidadeNeg;
            }
            else if(velocidade<0)
            {
                velocidade = velocidadePos;
            }

            //velocidade *= -1;
        }
        else if (ray==false && velocidadeAplicada == false)
        {
            if (velocidade > 0)
            {
                velocidade = velocidadeNeg;
                
            }
            else if (velocidade < 0)
            {
                velocidade = velocidadePos;
            }
            velocidadeAplicada = true;
          //  print("alterar velocidade");
          //  velocidade *= -1;
        }




    }


}