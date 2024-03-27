namespace DatingAppAPI.Extenstions
{
    public static class DateTimeExtensions
    {
        // Specify what it is that we extending 'this DateTime dob'
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
