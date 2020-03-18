using System.Collections.Generic;

namespace Northwind.Application.Categories.Dtos
{
    public class CategoriesListDto
    {
        public IList<CategoryDto> Categories { get; set; }

        public int Count { get; set; }
    }
}
