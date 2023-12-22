
namespace Test.Backend.Application.Features.Products.Commands.UpdateProduct
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

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidator<UpdateProductCommand>> _validators;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper, IEnumerable<IValidator<UpdateProductCommand>> validators, ILogger<UpdateProductCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _validators = validators;
            _logger = logger;
        }

        public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

                var productEntity = await _repository.GetByIdAsync(request.ProductId);
                if (productEntity is null)
                {
                    _logger.LogInformation($"Product does not exist");
                    return apiResponse.CreateNotFoundResponse(0, "Product does not exist");
                }
                productEntity.Name = request.Name ?? productEntity.Name;
                productEntity.Description = request.Description ?? productEntity.Description;
                productEntity.Stock = request.Stock ?? productEntity.Stock;
                productEntity.Price = request.Price ?? productEntity.Price;

                var resultEntity = await _repository.UpdateAsync(productEntity);
                if (resultEntity is null)
                {
                    _logger.LogInformation($"Product does not found");
                    return apiResponse.CreateNotFoundResponse(0, "Product does not found");
                }
                else
                {
                    _logger.LogInformation($"Get Object return services => ({JsonConvert.SerializeObject(productEntity)})");
                    return apiResponse.GetResponse(_mapper.Map<ProductDto>(productEntity), "Success");
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
