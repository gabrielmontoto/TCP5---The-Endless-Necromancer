using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoreGuerreiro : MonoBehaviour
{

    public Sprite[] gore;
    [SerializeField] GameObject[] objectGore;
    [SerializeField] int qntGore;
    [SerializeField] int goreSelecionado;
    [SerializeField] float pos_1, pos_2;
    [SerializeField] bool podeGore;

    // Start is called before the first frame update
    void Start()
    {
        //objectGore = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarGore()
    {
        if (!podeGore)
        {
            for (int i = 0; i < qntGore; i++)
            {
                var go = Instantiate(objectGore[i], new Vector3(this.transform.position.x + Random.Range(pos_1, pos_2),
                    this.transform.position.y + Random.Range(pos_1, pos_2), this.transform.position.z), Quaternion.identity);
                goreSelecionado = Random.Range(1, 10);
                go.GetComponent<SpriteRenderer>().sprite = gore[goreSelecionado];
            }
            podeGore = true;
        }
        
    }
}
