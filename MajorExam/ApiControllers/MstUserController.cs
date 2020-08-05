using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MajorExam.ApiControllers
{
    [Authorize, RoutePrefix("api/user")]
    public class MstUserController : ApiController
    {
        Data.itelectdbDataContext db = new Data.itelectdbDataContext();

        [HttpGet, Route("list")]
        public List<ApiModels.MstUser> ListUser()
        {
            var User = from d in db.MstUsers
                            select new ApiModels.MstUser
                            {
                                Id = d.Id,
                                FirstName = d.FirstName,
                                LastName = d.LastName,
                                Password = d.Password,
                                UserTypeId = d.UserTypeId,
                                AspNetUserId = d.AspNetUserId,
                                UserName = d.UserName,
                                Email = d.Email


                            };

            return User.ToList();
        }

        [HttpGet, Route("detail/{id}")]
        public ApiModels.MstUser DetailLibraryCards(String id)
        {

            var user = from d in db.MstUsers
                              where d.Id == Convert.ToInt32(id)
                              select new ApiModels.MstUser
                              {
                                  Id = d.Id,
                                  FirstName = d.FirstName,
                                  LastName = d.LastName,
                                  Password = d.Password,
                                  UserTypeId = d.UserTypeId,
                                  AspNetUserId = d.AspNetUserId,
                                  UserName = d.UserName,
                                  Email = d.Email

                              };

            return user.FirstOrDefault();
        }

        [HttpPost, Route("add")]
        public HttpResponseMessage AddUser(ApiModels.MstUser objUser)
        {
            try
            {
                Data.MstUser newUser = new Data.MstUser
                {

                    Id = objUser.Id,
                    FirstName = objUser.FirstName,
                    LastName = objUser.LastName,
                    Password = objUser.Password,
                    UserTypeId = objUser.UserTypeId,
                    AspNetUserId = objUser.AspNetUserId,
                    UserName = objUser.UserName,
                    Email = objUser.Email
                };
                db.MstUsers.InsertOnSubmit(newUser);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut, Route("update/{id}")]
        public HttpResponseMessage UpdateUser(ApiModels.MstUser objUser, String Id)
        {
            try
            {
                var User = from d in db.MstUsers
                                where d.Id == Convert.ToInt32(Id)
                                select d;

                if (User.Any())
                {
                    var updateUser = User.FirstOrDefault();
                    updateUser.Id = objUser.Id;
                    updateUser.FirstName = objUser.FirstName;
                    updateUser.LastName = objUser.LastName;
                    updateUser.Password = objUser.Password;
                    updateUser.UserTypeId = objUser.UserTypeId;
                    updateUser.AspNetUserId = objUser.AspNetUserId;
                    updateUser.UserName = objUser.UserName;
                    updateUser.Email = objUser.Email;


                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "User not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage DeleteUser(String Id)
        {
            try
            {
                var User = from d in db.MstUsers
                                where d.Id == Convert.ToInt32(Id)
                                select d;

                if (User.Any())
                {
                    db.MstUsers.DeleteOnSubmit(User.FirstOrDefault());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User not found!");
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}



