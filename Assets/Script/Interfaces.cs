using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface ITomarDano
    {

        void ReceberDano(float valor);
    }

    public interface IAtaque
    {
       
        
    }
}

public enum TipoInimigo
{
    Gurreiro,
    Mago,
    Arqueiro,
    Vazio
}

public enum EstadosIaInimigo
{
    Patrulhando,
    Atacando,
    Parado,
    Ocupado,
    Especial,
    Morrendo,

}