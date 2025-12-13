using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB3.WebApi.Services;

namespace TB3.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductDapperController : ControllerBase
{
    private readonly IProductDapperService _service;

    public ProductDapperController(IProductDapperService service)
    {
        _service = service;
    }
    
    [HttpGet("{pageNo}/{pageSize}")]
    public IActionResult GetProducts(int pageNo = 1, int pageSize = 10)
    {
        var result = _service.GetProducts(pageNo, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var result = _service.GetProduct(id);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductCreateRequestDto request)
    {
        var result = _service.CreateProduct(request);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductUpdateRequestDto request)
    {
        var result = _service.UpdateProduct(id, request);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchProduct(int id, ProductPatchRequestDto request)
    {
        var result = _service.PatchProduct(id, request);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var result = _service.DeleteProduct(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
