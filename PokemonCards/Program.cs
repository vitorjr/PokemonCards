using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonCards
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try{
                Console.WriteLine("1 - Acesse o site https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/");
                Console.WriteLine("2 - Realize uma pesquisa sem preencher nenhum campo(Clicando em Search)");                
                Console.WriteLine("3 - Informe abaixo a quantidade de páginas:");
                Console.WriteLine("");
                int numero_paginas = int.Parse(Console.ReadLine());
            }
            catch (Exception f){
                Console.WriteLine(f.Message);
            }
            
        }
    }
}
