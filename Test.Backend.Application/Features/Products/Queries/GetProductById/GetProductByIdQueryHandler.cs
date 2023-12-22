namespace Test.Backend.Application.Features.Products.Queries.GetProductById
{
    using AutoMapper;
    using LazyCache;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Test.Backend.Application.Behaviours.Response;
    using Test.Backend.Application.Contracts.Persistence;
    using Test.Backend.Application.Features.Products.Dtos;
    using Test.Backend.Domain.ExternalApi;
    using Newtonsoft.Json;
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ApiResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IExternalApi _externalApi;
        private readonly IAppCache _appCache;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;

        public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper, IExternalApi externalApi, IAppCache appCache, ILogger<GetProductByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _externalApi = externalApi;
            _appCache = appCache;
            _logger = logger;
        }

        public async Task<ApiResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse();
            try
            {
                _logger.LogInformation($"Initial Task GetProductById");
                AddLazyCache();

                KeyValuePair<string, string> resultStateCache= new KeyValuePair<string, string>();

                _logger.LogInformation($"Consume GetProductById ({request.ProductId})");
                var productEntity = await _repository.GetByIdAsync(request.ProductId);
                if(productEntity is null)
                {
                    _logger.LogInformation($"Product does not exists");
                    return apiResponse.CreateNotFoundResponse(request.ProductId, "Product not exists");
                }

                var productDto = _mapper.Map<ProductDtoQuery>(productEntity);
                _logger.LogInformation($"Result GetProductById => ({JsonConvert.SerializeObject(productDto)})");

                var diccionary = _appCache.GetAsync<Dictionary<string, string>>("StateProduct");
                _logger.LogInformation($"Get Cache diccionary => ({JsonConvert.SerializeObject(diccionary)})");
                if (diccionary.Result != null)
                {
                    resultStateCache = diccionary.Result.Where(x => x.Key == (productEntity.Status == true ? "1" : "0")).FirstOrDefault();
                    productDto.StateName = resultStateCache.Value.ToString();
                }

                List<DiscountProduct> responseDataApi = await _externalApi.GetDataFromMockapi();
                _logger.LogInformation($"Get Object Api => ({JsonConvert.SerializeObject(responseDataApi)})");
                if (responseDataApi.Count > 0)
                {
                    DiscountProduct discountProduct = responseDataApi.Where(x => x.Id == productDto.ProductId).FirstOrDefault();
                    productDto.Discount = discountProduct is null ? 0 : discountProduct.Discount;
                }
                
                productDto.FinalPrice = productEntity.Price * (100 - productDto.Discount) / 100;
                _logger.LogInformation($"Get Object return services => ({JsonConvert.SerializeObject(productDto)})");
                return apiResponse.GetResponse(productDto, "Success");

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving the Product => ({ex.Message})");
                return apiResponse.CreateInternalServerErrorResponse($"An error occurred while retrieving the Product.", ex.Message);
            }
        }

        private void AddLazyCache()
        {

            var diccionary = _appCache.GetAsync<Dictionary<string, string>>("StateProduct");
            if (diccionary.Result is null)
            {
                Dictionary<string, string> diccionarioString = new Dictionary<string, string>();
                diccionarioString.Add("1", "Active");
                diccionarioString.Add("0", "Inactive");

                _logger.LogInformation($"Save Cache with diccionary => ({JsonConvert.SerializeObject(diccionarioString)})");
                Func<Dictionary<string, string>> funcDictionary = () => diccionarioString;
                var data = _appCache.GetOrAdd<Dictionary<string, string>>("StateProduct", funcDictionary, DateTimeOffset.Now.AddMinutes(5));
            }
            
        }
    }
}
