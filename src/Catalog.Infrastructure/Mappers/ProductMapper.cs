using Catalog.Data.Dtos;
using Catalog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static ProductDTO MapToDTO(this Product product)
        {
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                Title = product.Title,
                Category = product.Category,
                Summary = product.Summary,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Price = product.Price
            };
        }

        public static Product MapToEntity(this ProductDTO productDTO)
        {
            if (productDTO == null) return null;

            return new Product
            {
                Id = productDTO.Id,
                Title = productDTO.Title,
                Category = productDTO.Category,
                Summary = productDTO.Summary,
                Description = productDTO.Description,
                ImageFile = productDTO.ImageFile,
                Price = productDTO.Price
            };
        }
    }

}
