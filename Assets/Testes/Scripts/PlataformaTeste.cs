using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaTeste : MonoBehaviour
{
    PlatformEffector2D effector;
    [SerializeField]float tempo;
    [SerializeField] bool playerEnostou;
    [SerializeField] Teste_Player player;
   // [SerializeField] Collision2D player;
   // Collider2D collider2D;
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
       // collider2D = this.GetComponent<Collider2D>();
        //player = GameObject.FindGameObjectWithTag("Pe");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Input.GetKeyUp(KeyCode.S) && player.UtilizandoMouse|| player.Move.y >= 0) //ver com botao de baixo do controle
            {
                tempo = 0.5f;
                effector.rotationalOffset = 0;
                playerEnostou = false;
            }
            if (Input.GetKey(KeyCode.S) && playerEnostou && player.UtilizandoMouse || player.Move.y < 0 && playerEnostou)
            {

                if (tempo <= 0)
                {
                    effector.rotationalOffset = 180f;
                    tempo -= Time.deltaTime;
                    //   if (tempo <= -0.5f)
                    {

                        //  playerEnostou = false;
                        tempo = 0.5f;
                    }
                }
                else
                {
                    tempo -= Time.deltaTime;
                }

            }
            else
            {

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
         ///   playerEnostou = true;
            player = collision.transform.GetComponent<Teste_Player>();
            // tempo = 0.5f;
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerEnostou = true;
           // player = collision.transform.GetComponent<Teste_Player>();
           // tempo = 0.5f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerEnostou = false;
            
        }
    }

}
