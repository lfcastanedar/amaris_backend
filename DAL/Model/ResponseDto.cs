namespace DAL.Model;

public class ResponseDto<TResponse>
{
    public string Status { get; set; }
    public TResponse? Data { get; set; }
}