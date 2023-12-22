
namespace Test.Backend.Application.Features.Products.Queries.GetProductById
{
    using MediatR;
    using Test.Backend.Application.Behaviours.Response;

    public class GetProductByIdQuery : IRequest<ApiResponse>
    {
        public int ProductId { get; set; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
