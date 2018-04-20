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
    public class EducationDetailController : ApiController
    {

        private ResumeCreatorEntities db = new ResumeCreatorEntities();

        private EducationDetailVM ConvertToViewModel(EducationDetail ed)
        {
            return new EducationDetailVM
            {
                ID = ed.ID,
                LoginID = ed.LoginID,
                Title = ed.Title,
                Specialization = ed.Specialization,
                University = ed.University,
                FromMonth = ed.FromMonth,
                FromYear = ed.FromYear,
                ToMonth = ed.ToMonth,
                ToYear = ed.ToYear,
                IsCurrent = ed.IsCurrent,
                Percentage = ed.Percentage,
                DisplayOrder = ed.DisplayOrder,
                UpdatedOn = ed.UpdatedOn
            };
        }

        private EducationDetail ConvertToDBModel(EducationDetailVM ed)
        {
            return new EducationDetail
            {
                ID = ed.ID,
                LoginID = ed.LoginID,
                Title = ed.Title,
                Specialization = ed.Specialization,
                University = ed.University,
                FromMonth = ed.FromMonth,
                FromYear = ed.FromYear,
                ToMonth = ed.ToMonth,
                ToYear = ed.ToYear,
                IsCurrent = ed.IsCurrent,
                Percentage = ed.Percentage,
                DisplayOrder = ed.DisplayOrder,
                UpdatedOn = ed.UpdatedOn
            };
        }

        // GET api/EducationDetail
        public IEnumerable<EducationDetailVM> GetEducationDetail()
        {
            var educationdetail = db.EducationDetails;
            List<EducationDetailVM> response = new List<EducationDetailVM>();
            foreach (var p in educationdetail)
            {
                response.Add(ConvertToViewModel(p));
            }
            return response.AsQueryable();

        }

        // GET: api/EducationDetail/5
        [ResponseType(typeof(EducationDetailVM))]
        public async Task<IHttpActionResult> GetUserProfile(int id)
        {
            EducationDetail educationdetail = await db.EducationDetails.FindAsync(id);
            if (educationdetail == null)
            {
                return NotFound();
            }

            return Ok(ConvertToViewModel(educationdetail));
        }

        // POST: api/UserProfiles
        [ResponseType(typeof(EducationDetail))]
        public async Task<IHttpActionResult> PostUserProfile(EducationDetailVM EducationDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EducationDetails.Add(ConvertToDBModel(EducationDetail));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = EducationDetail.ID }, EducationDetail);
        }

        // PUT: api/EducationDetail/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEducationDetail(int id, EducationDetailVM EducationDetailVM)
        {
            EducationDetail EducationDetail = ConvertToDBModel(EducationDetailVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != EducationDetail.ID)
            {
                return BadRequest();
            }

            db.Entry(EducationDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EducationDetailExists(id))
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
        [ResponseType(typeof(EducationDetailVM))]
        public async Task<IHttpActionResult> DeleteEducationDetail(int id)
        {
            EducationDetail EducationDetail = await db.EducationDetails.FindAsync(id);
            if (EducationDetail == null)
            {
                return NotFound();
            }

            db.EducationDetails.Remove(EducationDetail);
            await db.SaveChangesAsync();

            return Ok(ConvertToViewModel(EducationDetail));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EducationDetailExists(int id)
        {
            return db.EducationDetails.Count(e => e.ID == id) > 0;
        }
    }
}