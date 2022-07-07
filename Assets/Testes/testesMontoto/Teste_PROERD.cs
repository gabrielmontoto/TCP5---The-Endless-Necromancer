using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste_PROERD : MonoBehaviour
{
    [SerializeField] int inimigosPossuidos;
    // Start is called before the first frame update
    private void OnEnable()
    {
        TesteScript_Possessao.ContagemInimigosPossuidos += Contagem;
    }

    private void Contagem(int obj)
    {
        inimigosPossuidos += obj;
    }
    private void OnDisable()
    {
        TesteScript_Possessao.ContagemInimigosPossuidos -= Contagem;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
