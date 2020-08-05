using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MajorExam.ApiControllers
{
    [Authorize, RoutePrefix("api/usertype")]
    public class MstUserTypeController : ApiController
    {
        Data.itelectdbDataContext db = new Data.itelectdbDataContext();

        [HttpGet, Route("list")]
        public List<ApiModels.MstUserType> ListUsertypes()
        {
            var usertype = from d in db.MstUserTypes
                          select new ApiModels.MstUserType
                          {
                              Id = d.Id,
                              UserType = d.UserType
                          };

            return usertype.ToList();
        }

        [HttpGet, Route("detail/{id}")]
        public ApiModels.MstUserType DetailUsertypes(String id)
        {

            var usertype = from d in db.MstUserTypes
                          where d.Id == Convert.ToInt32(id)
                          select new ApiModels.MstUserType
                          {
                              Id = d.Id,
                              UserType = d.UserType
                          };

            return usertype.FirstOrDefault();
        }

        [HttpPost, Route("add")]
        public HttpResponseMessage AddUsertypes(ApiModels.MstUserType objUsertypes)
        {
            try
            {
                Data.MstUserType newUsertype = new Data.MstUserType
                {
                   
                    Id = objUsertypes.Id,
                    UserType = objUsertypes.UserType
                };
                db.MstUserTypes.InsertOnSubmit(newUsertype);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateUsertypes(ApiModels.MstUserType objUsertypes, String Id)
        {
            try
            {
                var usertype = from d in db.MstUserTypes
                             where d.Id == Convert.ToInt32(Id)
                             select d;

                if (usertype.Any())
                {
                    var updateUsertypes = usertype.FirstOrDefault();
                    updateUsertypes.Id = objUsertypes.Id;
                    updateUsertypes.UserType = objUsertypes.UserType;
                    

                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "UserType not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteUserType(String Id)
        {
            try
            {
                var usertype = from d in db.MstUserTypes
                              where d.Id == Convert.ToInt32(Id)
                              select d;

                if (usertype.Any())
                {
                    db.MstUserTypes.DeleteOnSubmit(usertype.FirstOrDefault());
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