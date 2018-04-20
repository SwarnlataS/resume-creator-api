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
    public class EmployerHistoryController : ApiController
    {
        private ResumeCreatorEntities db = new ResumeCreatorEntities();

        private EmployerHistoryVM ConvertToViewModel(EmployerHistory eh)
        {
            return new EmployerHistoryVM
            {
                ID = eh.ID,
                LoginID = eh.LoginID,
                EmployerName = eh.EmployerName,
                IsCurrent = eh.IsCurrent,
                FromMonth = eh.FromMonth,
                FromYear = eh.FromYear,
                ToMonth = eh.ToMonth,
                ToYear = eh.ToYear,
                Designation = eh.Designation,
                TeamSize = eh.TeamSize,
                JobProfile = eh.JobProfile,
                NoticePeriod = eh.NoticePeriod,
                DisplayOrder = eh.DisplayOrder,
                UpdatedOn = eh.UpdatedOn
            };
        }

        private EmployerHistory ConvertToDBModel(EmployerHistoryVM eh)
        {
            return new EmployerHistory
            {
                ID = eh.ID,
                LoginID = eh.LoginID,
                EmployerName = eh.EmployerName,
                IsCurrent = eh.IsCurrent,
                FromMonth = eh.FromMonth,
                FromYear = eh.FromYear,
                ToMonth = eh.ToMonth,
                ToYear = eh.ToYear,
                Designation = eh.Designation,
                TeamSize = eh.TeamSize,
                JobProfile = eh.JobProfile,
                NoticePeriod = eh.NoticePeriod,
                DisplayOrder = eh.DisplayOrder,
                UpdatedOn = eh.UpdatedOn
            };
        }

        // GET api/EducationDetail
        public IEnumerable<EmployerHistoryVM> GetEmployerHistory()
        {
            var employerHistory = db.EmployerHistories;
            List<EmployerHistoryVM> response = new List<EmployerHistoryVM>();
            foreach (var p in employerHistory)
            {
                response.Add(ConvertToViewModel(p));
            }
            return response.AsQueryable();

        }

        // GET: api/EducationDetail/5
        [ResponseType(typeof(EmployerHistoryVM))]
        public async Task<IHttpActionResult> GetUserProfile(int id)
        {
            EmployerHistory employerHistory = await db.EmployerHistories.FindAsync(id);
            if (employerHistory == null)
            {
                return NotFound();
            }

            return Ok(ConvertToViewModel(employerHistory));
        }

        // POST: api/UserProfiles
        [ResponseType(typeof(EmployerHistory))]
        public async Task<IHttpActionResult> PostUserProfile(EmployerHistoryVM EmployerHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployerHistories.Add(ConvertToDBModel(EmployerHistory));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = EmployerHistory.ID }, EmployerHistory);
        }

        // PUT: api/EducationDetail/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployerHistory(int id, EmployerHistoryVM EmployerHistoryVM)
        {
            EmployerHistory EmployerHistory = ConvertToDBModel(EmployerHistoryVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != EmployerHistory.ID)
            {
                return BadRequest();
            }

            db.Entry(EmployerHistory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployerHistoryExists(id))
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

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(EmployerHistoryVM))]
        public async Task<IHttpActionResult> DeleteEmployerHistory(int id)
        {
            EmployerHistory EmployerHistory = await db.EmployerHistories.FindAsync(id);
            if (EmployerHistory == null)
            {
                return NotFound();
            }

            db.EmployerHistories.Remove(EmployerHistory);
            await db.SaveChangesAsync();

            return Ok(ConvertToViewModel(EmployerHistory));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployerHistoryExists(int id)
        {
            return db.EmployerHistories.Count(e => e.ID == id) > 0;
        }
    }
}