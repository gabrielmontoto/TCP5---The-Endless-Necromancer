using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individuo : MonoBehaviour
{
    [SerializeField]
    [Header("Gerais")]protected int nivel;
    [SerializeField] protected int experiencia, experienciaPorNivel;
    [SerializeField] protected float vida;
    [SerializeField] protected float vidaMaxima;
    [SerializeField] protected float dano;
    [SerializeField] protected float velocidadeAndar;
    [SerializeField] protected float forcaSalto;
    [SerializeField] protected float contAtaqueEspecial,contEspecialMax;
    [SerializeField] protected float contDuracaoAtaqueEspecial, contDuracaoAtaqueEspecialMax;
    [SerializeField] protected float contAtaque,contAtaqueMax;
    [SerializeField] protected float defesa;
    [SerializeField] protected Rigidbody2D rigidbody2d;
    [SerializeField] protected bool viradoDireita;
    [SerializeField] protected float distanciaAtaque;

    public bool ViradoDireita { get { return viradoDireita; } set { viradoDireita = value; } }
    [Space][Header("Guerreiro")]
    [SerializeField][Tooltip("onde o ataque do guerreiro vai sair ou as flechas do arqueiro, ou a bola do mago...")] 
   protected GameObject SaidaAtaqueDireita;
    [SerializeField] [Tooltip("raio do ataque")]protected float raioAtaque;
    [SerializeField] [Tooltip("geral")]protected LayerMask emQueLayerEstaAlvo;
    [SerializeField] protected float forcaDashEspecial;
    [SerializeField] protected bool forcaAtribuida;
    [SerializeField] protected float danoEspecial;

    [Space]
    [Header("Arqueiro")][SerializeField] GameObject flecha;
    [SerializeField] float velocidadeFlecha;


    [Space] [SerializeField]protected bool ataqueOuEspecialCausado;
    [SerializeField]protected Guerreiro_Animacoes guerreiroAnimacoes;

    [Space][Header("efeitos")]
    [SerializeField] protected GameObject _paiParticulas;
    //[SerializeField] Transform spawn;

    [SerializeField] GameObject sangueAtaque;
    [SerializeField] GameObject sangueHabilidade;

    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject hitTrail;


    [SerializeField] public bool podeHabilidade;
    protected virtual void Ataque(TipoInimigo tipo){

       // if (contAtaque <= 0)
        {
            
            switch (tipo)
            {
                case TipoInimigo.Gurreiro:
                    //print("ataque causado");
                    //animacao ataque
                    //  Collider2D[] InimigosQuerecebemAtaque;
                    //  InimigosQuerecebemAtaque = Physics2D.OverlapCircleAll(SaidaAtaqueDireita.transform.position, raioAtaque, emQueLayerEstaAlvo);
                    if (guerreiroAnimacoes.Colisores != null)
                    {

                        for (int i = 0; i < guerreiroAnimacoes.Colisores.Count; i++)
                        {
                            //  if (InimigosQuerecebemAtaque[i].GetComponent<ITomarDano>() != null)
                            if (guerreiroAnimacoes.Colisores[i].GetComponent<ITomarDano>() != null)
                            {
                                //print("teste-----");
                                if(this.gameObject.tag!=guerreiroAnimacoes.Colisores[i].tag)
                                guerreiroAnimacoes.Colisores[i].GetComponent<ITomarDano>().ReceberDano(dano);
                                //InimigosQuerecebemAtaque[i].GetComponent<ITomarDano>().ReceberDano(dano);
                                //  ataqueOuEspecialCausado = true;
                                HitEffect(guerreiroAnimacoes.Colisores[i].transform, "Ataque");
                                

                                CameraShake.Instancia.ShakeDaCAmera(2f, 0.3f);
                            }
                        }
                        guerreiroAnimacoes.Colisores = new List<Collider2D>();
                    }
                    
                    break;
                case TipoInimigo.Mago:
                    break;
                case TipoInimigo.Arqueiro:
                    GameObject obj;
                  //  if (viradoDireita)
                         obj = Instantiate(flecha, SaidaAtaqueDireita.transform.position, Quaternion.identity);
                   // else
                      //  obj = Instantiate(flecha, SaidaAtaqueEsquerda.transform.position, Quaternion.identity);

                    obj.tag = this.tag;
                  //  print(this.tag);
                    if(viradoDireita)
                    obj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * velocidadeFlecha);
                    else
                        obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * velocidadeFlecha);
                    obj.GetComponent<Projetil>().Dano = dano;
                    Destroy(obj, 10);
                    break;
                case TipoInimigo.Vazio:
                    break;
                default:
                    break;
            }
           // contAtaque = contAtaqueMax;
        }

    }

    protected virtual EstadosIaInimigo Especial(TipoInimigo tipo)
    {

     //   if (contAtaqueEspecial <= 0)
        {
            
            switch (tipo)
            {
                case TipoInimigo.Gurreiro:
                    //print("ataque Especial");
                 //   if (contDuracaoAtaqueEspecial >= 0)
                    {
                        if (!forcaAtribuida)
                        {
                            if (viradoDireita)
                            {
                                rigidbody2d.AddForce(Vector2.right * forcaDashEspecial); //esse incremento acontece por um tempo
                                if (rigidbody2d.velocity.x > 25)
                                {
                                    rigidbody2d.velocity = new Vector2(25, rigidbody2d.velocity.y);
                                }
                            }
                            else
                            {
                                rigidbody2d.AddForce(Vector2.left * forcaDashEspecial);
                                if (rigidbody2d.velocity.x < -25)
                                {
                                    rigidbody2d.velocity = new Vector2(-25, rigidbody2d.velocity.y);
                                }
                            }
                            forcaAtribuida = true;
                        }

                        if (guerreiroAnimacoes.Colisores != null)
                        {

                            for (int i = 0; i < guerreiroAnimacoes.Colisores.Count; i++)
                            {
                                //  if (InimigosQuerecebemAtaque[i].GetComponent<ITomarDano>() != null)
                                if (guerreiroAnimacoes.Colisores[i].GetComponent<ITomarDano>() != null)
                                {
                                    //print("teste-----");
                                    guerreiroAnimacoes.Colisores[i].GetComponent<ITomarDano>().ReceberDano(danoEspecial);
                                    //InimigosQuerecebemAtaque[i].GetComponent<ITomarDano>().ReceberDano(dano);
                                    //  ataqueOuEspecialCausado = true;
                                    HitEffect(guerreiroAnimacoes.Colisores[i].transform, "Habilidade");
                                  //  guerreiroAnimacoes.Colisores = new List<Collider2D>();

                                    CameraShake.Instancia.ShakeDaCAmera(2f, 0.3f);
                                }
                            }
                            guerreiroAnimacoes.Colisores = new List<Collider2D>();
                        }
                        //  contDuracaoAtaqueEspecial -= Time.deltaTime;
                        //isso fica acontecendo ate o final

                        /*   Collider2D[] InimigosQuerecebemAtaque;
                          // if (viradoDireita)
                               InimigosQuerecebemAtaque = Physics2D.OverlapCircleAll(SaidaAtaqueDireita.transform.position, raioAtaque, emQueLayerEstaAlvo);
                           //   else
                           //  InimigosQuerecebemAtaque = Physics2D.OverlapCircleAll(SaidaAtaqueEsquerda.transform.position, raioAtaque, emQueLayerEstaAlvo);
                           if (ataqueOuEspecialCausado)
                           {
                               return EstadosIaInimigo.Parado;
                           }
                           for (int i = 0; i < InimigosQuerecebemAtaque.Length; i++)
                           {
                               if (InimigosQuerecebemAtaque[i].GetComponent<ITomarDano>() != null)
                               {
                                   InimigosQuerecebemAtaque[i].GetComponent<ITomarDano>().ReceberDano(dano);
                                   ataqueOuEspecialCausado = true;
                               }
                           }*/
                        //  return EstadosIaInimigo.Especial;
                    }
                   /* else
                    {
                        contDuracaoAtaqueEspecial = contDuracaoAtaqueEspecialMax;
                        
                    }*/
                    break;
                case TipoInimigo.Mago:
                    break;
                case TipoInimigo.Arqueiro:
                    print("ataque Especial");
                    /*      GameObject obj;
                         // if (viradoDireita)
                              obj = Instantiate(flecha, SaidaAtaqueDireita.transform.position, Quaternion.identity);
                         // else
                          //    obj = Instantiate(flecha, SaidaAtaqueEsquerda.transform.position, Quaternion.identity);

                          obj.tag = this.tag;
                          print(this.tag);
                          obj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * velocidadeFlecha);
                          obj.GetComponent<Projetil>().Dano = dano;
                          Destroy(obj, 10);*/
                    contAtaqueEspecial = contEspecialMax;
                    break;
                case TipoInimigo.Vazio:
                    break;
                default:
                    break;
            }
           // contAtaqueEspecial = contEspecialMax;
            
        }
        return EstadosIaInimigo.Patrulhando;
    }

    protected void HitEffect(Transform spawn, string tipoAtaque)
    {
        if (this.transform.CompareTag("Player")) // Possuido
        {
            GameObject go = Instantiate(hitEffect, new Vector3(spawn.transform.position.x, spawn.transform.position.y,
                       spawn.transform.position.z), Quaternion.identity);
            go.transform.SetParent(_paiParticulas.transform);

            for (int i = 0; i < go.transform.childCount; i++)
            {
                go.transform.GetChild(i).GetComponent<ParticleSystem>().startColor = new Color(150, 0, 150);
            }

            Destroy(go.gameObject, 1f);

            if (this.transform.position.x > spawn.transform.position.x) // esquerda
            {
                GameObject go2 = Instantiate(hitTrail, new Vector3(spawn.transform.position.x + 4.8f, spawn.transform.position.y - 2.8f,
                            spawn.transform.position.z), Quaternion.identity);
                go2.transform.SetParent(_paiParticulas.transform);
                go2.GetComponent<ParticleSystem>().startColor = new Color(150, 0, 150);

                if (tipoAtaque == "Ataque")
                    go2.transform.Rotate(new Vector3(0, 0, 180));
                else if (tipoAtaque == "Habilidade")
                    go2.transform.Rotate(new Vector3(0, 0, 90));

            }
            else // direita
            {
                GameObject go3 = Instantiate(hitTrail, new Vector3(spawn.transform.position.x - 4.8f, spawn.transform.position.y - 2.8f,
                            spawn.transform.position.z), Quaternion.identity);
                go3.transform.SetParent(_paiParticulas.transform);
                go3.GetComponent<ParticleSystem>().startColor = new Color(150, 0, 150);

                if (tipoAtaque == "Ataque")
                    go3.transform.Rotate(new Vector3(0, -180, 180));
                else if (tipoAtaque == "Habilidade")
                    go3.transform.Rotate(new Vector3(0, 180, 90));
            }

            if (tipoAtaque == "Ataque") 
            {
                GameObject go4 = Instantiate(sangueAtaque, new Vector3(spawn.transform.position.x, spawn.transform.position.y,
                            spawn.transform.position.z), Quaternion.identity);
                go4.transform.SetParent(_paiParticulas.transform);
                Destroy(go4.gameObject, 3f);
            }else if (tipoAtaque == "Habilidade")
            {
                GameObject go4 = Instantiate(sangueHabilidade, new Vector3(spawn.transform.position.x, spawn.transform.position.y,
                            spawn.transform.position.z), Quaternion.identity);
                go4.transform.SetParent(_paiParticulas.transform);
                Destroy(go4.gameObject, 3f);
            }
                
        }
        else // Normal
        {
            GameObject go = Instantiate(hitEffect, new Vector3(spawn.transform.position.x, spawn.transform.position.y,
                       spawn.transform.position.z), Quaternion.identity);
            go.transform.SetParent(_paiParticulas.transform);
            Destroy(go.gameObject, 1f);

            if (this.transform.position.x > spawn.transform.position.x) // esquerda
            {
                GameObject go2 = Instantiate(hitTrail, new Vector3(spawn.transform.position.x + 4.8f, spawn.transform.position.y - 2.8f,
                            spawn.transform.position.z), Quaternion.identity);
                go2.transform.SetParent(_paiParticulas.transform);

                if (tipoAtaque == "Ataque")
                    go2.transform.Rotate(new Vector3(0, 0, 180));
                else if (tipoAtaque == "Habilidade")
                    go2.transform.Rotate(new Vector3(0, 0, 90));

            }
            else // direita
            {
                GameObject go3 = Instantiate(hitTrail, new Vector3(spawn.transform.position.x - 4.8f, spawn.transform.position.y - 2.8f,
                            spawn.transform.position.z), Quaternion.identity);
                go3.transform.SetParent(_paiParticulas.transform);

                if (tipoAtaque == "Ataque")
                    go3.transform.Rotate(new Vector3(0, -180, 180));
                else if (tipoAtaque == "Habilidade")
                    go3.transform.Rotate(new Vector3(0, 180, 90));
            }


            if (tipoAtaque == "Ataque")
            {
                GameObject go4 = Instantiate(sangueAtaque, new Vector3(spawn.transform.position.x, spawn.transform.position.y,
                            spawn.transform.position.z), Quaternion.identity);
                go4.transform.SetParent(_paiParticulas.transform);
                Destroy(go4.gameObject, 3f);
            }
            else if (tipoAtaque == "Habilidade")
            {
                GameObject go4 = Instantiate(sangueHabilidade, new Vector3(spawn.transform.position.x, spawn.transform.position.y,
                            spawn.transform.position.z), Quaternion.identity);
                go4.transform.SetParent(_paiParticulas.transform);
                Destroy(go4.gameObject, 3f);
            }
        }

        
    }
}
