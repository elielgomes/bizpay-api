namespace bizpay_api.Services
{
    public static class GetBrasiliaTime
    {
        public static DateTime Time()
        {
            // Obtém a TimeZoneInfo para o fuso horário de Brasília
            TimeZoneInfo brTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            // Obtém a hora atual no fuso horário de Brasília
            DateTime currentDateTimeInBrasilia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brTimeZone);

            return currentDateTimeInBrasilia;
        }
    }
}
