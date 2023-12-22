
namespace Test.Backend.Application.Features.Products.Commands.CreateProduct
{
    using MediatR;
    using Test.Backend.Application.Behaviours.Response;
    public class CreateProductCommand : IRequest<ApiResponse>
    {
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
    }
}
