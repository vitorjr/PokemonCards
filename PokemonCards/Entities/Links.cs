﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonCards.Entities
{
    class Links
    {
        public static string GetLink(int page = 0, string link = "")
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"https://www.pokemon.com");

            if (page > 0)
            {
                sb.Append(@"/us/pokemon-tcg/pokemon-cards/").Append(page)
                .Append(@"?cardName=&cardText=&evolvesFrom=&simpleSubmit=&format=unlimited&hitPointsMin=0&hitPointsMax=340&retreatCostMin=0&retreatCostMax=5&totalAttackCostMin=0&totalAttackCostMax=5&particularArtist=");
            }
            else
            {
                sb.Append(link);
            }

            return sb.ToString();
        }
        public static string GetPath(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"C:\FitBank\Pokemon\")
                .Append(fileName);

            return sb.ToString();
        }
    }
}
