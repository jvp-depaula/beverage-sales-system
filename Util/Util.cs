namespace Sistema.Util
{
    public class Util
    {
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }       

        public static string Unmask(string str)
        {
            return str.Replace(".", "").Replace("-", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace(" ", "");
        }
    }
}
