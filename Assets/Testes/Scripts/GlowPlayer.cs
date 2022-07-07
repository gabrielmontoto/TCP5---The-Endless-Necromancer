using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowPlayer : MonoBehaviour
{

    public GameObject player;
    public GameObject elmo;
    public GameObject brilho;
    public Material original;
    public Material glow;
    public GameObject chama;
    public GameObject espada;
    public GameObject brilhoE;
    [SerializeField] bool brilacarai;

    //public GameObject protagonista;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        #region Comentei para colocar as mudanças pra apresentação
        //if (Input.GetKeyDown(KeyCode.R)) 
        //{

        //if (elmo.active == false)
        //{
        //    player.GetComponent<SpriteRenderer>().material = glow;
        //    elmo.SetActive(true);
        //    brilho.SetActive(true);
        //    chama.SetActive(true);
        //    espada.SetActive(true);
        //    brilhoE.SetActive(true);
        //    protagonista.SetActive(false);
        //}
        //else if (elmo.active == true)
        //{
        //    player.GetComponent<SpriteRenderer>().material = original;
        //    elmo.SetActive(false);
        //    brilho.SetActive(false);
        //    chama.SetActive(false);
        //    espada.SetActive(false);
        //    brilhoE.SetActive(false);
        //    protagonista.SetActive(true);
        //}

        //}
        #endregion

    /*    if (brilacarai == true)
        {
            player.GetComponent<SpriteRenderer>().material = glow;
            elmo.SetActive(true);
            brilho.SetActive(true);
            chama.SetActive(true);
            espada.SetActive(true);
            brilhoE.SetActive(true);
            //protagonista.SetActive(false);
        }
        else
        {
            player.GetComponent<SpriteRenderer>().material = original;
            elmo.SetActive(false);
            brilho.SetActive(false);
            chama.SetActive(false);
            espada.SetActive(false);
            brilhoE.SetActive(false);
            //protagonista.SetActive(true);
        }*/
    }

    public void BrilhaPorra(bool docarai)
    {
        this.brilacarai = docarai;
    }

}
