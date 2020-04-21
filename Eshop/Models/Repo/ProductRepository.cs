using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Eshop.Models.Tables;
namespace Eshop.Models.Repo
{
    public class ProductRepository
    {
        private readonly MyContext _context = MyContext.GetInstance();

        public ProductLabelImages GetProductById(int id)
        {
            var images = GetImages();

            var query = (from Products in _context.Product
                         join Categories in _context.Category on Products.Label_Id equals Categories.Id
                         where Products.Id == id
                         select new
                         {
                             Id = Products.Id,
                             Name = Products.Name,
                             Price = Products.Price,
                             Vendor = Products.Vendor,
                             Availability = Products.Availability,
                             Sku = Products.Sku,
                             Score = Products.Score,
                             Label_Id = Products.Label_Id,
                             Description = Products.Description,
                             Label_Name = Categories.The_Label,
                         }).AsEnumerable().Select(x =>

                            new ProductLabelImages
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Description = x.Description,
                                Price = x.Price,
                                Vendor = x.Vendor,
                                Sku = x.Sku,
                                Availability = x.Availability,
                                Score = x.Score,
                                Label_Id = x.Label_Id,
                                Label_Name = x.Label_Name,
                                Photo = images.Where(y => y.Product_Id == id).Select(y => y.TheImage).ToArray(),
                            }).FirstOrDefault();
            return query;
        }

        public List<ProductLabelImages> GetEightTopSoldProducts()
        {
            var images = GetImages();

            var query = (from Products in _context.Product
                         join Categories in _context.Category on Products.Label_Id equals Categories.Id
                         where Products.Label_Id == 1
                         select new
                         {
                             Id = Products.Id,
                             Name = Products.Name,
                             Description = Products.Description,
                             Price = Products.Price,
                             Vendor = Products.Vendor,
                             Sku = Products.Sku,
                             Availability = Products.Availability,
                             Score = Products.Score,
                             Label_Name = Categories.The_Label,
                         }).AsEnumerable()
                             .Select(x =>
                             new ProductLabelImages
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 Description = x.Description,
                                 Price = x.Price,
                                 Vendor = x.Vendor,
                                 Sku = x.Sku,
                                 Availability = x.Availability,
                                 Score = x.Score,
                                 Label_Name = x.Label_Name,
                                 Photo = images.Where(y => y.Product_Id == x.Id).Select(y => y.TheImage).ToArray(),
                             }).Take(8).ToList();

            return query;

        }

        public List<ProductLabelImages> GetEightBestSellers()
        {
            var images = GetImages();

            var query = (from Products in _context.Product
                         join Categories in _context.Category on Products.Label_Id equals Categories.Id
                         orderby Products.Name
                         select new
                         {
                             Id = Products.Id,
                             Name = Products.Name,
                             Description = Products.Description,
                             Price = Products.Price,
                             Vendor = Products.Vendor,
                             Sku = Products.Sku,
                             Availability = Products.Availability,
                             Score = Products.Score,
                             Label_Name = Categories.The_Label,
                         }).AsEnumerable()
                             .Select(x =>
                             new ProductLabelImages
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 Description = x.Description,
                                 Price = x.Price,
                                 Vendor = x.Vendor,
                                 Sku = x.Sku,
                                 Availability = x.Availability,
                                 Score = x.Score,
                                 Label_Name = x.Label_Name,
                                 Photo = images.Where(y => y.Product_Id == x.Id).Select(y => y.TheImage).ToArray(),
                             }).Take(8).ToList();

            return query;
        }

        public List<ProductLabelImages> GetEightNewArrivals()
        {
            var images = GetImages();

            var query = (from Products in _context.Product
                         join Categories in _context.Category on Products.Label_Id equals Categories.Id
                         orderby Products.Sku
                         select new
                         {
                             Id = Products.Id,
                             Name = Products.Name,
                             Description = Products.Description,
                             Price = Products.Price,
                             Vendor = Products.Vendor,
                             Sku = Products.Sku,
                             Availability = Products.Availability,
                             Score = Products.Score,
                             Label_Name = Categories.The_Label,
                         }).AsEnumerable()
                             .Select(x =>
                             new ProductLabelImages
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 Description = x.Description,
                                 Price = x.Price,
                                 Vendor = x.Vendor,
                                 Sku = x.Sku,
                                 Availability = x.Availability,
                                 Score = x.Score,
                                 Label_Name = x.Label_Name,
                                 Photo = images.Where(y => y.Product_Id == x.Id).Select(y => y.TheImage).ToArray(),
                             }).Take(8).ToList();

            return query;
        }

        public List<ProductLabelImages> GetBestRated()
        {
            var images = GetImages();
            var query = (from p in _context.Product
                         orderby p.Score descending
                         select new
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Description = p.Description,
                             Price = p.Price,
                             Availability = p.Availability,
                             Score = p.Score,
                         }).AsEnumerable()
                         .Select(x =>
                         new ProductLabelImages
                         {
                             Name = x.Name,
                             Price = x.Price,
                             Description = x.Description,
                             Availability = x.Availability,
                             Score = x.Score,
                             Photo = images.Where(y => y.Product_Id == x.Id).Select(y => y.TheImage).ToArray(),
                         }).Take(3).ToList();

            return query;
        }

        public List<ProductLabelImages> GetThreeProducts()
        {
            var images = GetImages();

            var query = (from p in _context.Product
                         select new
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Description = p.Description,
                             Price = p.Price,
                             Availability = p.Availability,
                             Score = p.Score,
                         }).AsEnumerable()
                         .Select(x =>
                         new ProductLabelImages
                         {
                             Name = x.Name,
                             Price = x.Price,
                             Availability = x.Availability,
                             Score = x.Score,
                             Photo = images.Where(y => y.Product_Id == x.Id).Select(y => y.TheImage).ToArray(),
                         }).Take(3).ToList();
            return query;
        }

        public List<ProductLabelImages> GetRelatedProducts(ProductLabelImages product)
        {
            var images = GetImages();

            return (from p in _context.Product
                    join c in _context.Category on p.Label_Id equals c.Id
                    where product.Label_Id == p.Label_Id
                    select new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Availability = p.Availability,
                        Score = p.Score,
                    }).AsEnumerable()
                         .Select(x =>
                     new ProductLabelImages
                     {
                         Name = x.Name,
                         Price = x.Price,
                         Availability = x.Availability,
                         Score = x.Score,
                         Photo = images.Where(y => y.Product_Id == x.Id).Select(y => y.TheImage).ToArray(),
                     }).Take(4).ToList();
        }

        public List<Image> GetImages()
        {
            return (from i in _context.Image
                    select i).ToList();
        }
    }
}