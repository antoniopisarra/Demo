namespace Demo.Classi
{
    public static class ExtensionMethod
    {
        public static string ToCapitalize(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = input.ToLower();
            var words = input.Split(' ');

            for (var i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i][1..];
                }
            }

            return string.Join(" ", words);
        }
    }
}