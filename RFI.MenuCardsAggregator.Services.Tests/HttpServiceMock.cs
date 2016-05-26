using System;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Services.Tests
{
    class HttpServiceMock : IHttpService
    {
        private readonly Func<string> _func;

        public HttpServiceMock(Func<string> getDataFunc)
        {
            _func = getDataFunc;
        }

        #region Implementation of IHttpService

        public Task<string> GetAsync(string uri)
        {
            return Task.Factory.StartNew(() => _func.Invoke());
        }

        #endregion
    }
}
