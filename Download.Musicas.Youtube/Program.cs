using Download.Musicas.Youtube.Extensions;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoLibrary;

namespace Download.Musicas.Youtube
{
    internal class Program : MusicaDownload
    {


        static void Main(string[] args)
        {
            var scrappingMusica = new ScrapingMusica();

            Console.WriteLine($"////////////////////////////////////////////");
            Console.WriteLine($"////// INICIANDO DOWNLOAD DE MUSICAS ///////");
            Console.WriteLine($"////////////////////////////////////////////");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            // Adicionar a url das suas musicas manualmente
            // basta adicionar como os exemplos abaixo:
            //listaMusicasUrl.Add("https://www.youtube.com/watch?v=XUbnhFO0bUc");
            //listaMusicasUrl.Add("https://www.youtube.com/watch?v=ktb8lq7Ue24");
            var listaMusicasUrlManual = new List<string>();
            
            BaixarMusicasManual(listaMusicasUrlManual);
            BaixarSertanejo(scrappingMusica, 40);
            BaixarFunk(scrappingMusica, 30);
            BaixarForro(scrappingMusica, 20);
            BaixarPagode(scrappingMusica, 20);

        }
    }
}
