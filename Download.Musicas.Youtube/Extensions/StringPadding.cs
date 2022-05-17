namespace Download.Musicas.Youtube.Extensions
{
    internal static class StringPadding
    {
        public static string PadBoth(this string str, int length, char character = ' ') => str.PadLeft((length - str.Length) / 2 + str.Length, character).PadRight(length, character);

    }
}
