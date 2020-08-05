using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MajorExam.ApiControllers
{
    [Authorize, RoutePrefix("api/Borrower")]
    public class MstMstBorrowerController : ApiController
    {
        Data.itelectdbDataContext db = new Data.itelectdbDataContext();

        [HttpGet, Route("list")]
        public List<ApiModels.MstBorrower> ListBorrower()
        {
            var borrower = from d in db.MstBorrowers
                            select new ApiModels.MstBorrower
                            {
                              Id = d.Id,
                              BorrowerNumber = d.BorrowerNumber,
                              ManualBorrowerNumber  = d.ManualBorrowerNumber,
                              FullName = d.FullName,
                              Department = d.Department,
                              Course = d.Course,
                              CreatedByUserId = d.CreatedByUserId,
                              CreatedDate = d.CreatedDate,
                              UpdatedByUserId = d.UpdatedByUserId,
                              UpdatedDate = d.UpdatedDate,
                              BorrowerTypeId = d.BorrowerTypeId,
                              LibraryCardId = d.LibraryCardId


                            };

            return borrower.ToList();
        }

        [HttpGet, Route("detail/{id}")]
        public ApiModels.MstBorrower DetailBorrowers(String id)
        {

            var borrower = from d in db.MstBorrowers
                              where d.Id == Convert.ToInt32(id)
                              select new ApiModels.MstBorrower
                              {
                                  Id = d.Id,
                                  BorrowerNumber = d.BorrowerNumber,
                                  ManualBorrowerNumber = d.ManualBorrowerNumber,
                                  FullName = d.FullName,
                                  Department = d.Department,
                                  Course = d.Course,
                                  CreatedByUserId = d.CreatedByUserId,
                                  CreatedDate = d.CreatedDate,
                                  UpdatedByUserId = d.UpdatedByUserId,
                                  UpdatedDate = d.UpdatedDate,
                                  BorrowerTypeId = d.BorrowerTypeId,
                                  LibraryCardId = d.LibraryCardId

                              };

            return borrower.FirstOrDefault();
        }

        [HttpPost, Route("add")]
        public HttpResponseMessage AddMstBorrower(ApiModels.MstBorrower objBorrower)
        {
            try
            {
                Data.MstBorrower newBorrower = new Data.MstBorrower
                {


                    Id = objBorrower.Id,
                    BorrowerNumber = objBorrower.BorrowerNumber,
                    ManualBorrowerNumber = objBorrower.ManualBorrowerNumber,
                    FullName = objBorrower.FullName,
                    Department = objBorrower.Department,
                    Course = objBorrower.Course,
                    CreatedByUserId = objBorrower.CreatedByUserId,
                    CreatedDate = objBorrower.CreatedDate,
                    UpdatedByUserId = objBorrower.UpdatedByUserId,
                    UpdatedDate = objBorrower.UpdatedDate,
                    BorrowerTypeId = objBorrower.BorrowerTypeId,
                    LibraryCardId = objBorrower.LibraryCardId
                };
                db.MstBorrowers.InsertOnSubmit(newBorrower);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateBorrower(ApiModels.MstBorrower objBorrower, String Id)
        {
            try
            {
                var Borrower = from d in db.MstBorrowers
                                where d.Id == Convert.ToInt32(Id)
                                select d;

                if (Borrower.Any())
                {
                    var updateBorrower = Borrower.FirstOrDefault();
                    updateBorrower.Id = objBorrower.Id;
                    updateBorrower.BorrowerNumber = objBorrower.BorrowerNumber;
                    updateBorrower.ManualBorrowerNumber = objBorrower.ManualBorrowerNumber;
                    updateBorrower.FullName = objBorrower.FullName;
                    updateBorrower.Department = objBorrower.Department;
                    updateBorrower.Course = objBorrower.Course;
                    updateBorrower.CreatedByUserId = objBorrower.CreatedByUserId;
                    updateBorrower.CreatedDate = objBorrower.CreatedDate;
                    updateBorrower.UpdatedByUserId = objBorrower.UpdatedByUserId;
                    updateBorrower.UpdatedDate = objBorrower.UpdatedDate;
                    updateBorrower.LibraryCardId = objBorrower.LibraryCardId;
                    updateBorrower.BorrowerTypeId = objBorrower.BorrowerTypeId;


                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Borrower not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteBorrower(String Id)
        {
            try
            {
                var Borrower = from d in db.MstBorrowers
                                where d.Id == Convert.ToInt32(Id)
                                select d;

                if (Borrower.Any())
                {
                    db.MstBorrowers.DeleteOnSubmit(Borrower.FirstOrDefault());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Borrower not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
    

