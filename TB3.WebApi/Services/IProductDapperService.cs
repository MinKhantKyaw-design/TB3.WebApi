using TB3.WebApi.Controllers;

namespace TB3.WebApi.Services
{
    public interface IProductDapperService
    {
        ProductResponseDto CreateProduct(ProductCreateRequestDto request);
        ProductResponseDto DeleteProduct(int id);
        ProductGetByIdResponseDto GetProduct(int id);
        ProductGetResponseDto GetProducts(int pageNo, int pageSize);
        ProductResponseDto PatchProduct(int id, ProductPatchRequestDto request);
        ProductResponseDto UpdateProduct(int id, ProductUpdateRequestDto request);
    }
}