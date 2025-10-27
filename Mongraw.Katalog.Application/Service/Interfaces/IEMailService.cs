using Mongraw.Katalog.Domain.Models;

namespace Mongraw.Katalog.Application.Service.Interfaces
{
    public interface IEMailService
    {
        void SendEmail(Email email);
    }
}