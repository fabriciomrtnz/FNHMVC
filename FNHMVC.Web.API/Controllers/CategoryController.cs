//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Web.Http;
//using FNHMVC.Data.Repositories;
//using FNHMVC.Model;
//using System.Net;

//namespace FNHMVC.Web.API.Controllers
//{
//    public class CategoryController : ApiController
//    {
//        private readonly ICategoryRepository categoryRepository;
//        public CategoryController(ICategoryRepository categoryRepository)
//        {

//            this.categoryRepository = categoryRepository;
//        }

//        public IEnumerable<Category> Get()
//        {
//            //var categories = categoryRepository.GetAll().ToList();
//            //return categories;
//            var categories = new List<Category>{
//                new Category { CategoryId=1, Name="CateName", Description="Description"}
//            };
//            return categories;
//        }

//        // GET /api/categoryservice/5
//        public Category Get(int id)
//        {
//            var category = categoryRepository.GetById(id);
//            if (category == null)
//                throw new HttpResponseException(HttpStatusCode.NotFound);
//            return category;
//        }

//        // POST /api/category
//        public HttpResponseMessage<Category> Post(Category value)
//        {
//            if (ModelState.IsValid)
//            {
//                //to do : Insert

//                //Created!
//                var response = new HttpResponseMessage<Category>(value, HttpStatusCode.Created);

//                //Let them know where the new NotATweet is
//                string uri = Url.Route(null, new { id = value.CategoryId });
//                response.Headers.Location = new Uri(Request.RequestUri, uri);

//                return response;

//            }
//            throw new HttpResponseException(HttpStatusCode.BadRequest);
//        }

//        // PUT /api/category/5
//        public HttpResponseMessage Put(int id, Category value)
//        {
//            if (ModelState.IsValid)
//            {
//               //to do: Update
//                return new HttpResponseMessage(HttpStatusCode.NoContent);
//            }
//            throw new HttpResponseException(HttpStatusCode.BadRequest);
//        }

//        // DELETE /api/category/5
//        public void Delete(int id)
//        {
//            var category = categoryRepository.GetById(id);
//            if (category == null)
//                throw new HttpResponseException(HttpStatusCode.NotFound);
//            //Delete
//        }
//    }
//}