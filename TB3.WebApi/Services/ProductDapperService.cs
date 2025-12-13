using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TB3.WebApi.Controllers;

namespace TB3.WebApi.Services;

public class ProductDapperService : IProductDapperService
{
    private readonly string _connectionString;

    public ProductDapperService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DbConnection")!;
    }

    public ProductGetResponseDto GetProducts(int pageNo, int pageSize)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();

            int skip = (pageNo - 1) * pageSize;

            string query = @"
            SELECT ProductId, ProductName, Quantity, Price
            FROM Tbl_Product
            WHERE DeleteFlag = 0
            ORDER BY ProductId DESC
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY";

            var lst = db.Query<ProductDto>(query, new
            {
                Skip = skip,
                Take = pageSize
            }).ToList();

            return new ProductGetResponseDto()
            {
                IsSuccess = true,
                Message = "Success.",
                Products = lst
            };
        }
    }

    public ProductGetByIdResponseDto GetProduct(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            string query = "select * from Tbl_Product where DeleteFlag = 0 and ProductId = @id";
            var item = db.QueryFirstOrDefault<ProductDto>(query, new { id = id });

            if (item is null)
            {
                return new ProductGetByIdResponseDto()
                {
                    IsSuccess = false,
                    Message = "Product not found."
                };
            }

            return new ProductGetByIdResponseDto()
            {
                IsSuccess = true,
                Message = "Success.",
                Product = item
            };
        }
    }

    public ProductResponseDto CreateProduct(ProductCreateRequestDto request)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[Quantity]
           ,[Price]
           ,[DeleteFlag]
           ,[CreatedDateTime])
     VALUES(@ProductName, @Quantity, @Price, 0, @DateTime)";

            int result = db.Execute(query, new
            {
                DateTime = DateTime.Now,
                Price = request.Price,
                ProductName = request.ProductName,
                Quantity = request.Quantity,
            });

            if (result > 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = true,
                    Message = "Product created successfully."
                };
            }

            return new ProductResponseDto()
            {
                IsSuccess = false,
                Message = "Failed to create product."
            };
        }
    }

    public ProductResponseDto UpdateProduct(int id, ProductUpdateRequestDto request)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            string query = @"UPDATE [dbo].[Tbl_Product]
   SET ProductName = @ProductName, Quantity = @Quantity, Price = @Price, ModifiedDateTime = @ModifiedDateTime where ProductId = @ProductId";

            int rowAffected = db.Execute(query, new
            {
                ProductId = id,
                ProductName = request.ProductName,
                Quantity = request.Quantity,
                Price = request.Price,
                ModifiedDateTime = DateTime.Now
            });

            if (rowAffected > 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = true,
                    Message = "Product updated successfully."
                };
            }

            return new ProductResponseDto()
            {
                IsSuccess = false,
                Message = "Failed to update product."
            };
        }
    }

    public ProductResponseDto PatchProduct(int id, ProductPatchRequestDto request)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            string query = @"UPDATE [dbo].[Tbl_Product] SET ModifiedDateTime = @ModifiedDateTime, ";

            if (!string.IsNullOrEmpty(request.ProductName))
            {
                query += "ProductName = @ProductName, ";
            }
            if (request.Price is not null && request.Price > 0)
            {
                query += "Price = @Price, ";
            }
            if (request.Quantity is not null && request.Quantity > 0)
            {
                query += "Quantity = @Quantity, ";
            }

            // အဆုံးမှာပိုနေတဲ့ coma ကိုဖယ်ထုတ်ခြင်း
            query = query.TrimEnd(',', ' ');

            query += " where ProductId = @ProductId;";

            int result = db.Execute(query, new
            {
                ProductId = id,
                ModifiedDateTime = DateTime.Now,
                Price = request.Price,
                ProductName = request.ProductName,
                Quantity = request.Quantity,
            });

            if (result > 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = true,
                    Message = "Product patched successfully."
                };
            }

            return new ProductResponseDto()
            {
                IsSuccess = false,
                Message = "Failed to patch product."
            };
        }
    }

    public ProductResponseDto DeleteProduct(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            db.Open();
            string query = @"UPDATE [dbo].[Tbl_Product]
   SET DeleteFlag = 1, ModifiedDateTime = @ModifiedDateTime where ProductId = @ProductId";

            int result = db.Execute(query, new
            {
                ProductId = id,
                ModifiedDateTime = DateTime.Now
            });

            if (result > 0)
            {
                return new ProductResponseDto()
                {
                    IsSuccess = true,
                    Message = "Product deleted successfully."
                };
            }

            return new ProductResponseDto()
            {
                IsSuccess = false,
                Message = "Failed to delete product."
            };
        }
    }
}