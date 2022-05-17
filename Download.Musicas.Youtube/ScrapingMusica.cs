using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Download.Musicas.Youtube
{
    internal class ScrapingMusica
    {

        private readonly HttpClient _clientHtml;
        private readonly string _youtubeUrl;

        public ScrapingMusica()
        {
            _clientHtml = new HttpClient();
            _youtubeUrl = "https://www.youtube.com/watch?v=";
        }

        public string AcessarPagina(string url)
        {
            return  _clientHtml.GetStringAsync(url).Result;
        }

        public List<string> ColetaListaUrlsMusicasYoutube(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var idsMusicasYoutube = htmlDoc.DocumentNode.SelectNodes("//div[@id='tracklist']/ol/li");
            var listaIDsMusicasYoutube = new List<string>();

            foreach (var musicaIDYoutube in idsMusicasYoutube)
            {
                var IdYoutube = musicaIDYoutube.Id;
                listaIDsMusicasYoutube.Add($"{_youtubeUrl + IdYoutube}");
            }

            return listaIDsMusicasYoutube;
        }

    }
}
