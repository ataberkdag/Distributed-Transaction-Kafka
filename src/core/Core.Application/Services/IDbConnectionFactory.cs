using System.Data;

namespace Core.Application.Services
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetOpenConnection();

        string GetDbName();
    }
}
