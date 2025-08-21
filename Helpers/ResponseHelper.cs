namespace CarKilometerTrack.Helpers
{
    public class ResponseHelper<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }

        public int StatusCode { get; set; }

        public static ResponseHelper<T> Ok(T data ,string message="İşlem Başarılı",int statusCode=200)
        {
            return new ResponseHelper<T> 
            {
             IsSuccess = true,
             Data = data,
             StatusCode = statusCode, 
             Message = message 
            };
        }

        public static ResponseHelper<T> Fail(string message = "İşlem Başarısız", int statusCode = 400)
        {
            return new ResponseHelper<T>
            {
                IsSuccess = false,
                Data = default,
                StatusCode = statusCode,
                Message = message
            };
        }
    }
}
