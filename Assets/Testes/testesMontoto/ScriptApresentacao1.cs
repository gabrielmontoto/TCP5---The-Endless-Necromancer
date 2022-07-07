using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptApresentacao1 : MonoBehaviour
{
    [SerializeField] List<GameObject> Objetos1Ativados, Objetos2Ativados, Objetos3Ativados, Objetos4Ativados, Objetos5Ativados, Objetos6Ativados;
    [SerializeField] bool Ativarobjetos1, Ativarobjetos2, Ativarobjetos3, Ativarobjetos4, Ativarobjetos5, Ativarobjetos6;

    public GameObject luzGlobal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Ativarobjetos1)
        {
            luzGlobal.SetActive(false);

            for (int i = 0; i < Objetos1Ativados.Count; i++)
            {
                Objetos1Ativados[i].SetActive(true);
            }
        }
        if (Ativarobjetos2)
        {
            for (int i = 0; i < Objetos2Ativados.Count; i++)
            {
                Objetos2Ativados[i].SetActive(true);
            }
        }
        if (Ativarobjetos3)
        {
            for (int i = 0; i < Objetos3Ativados.Count; i++)
            {
                Objetos3Ativados[i].SetActive(true);
            }
        }
        if (Ativarobjetos4)
        {
            for (int i = 0; i < Objetos4Ativados.Count; i++)
            {
                Objetos4Ativados[i].SetActive(true);
            }
        }

        // Desativar

        if (!Ativarobjetos1)
        {
            luzGlobal.SetActive(true);

            for (int i = 0; i < Objetos1Ativados.Count; i++)
            {
                Objetos1Ativados[i].SetActive(false);
            }
        }
        if (!Ativarobjetos2)
        {
            for (int i = 0; i < Objetos2Ativados.Count; i++)
            {
                Objetos2Ativados[i].SetActive(false);
            }
        }
        if (!Ativarobjetos3)
        {
            for (int i = 0; i < Objetos3Ativados.Count; i++)
            {
                Objetos3Ativados[i].SetActive(false);
            }
        }
        if (!Ativarobjetos4)
        {
            for (int i = 0; i < Objetos4Ativados.Count; i++)
            {
                Objetos4Ativados[i].SetActive(false);
            }
        }

        /*if (Ativarobjetos5)
        {
            for (int i = 0; i < Objetos5Ativados.Count; i++)
            {
                Objetos5Ativados[i].SetActive(true);
            }
        }
        if (Ativarobjetos6)
        {
            for (int i = 0; i < Objetos6Ativados.Count; i++)
            {
                Objetos6Ativados[i].SetActive(true);
            }
        }*/
    }
}
