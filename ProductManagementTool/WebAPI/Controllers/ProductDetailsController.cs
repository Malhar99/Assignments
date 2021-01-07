using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;
using log4net;

namespace WebAPI.Controllers
{
    public class ProductDetailsController : ApiController
    {
        readonly ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        private readonly ProductmanagmentDBEntities db = new ProductmanagmentDBEntities();
        // GET: api/ProductDetails
        /// <summary>
        /// This function is Get Product Details from The Database
        /// </summary>
        /// <returns>The product Details</returns>
        public IQueryable<ProductDetail> GetProductDetails()
        {
            log.Info("GET Product Called" + DateTime.Now.ToString());
            return db.ProductDetails;
        }

        // GET: api/ProductDetails/5
        /// <summary>
        ///  This function is Get Product Details by Product ID from The Database
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>The product Details</returns>
        [ResponseType(typeof(ProductDetail))]
        public IHttpActionResult GetProductDetail(int id)
        {
            ProductDetail productDetail = db.ProductDetails.Find(id);
            if (productDetail == null)
            {
                return NotFound();
            }
            log.Info("GET Product by id Called for ProductID: "+ id + DateTime.Now.ToString());
            return Ok(productDetail);
        }

        // PUT: api/ProductDetails/5
        /// <summary>
        /// This function is Update Product Details by Product ID into The Database
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="productDetail">Product Model Object</param>
        /// <returns>The Status</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductDetail(int id, ProductDetail productDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productDetail.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productDetail).State = EntityState.Modified;

            try
            {
                log.Info("PUT request for for ProductID: " + id + DateTime.Now.ToString());
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ProductDetailExists(id))
                {
                    log.Error(e.Message);
                    return NotFound();
                }
                else
                {
                    log.Error(e.Message);
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProductDetails
        /// <summary>
        /// This function is Insert Product Details into The Database
        /// </summary>
        /// <param name="productDetail">ProductDetail Model Object</param>
        /// <returns>The Status</returns>
        [ResponseType(typeof(ProductDetail))]
        public IHttpActionResult PostProductDetail(ProductDetail productDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool check = db.ProductDetails.Count(e => e.Name == productDetail.Name.ToString() && e.Category == productDetail.Category.ToString()) > 0;
            if (!check)
            {
                db.ProductDetails.Add(productDetail);
                log.Info("POST request for ProductName " + productDetail.Name + DateTime.Now.ToString());
                db.SaveChanges();
            }
            else
            {
                var message1 = string.Format("Product Exists");
                log.Info("POST request is called for existing product with ProductName " + productDetail.Name + DateTime.Now.ToString());
                return Content(HttpStatusCode.BadRequest, message1);
            }
            
            return CreatedAtRoute("DefaultApi", new { id = productDetail.ProductID }, productDetail);
        }
        /// <summary>
        /// This function is Insert Image into the folder 
        /// </summary>
        /// <returns>The Status</returns>
        [Route("user/PostUserImage")]
        [AllowAnonymous]
        public HttpResponseMessage PostUserImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 10; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName);
                            postedFile.SaveAs(filePath);

                        }
                    }

                }
                var message1 = string.Format("Image Updated Successfully.");
                log.Info("POST request for Image Upload"+ DateTime.Now.ToString());
                return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // DELETE: api/ProductDetails/5
        [ResponseType(typeof(ProductDetail))]
        public IHttpActionResult DeleteProductDetail(int id)
        {
            ProductDetail productDetail = db.ProductDetails.Find(id);
            if (productDetail == null)
            {
                return NotFound();
            }

            db.ProductDetails.Remove(productDetail);
            db.SaveChanges();
            log.Info("DELETE request for ProductID" + id + DateTime.Now.ToString());
            return Ok(productDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductDetailExists(int id)
        {
            return db.ProductDetails.Count(e => e.ProductID == id) > 0;
        }
    }
}