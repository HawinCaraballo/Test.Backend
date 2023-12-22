using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Backend.Domain.ExternalApi;

namespace Test.Backend.Application.Contracts.Persistence
{
    public interface IExternalApi
    {
        Task<List<DiscountProduct>> GetDataFromMockapi();
    }
}
