namespace Medi.Server.Models.DTOs
{
    public class ServiceResult<T> where T : class
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public ErrorType ErrorType { get; set; }

        public T Data { get; set; }

        public ServiceResult()
        {
            Success = true;
            ErrorType = ErrorType.None;
        }
    }

    public enum ErrorType
    {
        None,
        Authentication, 
        Validation, 
        Duplicate,
        Database
    }
}