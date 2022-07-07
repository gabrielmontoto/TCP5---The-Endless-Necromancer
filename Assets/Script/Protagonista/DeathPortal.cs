using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPortal : MonoBehaviour
{

    [SerializeField] Material material;

    [SerializeField] float tempo;
    [SerializeField] bool apareceu;
    [SerializeField] public bool sumir;
    [SerializeField] bool podeReiniciar;

    [SerializeField] Teste_Player player;
    // Start is called before the first frame update
    void Start()
    {
        material = this.GetComponent<SpriteRenderer>().material;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Teste_Player>();

        tempo = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (!apareceu)
        {
            tempo -= 2.8f * Time.deltaTime;

            if (tempo <= 1.2f)
            {
                tempo = 1.2f;

                if (player.renasceu == false) 
                {
                    player.Renascer();
                    sumir = true;
                    player.renasceu = true;
                }

                apareceu = true;
            }

            material.SetFloat("_DissolveAmount", tempo);
        }
        
        if (sumir) 
        {
            tempo += 3f * Time.deltaTime;

            if (tempo >= 4)
            {
                if (player.podeReiniciar)
                    GameManager.instance.reiniciar();
                else
                    Destroy(this.gameObject);
            }

            material.SetFloat("_DissolveAmount", tempo);
        }
    }
}
