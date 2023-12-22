
namespace Test.Backend.Application.Mappings
{
    using AutoMapper;
    using Test.Backend.Application.Features.Products.Commands.CreateProduct;
    using Test.Backend.Application.Features.Products.Commands.UpdateProduct;
    using Test.Backend.Application.Features.Products.Dtos;
    using Test.Backend.Domain;

    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Products, ProductDto>();
            CreateMap<CreateProductCommand, Products>();
            CreateMap<CreateProductCommand, ProductDto>();
            CreateMap<UpdateProductCommand, ProductDto>();
            CreateMap<UpdateProductCommand, Products>();
            CreateMap<Products, ProductDtoQuery>();
        }
    }
}
