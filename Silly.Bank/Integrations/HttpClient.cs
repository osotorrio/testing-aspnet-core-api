using Silly.Bank.Contracts;
using System;
using System.Collections.Generic;

namespace Silly.Bank.Integrations
{
    public class HttpClient : IHttpClient
    {
        public TEntity Get<TEntity>(string uri)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetList<TEntity>(string uri)
        {
            throw new NotImplementedException();
        }
    }
}
