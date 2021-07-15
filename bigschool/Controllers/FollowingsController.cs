using bigschool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bigschool.Controllers
{
    public class FollowingsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Follow(Following follow)
        {
            var UserID = User.Identity.GetUserId();
            if (UserID == null)

                return BadRequest("Please login first!");
            if (UserID == follow.FolloweeId)
                return BadRequest("Can not follow myself!");
            bigschoolContext context = new bigschoolContext();

            Following find = context.Followings.FirstOrDefault(p => p.FollowerId == UserID && p.FolloweeId == follow.FolloweeId);
            if (find != null)   
            {
                context.Followings.Remove(context.Followings.SingleOrDefault(p => p.FollowerId == UserID && p.FolloweeId == follow.FolloweeId));
                context.SaveChanges();
                return Ok("cancel");
            }
            follow.FollowerId = UserID;
            context.Followings.Add(follow);
            context.SaveChanges();

            return Ok();
        }
    }
}
