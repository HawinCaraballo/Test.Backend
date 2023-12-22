

namespace Test.Backend.API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using Test.Backend.Application.Behaviours.Response;
    using Test.Backend.Application.Features.Products.Commands.CreateProduct;
    using Test.Backend.Application.Features.Products.Commands.UpdateProduct;
    using Test.Backend.Application.Features.Products.Dtos;
    using Test.Backend.Application.Features.Products.Queries.GetProductById;
    using Test.Backend.Application.Models.Responses;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ValidationErrorResponse>>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetProductById(int Id)
        {
            var query = new GetProductByIdQuery(Id);
            var apiResponse = await _mediator.Send(query);
            return apiResponse.ResponseCode switch
            {
                (int)HttpStatusCode.OK => Ok(apiResponse),
                (int)HttpStatusCode.NoContent => NoContent(),
                (int)HttpStatusCode.BadRequest => BadRequest(apiResponse),
                (int)HttpStatusCode.InternalServerError => StatusCode((int)HttpStatusCode.InternalServerError, apiResponse),
                _ => throw new NotImplementedException()
            };
        }

        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType(typeof(ApiResponse<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ValidationErrorResponse>>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var apiResponse = await _mediator.Send(command);
            return apiResponse.ResponseCode switch
            {
                (int)HttpStatusCode.OK => Ok(apiResponse),
                (int)HttpStatusCode.NoContent => NoContent(),
                (int)HttpStatusCode.BadRequest => BadRequest(apiResponse),
                (int)HttpStatusCode.InternalServerError => StatusCode((int)HttpStatusCode.InternalServerError, apiResponse),
                _ => throw new NotImplementedException()
            };

        }

        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ValidationErrorResponse>>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ApiResponse>> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var apiResponse = await _mediator.Send(command);
            return apiResponse.ResponseCode switch
            {
                (int)HttpStatusCode.OK => Ok(apiResponse),
                (int)HttpStatusCode.NoContent => NoContent(),
                (int)HttpStatusCode.BadRequest => BadRequest(apiResponse),
                (int)HttpStatusCode.InternalServerError => StatusCode((int)HttpStatusCode.InternalServerError, apiResponse),
                _ => throw new NotImplementedException()
            };
        }

    }
}
