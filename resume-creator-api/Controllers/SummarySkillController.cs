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
    public class SummarySkillController : ApiController
    {
        private ResumeCreatorEntities db = new ResumeCreatorEntities();

        private SummarySkillVM ConvertToViewModel(SummarySkill s)
        {
            return new SummarySkillVM
            {
                ID = s.ID,
                LoginID = s.LoginID,
                DisplayText = s.DisplayText,
                DisplayOrder = s.DisplayOrder,
                UpdatedOn = s.UpdatedOn
            };
        }

        private SummarySkill ConvertToDBModel(SummarySkillVM s)
        {
            return new SummarySkill
            {
                ID = s.ID,
                LoginID = s.LoginID,
                DisplayText = s.DisplayText,
                DisplayOrder = s.DisplayOrder,
                UpdatedOn = s.UpdatedOn
            };
        }
        // GET: api/UserProfiles
        public IQueryable<SummarySkillVM> GetProjectDetails()
        {
            var summarySkill = db.SummarySkills;
            List<SummarySkillVM> response = new List<SummarySkillVM>();
            foreach (var s in summarySkill)
            {
                response.Add(ConvertToViewModel(s));
            }
            return response.AsQueryable();
        }

        // GET: api/UserProfiles/5
        [ResponseType(typeof(SummarySkillVM))]
        public async Task<IHttpActionResult> GetSummarySkill(int id)
        {
            SummarySkill summarySkill = await db.SummarySkills.FindAsync(id);
            if (summarySkill == null)
            {
                return NotFound();
            }

            return Ok(ConvertToViewModel(summarySkill));
        }

        // PUT: api/UserProfiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSummarySkill(int id, SummarySkillVM summarySkillVM)
        {
            SummarySkill summarySkill = ConvertToDBModel(summarySkillVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != summarySkill.ID)
            {
                return BadRequest();
            }

            db.Entry(summarySkill).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!summarySkillExists(id))
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
        [ResponseType(typeof(SummarySkill))]
        public async Task<IHttpActionResult> PostSummarySkill(SummarySkillVM summarySkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SummarySkills.Add(ConvertToDBModel(summarySkill));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = summarySkill.ID }, summarySkill);
        }

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(SummarySkillVM))]
        public async Task<IHttpActionResult> DeleteSummarySkill(int id)
        {
            SummarySkill summarySkill = await db.SummarySkills.FindAsync(id);
            if (summarySkill == null)
            {
                return NotFound();
            }

            db.SummarySkills.Remove(summarySkill);
            await db.SaveChangesAsync();

            return Ok(ConvertToViewModel(summarySkill));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool summarySkillExists(int id)
        {
            return db.SummarySkills.Count(e => e.ID == id) > 0;
        }
    }
}