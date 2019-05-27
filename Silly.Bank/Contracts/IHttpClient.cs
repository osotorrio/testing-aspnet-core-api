using System.Collections.Generic;

namespace Silly.Bank.Contracts
{
    public interface IHttpClient
    {
        TEntity Get<TEntity>(string uri);

        List<TEntity> GetList<TEntity>(string uri);
    }
}
