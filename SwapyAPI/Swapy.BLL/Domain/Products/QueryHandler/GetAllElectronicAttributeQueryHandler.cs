﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.QueryHandler
{
    public class GetAllElectronicAttributeQueryHandler : IRequestHandler<GetAllElectronicAttributeQuery, IEnumerable<ElectronicAttribute>>
    {
        private readonly Guid _userId;
        private readonly IElectronicAttributeRepository _electronicAttributeRepository;

        public GetAllElectronicAttributeQueryHandler(Guid userId, IElectronicAttributeRepository electronicAttributeRepository)
        {
            _userId = userId;
            _electronicAttributeRepository = electronicAttributeRepository;
        }

        public async Task<IEnumerable<ElectronicAttribute>> Handle(GetAllElectronicAttributeQuery request, CancellationToken cancellationToken)
        {
            var query = await _electronicAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId == request.CategoryId) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId == request.SubcategoryId) &&
                (request.CityId == null || x.Product.CityId == request.CityId) &&
                (request.UserId == null ? x.Product.UserId != _userId : x.Product.UserId == request.UserId) &&
                (request.IsNew == null || x.IsNew == request.IsNew) &&
                (request.MemoriesId == null || request.MemoriesId.Contains(x.MemoryModel.MemoryId)) &&
                (request.ColorsId == null || request.ColorsId.Contains(x.ModelColor.ColorId)) &&
                (request.ModelsId == null || request.ModelsId.Contains(x.MemoryModel.ModelId)) &&
                ((request.BrandsId == null && request.ModelsId != null) || request.BrandsId.Contains(x.MemoryModel.Model.ElectronicBrandType.ElectronicBrandId)) &&
                ((request.TypesId == null && request.ModelsId != null) || request.TypesId.Contains(x.MemoryModel.Model.ElectronicBrandType.ElectronicTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();

            return result;
        }
    }
}
