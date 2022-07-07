using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guerreiro_Animacoes : MonoBehaviour
{
    public Animator animator;
    private string currentState;

 //   [SerializeField] GameObject paiParticulas;
  //  [SerializeField] Transform spawn;

  //  [SerializeField] GameObject sangueAtaque;
 //   [SerializeField] GameObject sangueHabilidade;

  //  [SerializeField] GameObject hitEffect;
 //   [SerializeField] GameObject hitTrail;

    // Strings das animações
    #region
    // Animações básicas
    public const string G_Ataque = "Guerreiro_Ataque";
    public string Ataque_Guerreiro { get { return G_Ataque; }}

    public const string G_Habilidade = "Guerreiro_Habilidade";
    public string Habilidade_Guerreiro { get { return G_Habilidade; } }

    public const string G_Idle = "Guerreiro_Idle";
    public string Idle_Guerreiro { get { return G_Idle; } }

    public const string G_Andar = "Guerreiro_Andar";
    public string Andar_Guerreiro { get { return G_Andar; } }
    #endregion

    [SerializeField] Inimigos guerreiro;
    [SerializeField] Teste_Player player;
    [SerializeField] Collider2D meuColisor;
    [SerializeField] List<Collider2D> colisores;
    public List<Collider2D> Colisores { get { return colisores; } set { colisores = value; }  }

    [SerializeField] GameObject particulas;
    [SerializeField] GameObject alvo;
    [SerializeField] GameObject paiParticulas;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        paiParticulas = GameObject.FindGameObjectWithTag("GerenciadorParticulas");
      //  spawn = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
      // if (Input.GetKeyDown(KeyCode.K))
        //    HitEffect();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
      //  print(newState);
        animator.Play(newState);

        currentState = newState;
    }

    void Habilidade() 
    {
        if (guerreiro != null)
        {
            if (guerreiro.podeHabilidade)
                guerreiro.podeHabilidade = false;
            else
                guerreiro.podeHabilidade = true;
        }
        if(player!=null)
        {
            if (player.podeHabilidade)
                player.podeHabilidade = false;
            else
                player.podeHabilidade = true;
        }
    }

    void Carregar() 
    {
        if (guerreiro != null)
        {
            GameObject go = Instantiate(particulas, new Vector3(alvo.transform.position.x, alvo.transform.position.y,
                            alvo.transform.position.z), Quaternion.identity);
            go.transform.SetParent(paiParticulas.transform);
            Destroy(go.gameObject, 0.5f);
        }
        if (player != null)
        {

            GameObject go = Instantiate(particulas, new Vector3(alvo.transform.position.x, alvo.transform.position.y,
                            alvo.transform.position.z), Quaternion.identity);
            go.transform.SetParent(paiParticulas.transform);
            go.GetComponent<ParticleSystem>().startColor = new Color(0, 170, 170);
            Destroy(go.gameObject, 0.5f);
        }
    }

    void CallSound(string sfxPath)
    {
        if (this.gameObject.GetComponentInParent<Teste_Player>())
        {
            sfxPath = "event:/Player/SFX_GuerreiroPossuido_Ataque";
        }
        AudioManager.instance.PlaySound(sfxPath, this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(meuColisor.IsTouching(collision) )
        {
            if (collision.CompareTag("Player")||collision.CompareTag("Inimigo"))
            {
                
               // colisores = new List<Collider2D>();
                //  colisores = collision;
                colisores.Add(collision);
            }
         //   print(collision.gameObject.name);
        }
    }
}
