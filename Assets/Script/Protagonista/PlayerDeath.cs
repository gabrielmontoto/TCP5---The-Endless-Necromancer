using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    [SerializeField] GameObject portal;
    [SerializeField] GameObject playerAlma;
    [SerializeField] GameObject fogoD;
    [SerializeField] GameObject fogoE;

    [SerializeField] Material playerMaterial;
    [SerializeField] Material disintegrate;
    [SerializeField] float tempoDisintegrate;
    [SerializeField] bool podeDesintegrar;
    [SerializeField] bool spawnou;

    [SerializeField] float tempo;
    [SerializeField] float tempoSlow;
    [SerializeField] bool podeSlow;

    // Start is called before the first frame update
    void Start()
    {
        tempoDisintegrate = 1;
        playerMaterial = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        /*Time.timeScale += (1f / tempoSlow) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        if (podeSlow)
        {
            tempo += 1 * Time.fixedDeltaTime;

            if (tempo >= 1f)
            {
                tempo = 0;
                podeSlow = false;
            }
        }*/

        if (Input.GetKeyDown(KeyCode.I))
            PlayerMorreu();

        if (podeDesintegrar) 
        {
            tempoDisintegrate -= 0.4f * Time.deltaTime;

            if (tempoDisintegrate <= 0.5f) 
            {
                fogoD.SetActive(false);
                fogoE.SetActive(false);
            }

            if (tempoDisintegrate <= 0.2f)
                Portal();

            if (tempoDisintegrate <= 0f)
                podeDesintegrar = false;

            playerMaterial.SetFloat("_Progress", tempoDisintegrate);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SpriteRenderer>().material = playerMaterial;
        }
    }

    public void PlayerMorreu() 
    {
        playerMaterial = disintegrate;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Teste_Player>().podeReiniciar = true;
        podeDesintegrar = true;
    }

    void Portal() 
    {
        if (!spawnou) 
        {
            Instantiate(portal, new Vector3(this.transform.position.x - 10f, this.transform.position.y,
                this.transform.position.z), Quaternion.identity);

            Instantiate(playerAlma, new Vector3(this.transform.position.x, this.transform.position.y,
                            this.transform.position.z), Quaternion.identity);

            spawnou = true;
        }
    }
}
