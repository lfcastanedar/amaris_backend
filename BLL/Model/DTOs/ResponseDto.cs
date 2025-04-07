namespace BLL.Model.DTOs;

public class ResponseDto
{
    public string Message { get; set; } = null!;
    public object? Result { get; set; }
}

public class ResponseDTO<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public T? Result { get; set; }
}