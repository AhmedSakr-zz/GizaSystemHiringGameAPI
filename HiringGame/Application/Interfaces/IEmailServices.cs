using System.Threading.Tasks;
using HiringGame.Application.Dtos;

namespace HiringGame.Application.Interfaces
{
    public interface IEmailServices
    {
        Task<bool> SendMailAsync(SendMailParamsDto dto);
    }
}
