namespace Application.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; } // Данные ответа
        public bool IsSuccess { get; set; } // Флаг успешности выполнения операции
        public string Message { get; set; } // Сообщение об ошибке или дополнительной информации

        public ServiceResponse()
        {
            IsSuccess = true;
        }
    }
}
