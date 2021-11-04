using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthwindAPI.Models;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //get all = api/Product/getAll
        [HttpGet("getAll")]
        public List<Product> GetAllProducts()
        {
            List<Product> result = null;
            using (NorthwindContext context = new NorthwindContext())
            {
                result = context.Products.ToList();
            }
            return result;
        }
        //get by Type = api/Product/productName/{name}
        [HttpGet("productName/{name}")]
        public List<Product> GetProductByName(string name)
        {
            List<Product> result = null;
            using (NorthwindContext context = new NorthwindContext())
            {
                result = context.Products.Where(a => a.ProductName == name).ToList();
            }
            return result;
        }
        //add product = api/Product/add?(all the parameters with & in betweeen)
        [HttpPost("add")]
        public Product AddProduct(string name, int supplierID, int categoryId, string quantityPerUnit, decimal unitPrice, short unitsInStock, short unitsOnOrder, short reorderLevel, bool discontinued)
        {
            Product newProduct = new Product();
            newProduct.ProductName = name;
            newProduct.SupplierId = supplierID;
            newProduct.CategoryId = categoryId;
            newProduct.QuantityPerUnit = quantityPerUnit;
            newProduct.UnitPrice = unitPrice;
            newProduct.UnitsInStock = unitsInStock;
            newProduct.UnitsOnOrder = unitsOnOrder;
            newProduct.ReorderLevel = reorderLevel;
            newProduct.Discontinued = discontinued;

            using (NorthwindContext context = new NorthwindContext())
            {
                context.Products.Add(newProduct);
                context.SaveChanges();
            }
            return newProduct;
        }

        //delete product = api/product/delete/bookshelf
        [HttpDelete("delete/{id}")]
        public Product DeleteByProductId(int id)
        {
            Product result = null;
            List<OrderDetail> orderDetail = null;
            using (NorthwindContext context = new NorthwindContext())
            {
                orderDetail = context.OrderDetails.Where(o => o.ProductId == id).ToList();

                foreach (OrderDetail o in orderDetail)
                {
                    context.OrderDetails.Remove(o);
                }
                context.SaveChanges();
                result = context.Products.Find(id);
                if (result != null)
                {
                    context.Products.Remove(result);
                }
                context.SaveChanges();
            }
            return result;
        }
    }
}
