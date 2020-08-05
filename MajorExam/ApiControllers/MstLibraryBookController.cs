using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MajorExam.ApiControllers
{
    [Authorize, RoutePrefix("api/librarybook")]
    public class MstLibraryBooksController : ApiController
    {
        Data.itelectdbDataContext db = new Data.itelectdbDataContext();

        [HttpGet, Route("list")]
        public List<ApiModels.MstLibraryBook> ListUsertypes()
        {
            var libraryBooks = from d in db.MstLibraryBooks
                           select new ApiModels.MstLibraryBook
                           {
                               Id = d.Id,
                               BookNumber = d.BookNumber,
                               Title = d.Title,
                               Author = d.Author,
                               EditionNumber = d.EditionNumber,
                               PlaceOfPublication = d.PlaceOfPublication,
                               CopyRightDate = d.CopyRightDate,
                               ISBN = d.ISBN,
                               CreatedByUserId =  d.CreatedByUserId,
                               CreatedBy  = d.CreatedBy,
                               CreatedDate = d.CreatedDate,
                               UpdatedByUserId = d.UpdatedByUserId,
                               UpdatedBy = d.UpdatedBy,
                               UpdatedDate = d.UpdatedDate
                           };

            return libraryBooks.ToList();
        }

        [HttpGet, Route("detail/{id}")]
        public ApiModels.MstLibraryBook DetailLibraryBooks(String id)
        {

            var libraryBooks = from d in db.MstLibraryBooks
                           where d.Id == Convert.ToInt32(id)
                           select new ApiModels.MstLibraryBook
                           {
                               Id = d.Id,
                               BookNumber = d.BookNumber,
                               Title = d.Title,
                               Author = d.Author,
                               EditionNumber = d.EditionNumber,
                               PlaceOfPublication = d.PlaceOfPublication,
                               CopyRightDate = d.CopyRightDate,
                               ISBN = d.ISBN,
                               CreatedByUserId = d.CreatedByUserId,
                               CreatedBy = d.CreatedBy,
                               CreatedDate = d.CreatedDate,
                               UpdatedByUserId = d.UpdatedByUserId,
                               UpdatedBy = d.UpdatedBy,
                               UpdatedDate = d.UpdatedDate
                           };

            return libraryBooks.FirstOrDefault();
        }

        [HttpPost, Route("add")]
        public HttpResponseMessage AddLibraryBooks(ApiModels.MstLibraryBook objLibraryBooks)
        {
            try
            {
                Data.MstLibraryBook newLibraryBooks = new Data.MstLibraryBook
                {
                    Id = objLibraryBooks.Id,
                    BookNumber = objLibraryBooks.BookNumber,
                    Title = objLibraryBooks.Title,
                    Author = objLibraryBooks.Author,
                    EditionNumber = objLibraryBooks.EditionNumber,
                    PlaceOfPublication = objLibraryBooks.PlaceOfPublication,
                    CopyRightDate = objLibraryBooks.CopyRightDate,
                    ISBN = objLibraryBooks.ISBN,
                    CreatedByUserId = objLibraryBooks.CreatedByUserId,
                    CreatedBy = objLibraryBooks.CreatedBy,
                    CreatedDate = objLibraryBooks.CreatedDate,
                    UpdatedByUserId = objLibraryBooks.UpdatedByUserId,
                    UpdatedBy = objLibraryBooks.UpdatedBy,
                    UpdatedDate = objLibraryBooks.UpdatedDate
                };
                db.MstLibraryBooks.InsertOnSubmit(newLibraryBooks);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateLibraryBooks(ApiModels.MstLibraryBook objLibraryBooks, String Id)
        {
            try
            {
                var libraryBooks = from d in db.MstLibraryBooks
                               where d.Id == Convert.ToInt32(Id)
                               select d;

                if (libraryBooks.Any())
                {
                    var updateLibraryBooks = libraryBooks.FirstOrDefault();
                    updateLibraryBooks.Id = objLibraryBooks.Id;
                    updateLibraryBooks.BookNumber = objLibraryBooks.BookNumber;
                    updateLibraryBooks.Title = objLibraryBooks.Title;
                    updateLibraryBooks.Author = objLibraryBooks.Author;
                    updateLibraryBooks.EditionNumber = objLibraryBooks.EditionNumber;
                    updateLibraryBooks.PlaceOfPublication = objLibraryBooks.PlaceOfPublication;
                    updateLibraryBooks.CopyRightDate = objLibraryBooks.CopyRightDate;
                    updateLibraryBooks.ISBN = objLibraryBooks.ISBN;
                    updateLibraryBooks.CreatedByUserId = objLibraryBooks.CreatedByUserId;
                    updateLibraryBooks.CreatedBy = objLibraryBooks.CreatedBy;
                    updateLibraryBooks.CreatedDate = objLibraryBooks.CreatedDate;
                    updateLibraryBooks.UpdatedByUserId = objLibraryBooks.UpdatedByUserId;
                    updateLibraryBooks.UpdatedBy = objLibraryBooks.UpdatedBy;
                    updateLibraryBooks.UpdatedDate = objLibraryBooks.UpdatedDate;



                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "libraryBooks not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteLibraryBook(String Id)
        {
            try
            {
                var libraryBooks = from d in db.MstLibraryBooks
                               where d.Id == Convert.ToInt32(Id)
                               select d;

                if (libraryBooks.Any())
                {
                    db.MstLibraryBooks.DeleteOnSubmit(libraryBooks.FirstOrDefault());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "UserType not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
