using Microsoft.EntityFrameworkCore;
using StockCore.Data;
using StockCore.DTOs.Product;
using StockCore.Entities;
using StockCore.Interfaces;
using System.Net;

namespace StockCore.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IUploadFileService uploadFileService;

        public ProductService(DatabaseContext databaseContext,IUploadFileService uploadFileService)
        {
            this.databaseContext = databaseContext;
            this.uploadFileService = uploadFileService;
        }
        

        public async Task Create(Product product)
        {
            //throw new NotImplementedException();
            try
            {
                //var product = new Product
                //{
                //    ProductId = productRequest.ProductId,
                //    Name = productRequest.Name,
                //    Stock = productRequest.Stock,
                //    Price = productRequest.Price,
                //    CategoryId = productRequest.CategoryId,
                //    //FormFiles = productRequest.FormFilesame
                //};

                //var res = ProductRequest.Adapt(Product);
                  

                databaseContext.Products.Add(product);
                await databaseContext.SaveChangesAsync();
                //return StatusCode(201);
                //return Ok();
                //return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public async Task Delete(Product product)
        {
            //throw new NotImplementedException();
   
            //res.Username = model.Username;
            //res.Password = model.Password;
            databaseContext.Products.Remove(product);
            await databaseContext.SaveChangesAsync();
            //return NoContent();
        }

        public async Task<IEnumerable<Product>> findAll()
        {
            //return databaseContext.Products.Include(p => p.Category)
            //  .OrderByDescending(p => p.ProductId)
            //  .Select(ProductResponse.FromProduct).ToList();
            //throw new NotImplementedException();

            return await databaseContext.Products.Include(p => p.Category)
             .OrderByDescending(p => p.ProductId).ToListAsync();
        }

        public async Task<Product> FindById(int id)
        {

            //throw new NotImplementedException();
            return await  databaseContext.Products.Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.ProductId == id);
            //query on ly one data , if null then return null

        }

        public async Task<IEnumerable<Product>> Search(string name)
        {
            //throw new NotImplementedException();
            var res = await databaseContext.Products.Include(p => p.Category)
                .Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            return res;


        }

        public async Task Update(int id, Product product)
        {
            //throw new NotImplementedException();
            var res = databaseContext.Products.Update(product);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(List<IFormFile> formFiles)
        {
            //throw new NotImplementedException();
            String errorMessage = String.Empty;
            String ImageName = String.Empty;
            if (uploadFileService.IsUpload(formFiles))
            {
                errorMessage = uploadFileService.Validation(formFiles);
                if (String.IsNullOrEmpty(errorMessage))
                {
                    ImageName = (await uploadFileService.UploadImages(formFiles))[0];

                }
            }
            return (errorMessage, ImageName);

        }
    }
}
