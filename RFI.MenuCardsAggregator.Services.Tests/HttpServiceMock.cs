using System;
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

        public string Get(string uri)
        {
            return _func.Invoke();
        }

        #endregion
    }
}
