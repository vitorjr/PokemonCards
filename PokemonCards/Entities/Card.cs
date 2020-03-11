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
            StringBuilder x = new StringBuilder();

            x.AppendLine($"- Nome: {Name}")
                .AppendLine($"- Modelo: {Numeracao}")
                .AppendLine($"- Tipo de Carta: {Expansao}")
                .AppendLine($"- Imagem Base64: {Image}")
                .AppendLine();

            return x.ToString();
        }
    }
}
