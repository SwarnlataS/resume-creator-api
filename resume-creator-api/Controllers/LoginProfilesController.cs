using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using resume_creator_api.edmx;
using resume_creator_api.ViewModels;

namespace resume_creator_api.Controllers
{
    public class LoginProfilesController : ApiController
    {
        private ResumeCreatorEntities db = new ResumeCreatorEntities();

        // GET: api/LoginProfiles
        //public IQueryable<LoginProfile> GetLoginProfiles()
        //{
        //    return db.LoginProfiles;
        //}

        //// GET: api/LoginProfiles/5
        //[ResponseType(typeof(LoginProfile))]
        //public async Task<IHttpActionResult> GetLoginProfile(int id)
        //{
        //    LoginProfile loginProfile = await db.LoginProfiles.FindAsync(id);
        //    if (loginProfile == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(loginProfile);
        //}

        //// PUT: api/LoginProfiles/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutLoginProfile(int id, LoginProfile loginProfile)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != loginProfile.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(loginProfile).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LoginProfileExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/LoginProfiles
        //[ResponseType(typeof(LoginProfile))]
        //public async Task<IHttpActionResult> PostLoginProfile(LoginProfile loginProfile)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.LoginProfiles.Add(loginProfile);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = loginProfile.ID }, loginProfile);
        //}

        //// DELETE: api/LoginProfiles/5
        //[ResponseType(typeof(LoginProfile))]
        //public async Task<IHttpActionResult> DeleteLoginProfile(int id)
        //{
        //    LoginProfile loginProfile = await db.LoginProfiles.FindAsync(id);
        //    if (loginProfile == null)
        //    {
        //        return NotFound();
        //    }

        //    db.LoginProfiles.Remove(loginProfile);
        //    await db.SaveChangesAsync();

        //    return Ok(loginProfile);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool LoginProfileExists(int id)
        //{
        //    return db.LoginProfiles.Count(e => e.ID == id) > 0;
        //}


        private LoginProfileVM ConvertToViewModel(LoginProfile l)
        {
            return new LoginProfileVM
            {
                ID = l.ID,
                Password = l.Password,
                Username = l.Username,
                CreatedOn = l.CreatedOn
            };
        }

        private LoginProfile ConvertToDBModel(LoginProfileVM l)
        {
            return new LoginProfile
            {
                ID = l.ID,
                Password = l.Password,
                Username = l.Username,
                CreatedOn = l.CreatedOn
            };
        }
        // GET: api/UserProfiles
        public IQueryable<LoginProfileVM> GetLoginProfiles()
        {
            var profiles = db.LoginProfiles;
            List<LoginProfileVM> response = new List<LoginProfileVM>();
            foreach (var p in profiles)
            {
                response.Add(ConvertToViewModel(p));
            }
            return response.AsQueryable();
        }

        // GET: api/UserProfiles/5
        [ResponseType(typeof(LoginProfileVM))]
        public async Task<IHttpActionResult> GetLoginProfile(int id)
        {
            LoginProfile loginProfile = await db.LoginProfiles.FindAsync(id);
            if (loginProfile == null)
            {
                return NotFound();
            }

            return Ok(ConvertToViewModel(loginProfile));
        }

        // PUT: api/UserProfiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLoginProfile(int id, LoginProfileVM loginProfileVM)
        {
            LoginProfile loginProfile = ConvertToDBModel(loginProfileVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loginProfile.ID)
            {
                return BadRequest();
            }

            db.Entry(loginProfile).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginProfileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserProfiles
        [ResponseType(typeof(LoginProfile))]
        public async Task<IHttpActionResult> PostLoginProfile(LoginProfileVM loginProfile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.LoginProfiles.Add(ConvertToDBModel(loginProfile));
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = loginProfile.ID }, loginProfile);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(UserProfileVM))]
        public async Task<IHttpActionResult> DeleteLoginProfile(int id)
        {
            LoginProfile loginProfile = await db.LoginProfiles.FindAsync(id);
            if (loginProfile == null)
            {
                return NotFound();
            }

            db.LoginProfiles.Remove(loginProfile);
            await db.SaveChangesAsync();

            return Ok(ConvertToViewModel(loginProfile));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginProfileExists(int id)
        {
            return db.LoginProfiles.Count(e => e.ID == id) > 0;
        }
    }
}