using DAL.Model;

namespace DAL.External.Interfaces;

public interface IExternalRequestDal
{
    public Task<ResponseDto<TResponse>> GetRequest<TResponse>(string url, string apiEndpoint);
}