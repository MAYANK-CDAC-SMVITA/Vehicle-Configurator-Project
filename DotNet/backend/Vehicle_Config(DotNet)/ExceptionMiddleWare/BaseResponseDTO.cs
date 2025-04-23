namespace Vehicle_Config_DotNet_.ExceptionMiddleware
{

        public class BaseResponseDTO<T>
        {
            public int ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
            public bool Succeed { get; set; }
            public T Data { get; set; }
        }
}
