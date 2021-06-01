namespace NotasSSA
{
    public static class String_Handler
    {
        public static string Remove1stWord(string s, string separator = " ")
        {
            string[] word = s.Split(separator);
            word[0] = null;
            return string.Join(separator, word);
        }

        public static string RemoveLastWord(string s, string separator = " ")
        {
            string[] word = s.Split(separator);
            word[^1] = null;
            return string.Join(separator, word);
        }

        public static string GetLastWord(string s, string separator = " ")
        {
            string[] word = s.Split(separator);
            return word[^1];
        }

        public static bool Verify1stLetter(string s, char l)
        {
            var array = s.ToCharArray();
            return l == array[0];
        }
    }
}
