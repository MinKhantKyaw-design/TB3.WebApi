using TB3.WebApi.Controllers;

namespace TB3.WebApi.Services
{
    public interface IProductService
    {
        ProductResponseDto CreateProduct(ProductCreateRequestDto requestDto);
        ProductResponseDto DeleteProduct(int id);
        ProductGetByIdResponseDto GetProductById(int id);
        ProductGetResponseDto GetProducts(int pageNo, int pageSize);
        ProductResponseDto PatchProduct(int id, ProductPatchRequestDto requestDto);
        ProductResponseDto UpdateProduct(int id, ProductUpdateRequestDto requestDto);
    }
}