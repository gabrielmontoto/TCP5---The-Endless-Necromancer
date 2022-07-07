using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapeamentoPosicaoMapas : MonoBehaviour
{

   [SerializeField] GameObject[] salas;
    public GameObject[] Salas { get { return salas; }  }
    bool salasmapeadas = false;
   // public bool SalasMapeadas { get { return salasmapeadas; } }
    // Start is called before the first frame update

    public void MapearSalas()
    {
        for (int i = 0; i < 25; i++)
        {
            salas[i] =this.transform.GetChild(i).gameObject;
           
        }

       // GameManager.instance.AleatorizarObjetosNasSalas();
    }



    // Update is called once per frame
    void Update()
    {
           if(transform.childCount==25 && salasmapeadas==false)
            {
                MapearSalas();
                salasmapeadas = true;
            }
        
    }
}
