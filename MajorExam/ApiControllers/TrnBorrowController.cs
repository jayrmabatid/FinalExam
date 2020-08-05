using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MajorExam.ApiControllers
{
    [Authorize, RoutePrefix("api/trnborrow")]
    public class TrnBorrowController : ApiController
    {
        Data.itelectdbDataContext db = new Data.itelectdbDataContext();

        [HttpGet, Route("list")]
        public List<ApiModels.TrnBorrow> ListTrnBorrow()
        {
            var trnborrow = from d in db.TrnBorrows
                              select new ApiModels.TrnBorrow
                              {
                                   Id = d.Id,
                                  BorrowNumber = d.BookNumber,
                                 BookNumber = d.BookNumber,
                                  BorrowDate =d.BorrowDate,
                                  ManualBorrowNumber = d.ManualBorrowNumber,
                                  BorrowerId = d.BorrowerId,
                                  LibraryCardId =d.LibraryCardId, 
                                  PreparedByUser = d.PreparedByUser,
                                  CreatedByUserId = d.CreatedByUserId,
                                  CreatedDate = d.CreatedDate,
                                  UpdatedByUserId = d.UpdatedByUserId,
                                  UpdatedDate = d.UpdatedDate


                              };

            return trnborrow.ToList();
        }

        [HttpGet, Route("detail/{id}")]
        public ApiModels.TrnBorrow DetailLibraryCards(String id)
        {

            var librarycard = from d in db.TrnBorrows
                              where d.Id == Convert.ToInt32(id)
                              select new ApiModels.TrnBorrow
                              {
                                  Id = d.Id,
                                  BorrowNumber = d.BookNumber,
                                  BookNumber = d.BookNumber,
                                  BorrowDate = d.BorrowDate,
                                  ManualBorrowNumber = d.ManualBorrowNumber,
                                  BorrowerId = d.BorrowerId,
                                  LibraryCardId = d.LibraryCardId,
                                  PreparedByUser = d.PreparedByUser,
                                  CreatedByUserId = d.CreatedByUserId,
                                  CreatedDate = d.CreatedDate,
                                  UpdatedByUserId = d.UpdatedByUserId,
                                  UpdatedDate = d.UpdatedDate

                              };

            return librarycard.FirstOrDefault();
        }

        [HttpPost, Route("add")]
        public HttpResponseMessage AddTrnBorrow(ApiModels.TrnBorrow objTrnBorrow)
        {
            try
            {
                Data.TrnBorrow newTrnBorrow = new Data.TrnBorrow
                {

                 
                    Id = objTrnBorrow.Id,
                    BorrowNumber = objTrnBorrow.BookNumber,
                    BookNumber = objTrnBorrow.BookNumber,
                    BorrowDate = objTrnBorrow.BorrowDate,
                    ManualBorrowNumber = objTrnBorrow.ManualBorrowNumber,
                    BorrowerId = objTrnBorrow.BorrowerId,
                    LibraryCardId = objTrnBorrow.LibraryCardId,
                    PreparedByUser = objTrnBorrow.PreparedByUser,
                    CreatedByUserId = objTrnBorrow.CreatedByUserId,
                    CreatedDate = objTrnBorrow.CreatedDate,
                    UpdatedByUserId = objTrnBorrow.UpdatedByUserId,
                    UpdatedDate = objTrnBorrow.UpdatedDate
                };
                db.TrnBorrows.InsertOnSubmit(newTrnBorrow);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateTrnBorrow(ApiModels.TrnBorrow objTrnBorrow, String Id)
        {
            try
            {
                var trnBorrow = from d in db.TrnBorrows
                                  where d.Id == Convert.ToInt32(Id)
                                  select d;

                if (trnBorrow.Any())
                {
                    var updatetrnBorrow = trnBorrow.FirstOrDefault();
                    updatetrnBorrow.Id = objTrnBorrow.Id;
                    updatetrnBorrow.BorrowNumber = objTrnBorrow.BookNumber;
                    updatetrnBorrow.BookNumber = objTrnBorrow.BookNumber;
                    updatetrnBorrow.BorrowDate = objTrnBorrow.BorrowDate;
                    updatetrnBorrow.ManualBorrowNumber = objTrnBorrow.ManualBorrowNumber;
                    updatetrnBorrow.BorrowerId = objTrnBorrow.BorrowerId;
                    updatetrnBorrow.LibraryCardId = objTrnBorrow.LibraryCardId;
                    updatetrnBorrow.PreparedByUser = objTrnBorrow.PreparedByUser;
                    updatetrnBorrow.CreatedByUserId = objTrnBorrow.CreatedByUserId;
                    updatetrnBorrow.CreatedDate = objTrnBorrow.CreatedDate;
                    updatetrnBorrow.UpdatedByUserId = objTrnBorrow.UpdatedByUserId;
                    updatetrnBorrow.UpdatedDate = objTrnBorrow.UpdatedDate;


                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "trnBorrow not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteTrnBorrow(String Id)
        {
            try
            {
                var trnBorrow = from d in db.TrnBorrows
                                  where d.Id == Convert.ToInt32(Id)
                                  select d;

                if (trnBorrow.Any())
                {
                    db.TrnBorrows.DeleteOnSubmit(trnBorrow.FirstOrDefault());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "trnBorrow not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}