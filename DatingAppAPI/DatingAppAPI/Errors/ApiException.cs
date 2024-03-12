namespace DatingAppAPI.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string massage, string details)
        {
            StatusCode = statusCode;
            Massage = massage;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string  Massage { get; set; }
        public string Details { get; set; }
    }
}
