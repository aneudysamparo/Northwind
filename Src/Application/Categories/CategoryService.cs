using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Application.Categories.Dtos;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Common.Services;
using System.Threading.Tasks;

namespace Northwind.Application.Categories
{
    [Service(ServiceLifetime.Transient)]
    public class CategoryService: AppService
    {
        private readonly INorthwindDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(INorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoriesListDto> GetCategoriesAsync()
        {
            var categories = await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var dto = new CategoriesListDto
            {
                Categories = categories,
                Count = categories.Count
            };

            return dto;
        }
    }
}
