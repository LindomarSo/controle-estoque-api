namespace CasaAzul.Api.Ultils
{
    public static class CPFFormat
    {
        public static string UnFormat(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }

        public static bool IsValidCpf(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (decimal.TryParse(cpf, out var result))
            {
                return cpf.Length == 11;
            }

            return false;
        }
    }
}
