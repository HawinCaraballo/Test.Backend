namespace Test.Backend.Application.Features.Products.Commands.CreateProduct
{
    using AutoMapper;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Test.Backend.Application.Behaviours.Response;
    using Test.Backend.Application.Contracts.Persistence;
    using Test.Backend.Application.Features.Products.Dtos;
    using Test.Backend.Domain;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidator<CreateProductCommand>> _validators;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper, IEnumerable<IValidator<CreateProductCommand>> validators, ILogger<CreateProductCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _validators = validators;
            _logger = logger;
        }

        public async Task<ApiResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse();
            try
            {
                var validationResponse = apiResponse.ValidateCommand(request, _validators);
                if (validationResponse != null)
                {
                    _logger.LogInformation($"Error validation");
                    return validationResponse;
                }
                _logger.LogInformation($"Validate success");

                var productEntity = _mapper.Map<Products>(request);
                var resultEntity = await _repository.AddAsync(productEntity);
                if (resultEntity != null)
                {
                    _logger.LogInformation($"Get Object return services => ({JsonConvert.SerializeObject(productEntity)})");
                    return apiResponse.GetResponse(_mapper.Map<ProductDto>(productEntity), "Success");
                }
                else
                {
                    _logger.LogInformation($"Product not Not Found");
                    return apiResponse.CreateNotFoundResponse(0, "Product not Not Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving the Product => ({ex.Message})");
                return apiResponse.CreateInternalServerErrorResponse($"An error occurred while retrieving the Product.", ex.Message);
            }
        }
    }
}
