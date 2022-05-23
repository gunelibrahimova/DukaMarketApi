using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProductDal : EfEntityRepositoryBase<Product, ShopDbContext>, IProductDal
    {
        public ProductDTO FindById(int id)
        {
            using (ShopDbContext context = new())
            {
                var product = context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
                var productPictures = context.ProductPicture.Where(x => x.ProductId == id).ToList();
                var comments = context.Comments.Where(x=>x.ProductId == product.Id).ToList();


                decimal raitingSum = 0;
                int ratingCount = 0;

                List<CommentDTO> commentsRasult = new();

                for(int i = 0; i < comments.Count; i++)
                {
                    CommentDTO comment = new()
                    {
                        ProductId = product.Id,
                        UserName = comments[i].UserName,
                        UserEmail = comments[i].UserEmail,
                        Review = comments[i].Review,
                        Ratings = comments[i].Ratings,
                        
                    };
                    commentsRasult.Add(comment);
                }


                List<string> pictures = new();

                foreach (var picture in productPictures)
                {
                    pictures.Add(picture.PhotoURL);
                }

                foreach (var rating in comments.Where(x=>x.Ratings !=0))
                {
                    ratingCount++;
                    raitingSum += rating.Ratings;
                }

                if(ratingCount == 0)
                {
                    raitingSum = 0;
                }
                else
                {
                    raitingSum /= ratingCount;
                }
                ProductDTO result = new()
                {
                    id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryName = product.Category.Name,
                    Price = product.Price,
                    SKU = product.SKU,
                    Summary = product.Summary,
                    ProductPicture = pictures,
                    CoverPhoto = product.CoverPhoto,
                    IsSlider = product.IsSlider,
                    Rating = Math.Round(raitingSum, 1),
                    Comments = commentsRasult,
                };
                return result;
            }
        }
        public List<ProductDTO> GetAllProducts()
        {
            using (ShopDbContext context = new())
            {
                var products = context.Products.Include(x => x.Category).Include(x => x.ProductPicture).ToList();
                var productPictures = context.ProductPicture;
                var ratings = context.Comments;
                List<ProductDTO> result = new();

                

                for (int i = 0; i < products.Count; i++)
                {
                    decimal raitingSum = 0;
                    int ratingCount = 0;
                    List<string> pictures = new();
                    foreach (var item in productPictures.Where(x => x.ProductId == products[i].Id))
                    {
                        pictures.Add(item.PhotoURL);
                    }

                    foreach (var rating in ratings.Where(x=>x.ProductId == products[i].Id && x.Ratings != 0))
                    {
                        ratingCount++;
                        ratingCount++;
                        raitingSum += rating.Ratings;
                    }

                    if (raitingSum == 0)
                    {
                        raitingSum = 0;
                    }
                    else
                    {
                        raitingSum = raitingSum / ratingCount;
                    }
                    
                    ProductDTO productList = new()
                    {
                        id = products[i].Id,
                        Name = products[i].Name,
                        Description = products[i].Description,
                        CategoryName = products[i].Category.Name,
                        Price = products[i].Price,
                        SKU = products[i].SKU,
                        Summary = products[i].Summary,
                        ProductPicture = pictures,
                        CoverPhoto = products[i].CoverPhoto,
                        IsSlider = products[i].IsSlider,
                        Rating = Math.Round(raitingSum, 1),
                    };
                    result.Add(productList);
                }

                return result;
            }
        }
    }
}
