using HtmlAgilityPack;
using PokemonCards.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Web.Helpers;

namespace PokemonCards
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            /*
                Console.WriteLine("1 - Acesse o site https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/");
                Console.WriteLine("2 - Realize uma pesquisa sem preencher nenhum campo(Clicando em Search)");                
                Console.WriteLine("3 - Informe abaixo a quantidade de páginas:");
                Console.WriteLine("");
                int numero_paginas = int.Parse(Console.ReadLine());*/
            
            
        }

        static void Acessar_Links(int numero_paginas)
        {
            List<String> listas_cartas = new List<string>();
            Parallel.For(1, numero_paginas + 1, i => {
                string url = "https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/" + i + "?cardName=&cardText=&evolvesFrom=&simpleSubmit=&format=unlimited&hitPointsMin=0&hitPointsMax=340&retreatCostMin=0&retreatCostMax=5&totalAttackCostMin=0&totalAttackCostMax=5&particularArtist=";

                var Webget = new HtmlWeb();
                Webget.OverrideEncoding = Encoding.UTF8;
                var download_principal = Webget.Load(url);

                HtmlNodeCollection busca_main = download_principal.DocumentNode.SelectNodes("//div[@class='column-12 push-1 card-results-anchor']//li");

                Parallel.ForEach(busca_main, node => {
                    HtmlNode aux = node.SelectSingleNode("./a");
                    String url_cartas = ("https://www.pokemon.com" + aux.GetAttributeValue("href", "default"));
                    Console.WriteLine(url_cartas);
                    listas_cartas.Add(url_cartas);
                    // Acessar_CartasPokemon(url_cartas);                    
                });
            });
            Acessar_CartasPokemon(listas_cartas);
        }

        static void Acessar_CartasPokemon(List<String> tipocard)
        {
            var buffer = new BufferBlock<Card>();
            List<Card> cartas = new List<Card>();
            string nomeArquivo = @"C:\FitBank\Pokemon\arquivo-teste.json";
            Parallel.For(0, tipocard.Count, i => {
                var Webget = new HtmlWeb();
                Webget.OverrideEncoding = Encoding.UTF8;
                var download_cartas = Webget.Load(tipocard[i]);

                HtmlNodeCollection busca_dados = download_cartas.DocumentNode.SelectNodes("//section[@class=\"mosaic section card-detail\"]");


                Parallel.ForEach(busca_dados, dados => {
                    String imagem_temp = dados.SelectSingleNode(".//div[@class='column-6 push-1']//img").Attributes["src"].Value;
                    String recebe = GetImageBase64ByUrlEncode(imagem_temp);
                    var cartas_itens = new Card
                    {
                        Name = dados.SelectSingleNode(".//div[@class='card-description']//h1").InnerText,
                        Expansao = WebUtility.HtmlDecode(dados.SelectSingleNode(".//div[@class='pokemon-stats']//h3/a").InnerText),
                        Numeracao = WebUtility.HtmlDecode(dados.SelectSingleNode(".//div[@class='pokemon-stats']//span").InnerText),
                        //Url_imagem = Convert.ToBase64String(new HttpClient().GetByteArrayAsync(dados.SelectSingleNode(".//div[@class='column-6 push-1']//img").Attributes["src"].Value).Result)
                        Image = recebe
                    };
                    /*String nome = dados.SelectSingleNode(".//div[@class='card-description']//h1").InnerText;
                    String numeracao = WebUtility.HtmlDecode(dados.SelectSingleNode(".//div[@class='pokemon-stats']//h3/a").InnerText);
                    String expansao = WebUtility.HtmlDecode(dados.SelectSingleNode(".//div[@class='pokemon-stats']//span").InnerText);
                    String base64 = recebe;
                    buffer.Post(new Card(nome,numeracao,expansao,base64));*/
                    cartas.Add(cartas_itens);
                });
            });
            //buffer.Complete();
            //var consume = ConsumeAsync(buffer);
            using (StreamWriter file = File.CreateText(Links.GetPath("single_file_pokemons.json")))
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(cartas);
                file.WriteLine(json);
            }

        }

        public static string GetImageBase64ByUrlEncode(string url)
        {
            WebClient webClientencode = new WebClient();
            return Convert.ToBase64String(webClientencode.DownloadData(url));
        }
    }
}
