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
    public class UserProfilesController : ApiController
    {
        private ResumeCreatorEntities db = new ResumeCreatorEntities();

        private UserProfileVM ConvertToViewModel(UserProfile u)
        {
            return new UserProfileVM {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address1 = u.Address1,
                Address2 = u.Address2,
                City = u.City,
                Contact = u.Contact,
                Country = u.Country,
                CreatedOn = u.CreatedOn,
                Email = u.Email,
                LoginID = u.LoginID,
                State = u.State,
                ID = u.ID,
                UpdatedOn = u.UpdatedOn,
                Objective = u.Objective
            };
        }

        private UserProfile ConvertToDBModel(UserProfileVM u)
        {
            return new UserProfile
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address1 = u.Address1,
                Address2 = u.Address2,
                City = u.City,
                Contact = u.Contact,
                Country = u.Country,
                CreatedOn = u.CreatedOn,
                Email = u.Email,
                LoginID = u.LoginID,
                State = u.State,
                ID = u.ID,
                UpdatedOn = u.UpdatedOn,
                Objective = u.Objective
            };
        }
        // GET: api/UserProfiles
        public IQueryable<UserProfileVM> GetUserProfiles()
        {
            var profiles = db.UserProfiles;
            List<UserProfileVM> response = new List<UserProfileVM>();
            foreach (var p in profiles)
            {
                response.Add(ConvertToViewModel(p));
            }
            return response.AsQueryable();
        }

        // GET: api/UserProfiles/5
        [ResponseType(typeof(UserProfileVM))]
        public async Task<IHttpActionResult> GetUserProfile(int id)
        {
            UserProfile userProfile = await db.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(ConvertToViewModel(userProfile));
        }

        // PUT: api/UserProfiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserProfile(int id, UserProfileVM userProfileVM)
        {
            UserProfile userProfile = ConvertToDBModel(userProfileVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userProfile.ID)
            {
                return BadRequest();
            }

            db.Entry(userProfile).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
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
        [ResponseType(typeof(UserProfile))]
        public async Task<IHttpActionResult> PostUserProfile(UserProfileVM userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserProfiles.Add(ConvertToDBModel(userProfile));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userProfile.ID }, userProfile);
        }

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(UserProfileVM))]
        public async Task<IHttpActionResult> DeleteUserProfile(int id)
        {
            UserProfile userProfile = await db.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }

            db.UserProfiles.Remove(userProfile);
            await db.SaveChangesAsync();

            return Ok(ConvertToViewModel(userProfile));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserProfileExists(int id)
        {
            return db.UserProfiles.Count(e => e.ID == id) > 0;
        }
    }
}