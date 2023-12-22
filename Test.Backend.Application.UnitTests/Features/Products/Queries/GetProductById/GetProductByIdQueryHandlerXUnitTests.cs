

namespace Test.Backend.Application.UnitTests.Features.Products.Queries.GetProductById
{
    using AutoMapper;
    using LazyCache;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Shouldly;
    using System.Net;
    using Test.Backend.Application.Behaviours.Response;
    using Test.Backend.Application.Contracts.Persistence;
    using Test.Backend.Application.Features.Products.Queries.GetProductById;
    using Test.Backend.Application.Mappings;
    using Test.Backend.Application.UnitTests.Mocks;
    using Xunit;

    public class GetProductByIdQueryHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _repository;
        private readonly Mock<IExternalApi> _externalApi;
        private readonly Mock<IAppCache> _appCache;
        private readonly Mock<ILogger<GetProductByIdQueryHandler>> _logger;

        public GetProductByIdQueryHandlerXUnitTests()
        {
            _repository = MockProductRepository.GetProductRepositoryInterface();
            var mapperConfig = new MapperConfiguration(x => 
                x.AddProfile<MappingProfile>()
            );
            _mapper = mapperConfig.CreateMapper();
            _externalApi = new Mock<IExternalApi>();
            _appCache = new Mock<IAppCache>();
            _logger = new Mock<ILogger<GetProductByIdQueryHandler>>();
        }

        [Fact]
        public async Task GetProductById_WhenProductId_NotExits()
        {
            var requestProduct = new GetProductByIdQuery(1);

            var productHandler = new GetProductByIdQueryHandler(_repository.Object,_mapper, _externalApi.Object, _appCache.Object, _logger.Object);

            //act
            var result = await productHandler.Handle(requestProduct, CancellationToken.None);

            //assert
            result.ShouldBeOfType<ApiResponse>();
            result.Status.ShouldBeFalse();
            result.ResponseCode.Equals(HttpStatusCode.NoContent);
        }
    }
}
