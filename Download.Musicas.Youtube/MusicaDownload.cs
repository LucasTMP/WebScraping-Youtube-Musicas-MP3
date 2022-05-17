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
    internal class MusicaDownload
    {
        private static readonly string _diretorioRaiz = @"C:\Users\Lucas Paes\Desktop\Musicas\Download\";
        private static readonly string _diretorioFunk = @"Funk\";
        private static readonly string _diretorioPagode = @"Pagode\";
        private static readonly string _diretorioForro = @"Forro\";
        private static readonly string _diretorioSertanejo = @"Sertanejo\";
        private static readonly string _diretorioPadrao = @"Manual\";
        private static int numeroMusica = 0;

        static void BaixarConveterMusicaMP3(string link, string diretorio)
        {

            TextoConsole($" MUSICA NUMERO: {++numeroMusica} ");

            var youTube = YouTube.Default;
            string arquivo = "";
            YouTubeVideo video = null;

            try
            {
                video = youTube.GetVideo(link);
                arquivo = diretorio + video.FullName;
                Console.WriteLine($"DOWNLOAD MUSICA: {video.Title} .....");
                File.WriteAllBytes(arquivo, video.GetBytes());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Video da musica no youtube não encontrado: {link} .....");
                Console.WriteLine($"Mensagem de erro: {ex.Message}");
                Console.ResetColor();
                return;
            }

            var inputFile = new MediaFile { Filename = arquivo };
            var outputFile = new MediaFile { Filename = $"{diretorio + video.Title}.mp3" };

            try
            {
                ConverterMusicaMP4ToMP3(video, inputFile, outputFile);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro na conversão do video de MP4 para MP3: {video.Title} .....");
                Console.WriteLine($"Mensagem de erro: {ex.Message}");
                Console.ResetColor();
                return;
            }

            try
            {
                DeletarVideoMP4DoDiretorio(video, diretorio);
            }
            catch (IOException ioExp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Não foi possivel deletar o video MP4 da musica {video.Title} no diretorio {diretorio}");
                Console.WriteLine($"Mensagem de erro: {ioExp.Message}");
                Console.ResetColor();
                return;
            }

        }

        public static void BaixarForro(ScrapingMusica scrappingMusica, int quantidadeMusicas)
        {
            TextoConsole(" FORRO ");

            var diretorioMusicas = _diretorioRaiz + _diretorioForro;
            var url = "https://maistocadas.mus.br/forro/";
            BaixarMusicas(scrappingMusica, diretorioMusicas, url, quantidadeMusicas);
        }

        public static void BaixarFunk(ScrapingMusica scrappingMusica, int quantidadeMusicas)
        {
            TextoConsole(" FUNK ");

            var diretorioMusicas = _diretorioRaiz + _diretorioFunk;
            var url = "https://maistocadas.mus.br/musicas-funks/";
            BaixarMusicas(scrappingMusica, diretorioMusicas, url, quantidadeMusicas);
        }

        static void BaixarListaMusicasYoutube(IEnumerable<string> listaUrlsMusicasYoutube, string diretorio)
        {
            foreach (var urlMusicaYoutube in listaUrlsMusicasYoutube)
            {
                BaixarConveterMusicaMP3(urlMusicaYoutube, diretorio);
            }
        }

        private static void BaixarMusicas(ScrapingMusica scrappingMusica, string diretorioMusicas, string url, int quantidadeMusicas)
        {
            string htmlDoc;
            htmlDoc = scrappingMusica.AcessarPagina(url);
            var listaUrlMusicasYoutube = scrappingMusica.ColetaListaUrlsMusicasYoutube(htmlDoc);
            BaixarListaMusicasYoutube(listaUrlMusicasYoutube.Take(quantidadeMusicas), diretorioMusicas);
        }

        public static void BaixarMusicasManual(IEnumerable<string> listaMusicasUrlManual)
        {

            var diretorioMusicas = _diretorioRaiz + _diretorioPadrao;
            if (listaMusicasUrlManual.Any())
                BaixarListaMusicasYoutube(listaMusicasUrlManual, diretorioMusicas);
        }

        public static void BaixarPagode(ScrapingMusica scrappingMusica, int quantidadeMusicas)
        {
            TextoConsole(" PAGODE ");

            var diretorioMusicas = _diretorioRaiz + _diretorioPagode;
            var url = "https://maistocadas.mus.br/pagode-samba/";
            BaixarMusicas(scrappingMusica, diretorioMusicas, url, quantidadeMusicas);
        }

        public static void BaixarSertanejo(ScrapingMusica scrappingMusica, int quantidadeMusicas)
        {
            TextoConsole(" SERTANEJO ");

            var diretorioMusicas = _diretorioRaiz + _diretorioSertanejo;
            var url = "https://maistocadas.mus.br/musicas-sertanejas/";
            BaixarMusicas(scrappingMusica, diretorioMusicas, url, quantidadeMusicas);
        }

        static void ConverterMusicaMP4ToMP3(YouTubeVideo video, MediaFile arquivoEntrada, MediaFile arquivoSaida)
        {

            using (var engine = new Engine())
            {
                Console.WriteLine($"CONVERTENDO MP3: {video.Title} .....");
                engine.GetMetadata(arquivoEntrada);
                var options = new ConversionOptions() { AudioSampleRate = AudioSampleRate.Default };
                engine.Convert(arquivoEntrada, arquivoSaida, options);
                Console.WriteLine("MUSICA SALVA COM SUCESSO");
            }

        }

        static void DeletarVideoMP4DoDiretorio(YouTubeVideo video, string diretorio)
        {
            if (File.Exists(Path.Combine(diretorio, video.FullName)))
            {
                File.Delete(Path.Combine(diretorio, video.FullName));
            }
        }

        private static void TextoConsole(string texto)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(new string('/', 44));
            Console.WriteLine(texto.PadBoth(44, '/'));
            Console.WriteLine(new string('/', 44));
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}