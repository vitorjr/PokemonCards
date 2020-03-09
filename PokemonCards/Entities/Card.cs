using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonCards.Entities
{
    class Card
    {
        public string Name { get; set; }
        public string Numeracao { get; set; }
        public string Expansao { get; set; }
        public string Image { get; set; }

        public Card() { }

        public Card(string name, string numeracao, string expansao, string image)
        {
            Name = name;
            Numeracao = numeracao;
            Expansao = expansao;
            Image = image;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
