using HonsBackendAPI.Models;

namespace HonsBackendAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Customer customer);
    }
}
