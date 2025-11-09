using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductListManagement.Service.Dtos
{
    public class FilterDto
    {
        [FromQuery]
        public string? Name { get; set; }

        [FromQuery]
        public double? Price { get; set; }

        [FromQuery]
        [Range(0, double.MaxValue)]
        public double? MinPrice { get; set; }

        [FromQuery]
        [Range(0, double.MaxValue)]
        public double? MaxPrice { get; set; }
    }
}
