using AutoMapper;
using CleanArchitecture.WebApiFramework.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.WebApiFramework.Application.Features.Products.Queries.GetAllProducts;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Commands.CreateClave;
using CleanArchitecture.WebApiFramework.Application.Features.RegistroSistema.Queries.GetKeyValues;
using CleanArchitecture.WebApiFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.WebApiFramework.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
            CreateMap<CreateClaveCommand, ClaveRegistro>();
          //  CreateMap <GetKeyValuesCommand, IEnumerable<ClaveRegistro>()();
            CreateMap<CreateProductCommand, Product>();
        }
    }
}
