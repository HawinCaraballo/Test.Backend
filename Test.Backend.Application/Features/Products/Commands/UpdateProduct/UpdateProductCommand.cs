using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Backend.Application.Behaviours.Response;

namespace Test.Backend.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand: IRequest<ApiResponse>
    {
        public int ProductId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? Stock { get; set; } 
        public string? Description { get; set; } = string.Empty;
        public float? Price { get; set; }
    }
}
