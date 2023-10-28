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

        public static string FormatCPFCNPJ(string str)
        {
            if (str.Length == 11)
                return Convert.ToUInt64(str).ToString(@"000\.000\.000\-00");
            else
                return Convert.ToUInt64(str).ToString(@"00\.000\.000\/0000\-00");                
        }
        
        public static string FormatTelefone(string str)
        {
            if (str.Length == 11)
                return Convert.ToUInt64(str).ToString(@"(00) 00000-0000");
            else
                return Convert.ToUInt64(str).ToString(@"(00) 0000-0000");
        }
    }
}
