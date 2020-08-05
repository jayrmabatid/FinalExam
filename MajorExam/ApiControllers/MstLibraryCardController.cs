using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MajorExam.ApiControllers
{
    [Authorize, RoutePrefix("api/librarycard")]
    public class MstLibraryCardController : ApiController
    {
        Data.itelectdbDataContext db = new Data.itelectdbDataContext();

        [HttpGet, Route("list")]
        public List<ApiModels.MstLibraryCard> ListLibraryCards()
        {
            var librarycard = from d in db.MstLibraryCards
                           select new ApiModels.MstLibraryCard
                           {
                               Id = d.Id,
                               LibraryCardNumber = d.LibraryCardNumber,
                               ManualLibraryCardNumber = d.ManualLibraryCardNumber,
                               BorrowerId = d.BorrowerId,
                               IsPrinted = d.IsPrinted,
                               LibraryInChargeUserId = d.LibraryInChargeUserId,
                               FootNote = d.FootNote,
                               CreatedByUserId = d.CreatedByUserId,
                               CreatedDate = d.CreatedDate,
                               UpdatedByUserId = d.UpdatedByUserId,
                               UpdatedDate = d.UpdatedDate

                           };

            return librarycard.ToList();
        }

        [HttpGet, Route("detail/{id}")]
        public ApiModels.MstLibraryCard DetailLibraryCards(String id)
        {

            var librarycard = from d in db.MstLibraryCards
                           where d.Id == Convert.ToInt32(id)
                           select new ApiModels.MstLibraryCard
                           {
                               Id = d.Id,
                               LibraryCardNumber = d.LibraryCardNumber,
                               ManualLibraryCardNumber = d.ManualLibraryCardNumber,
                               BorrowerId = d.BorrowerId,
                               IsPrinted = d.IsPrinted,
                               LibraryInChargeUserId = d.LibraryInChargeUserId,
                               FootNote = d.FootNote,
                               CreatedByUserId = d.CreatedByUserId,
                               CreatedDate = d.CreatedDate,
                               UpdatedByUserId = d.UpdatedByUserId,
                               UpdatedDate = d.UpdatedDate

                           };

            return librarycard.FirstOrDefault();
        }

        [HttpPost, Route("add")]
        public HttpResponseMessage AddLibraryCards(ApiModels.MstLibraryCard objLibraryCards)
        {
            try
            {
                Data.MstLibraryCard newLibraryCard = new Data.MstLibraryCard
                {

                    Id = objLibraryCards.Id,
                    LibraryCardNumber = objLibraryCards.LibraryCardNumber,
                    ManualLibraryCardNumber = objLibraryCards.ManualLibraryCardNumber,
                    BorrowerId = objLibraryCards.BorrowerId,
                    IsPrinted = objLibraryCards.IsPrinted,
                    LibraryInChargeUserId = objLibraryCards.LibraryInChargeUserId,
                    FootNote = objLibraryCards.FootNote,
                    CreatedByUserId = objLibraryCards.CreatedByUserId,
                    CreatedDate = objLibraryCards.CreatedDate,
                    UpdatedByUserId = objLibraryCards.UpdatedByUserId,
                    UpdatedDate = objLibraryCards.UpdatedDate
                };
                db.MstLibraryCards.InsertOnSubmit(newLibraryCard);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateLibraryCards(ApiModels.MstLibraryCard objLibraryCards, String Id)
        {
            try
            {
                var librarycard = from d in db.MstLibraryCards
                               where d.Id == Convert.ToInt32(Id)
                               select d;

                if (librarycard.Any())
                {
                    var updatelibrarycard = librarycard.FirstOrDefault();
                    updatelibrarycard.Id = objLibraryCards.Id;
                    updatelibrarycard.LibraryCardNumber = objLibraryCards.LibraryCardNumber;
                    updatelibrarycard.ManualLibraryCardNumber = objLibraryCards.ManualLibraryCardNumber;
                    updatelibrarycard.BorrowerId = objLibraryCards.BorrowerId;
                    updatelibrarycard.IsPrinted = objLibraryCards.IsPrinted;
                    updatelibrarycard.LibraryInChargeUserId = objLibraryCards.LibraryInChargeUserId;
                    updatelibrarycard.FootNote = objLibraryCards.FootNote;
                    updatelibrarycard.CreatedByUserId = objLibraryCards.CreatedByUserId;
                    updatelibrarycard.CreatedDate = objLibraryCards.CreatedDate;
                    updatelibrarycard.UpdatedByUserId = objLibraryCards.UpdatedByUserId;
                    updatelibrarycard.UpdatedDate = objLibraryCards.UpdatedDate;


                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "librarycard not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteLibraryCards(String Id)
        {
            try
            {
                var librarycard = from d in db.MstLibraryCards
                               where d.Id == Convert.ToInt32(Id)
                               select d;

                if (librarycard.Any())
                {
                    db.MstLibraryCards.DeleteOnSubmit(librarycard.FirstOrDefault());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "librarycard not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}