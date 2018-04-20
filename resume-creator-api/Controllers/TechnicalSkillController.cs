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
    public class TechnicalSkillController : ApiController
    {
        private ResumeCreatorEntities db = new ResumeCreatorEntities();

        private TechnicalSkillVM ConvertToViewModel(TechnicalSkill ts)
        {
            return new TechnicalSkillVM
            {
                ID = ts.ID,
                LoginID = ts.LoginID,
                Title = ts.Title,
                Version = ts.Version,
                LastUsed = ts.LastUsed,
                ExperienceYear = ts.ExperienceYear,
                ExperienceMonth = ts.ExperienceMonth,
                ProfileID = ts.ProfileID,
                DisplayOrder = ts.DisplayOrder,
                UpdatedOn = ts.UpdatedOn
            };
        }

        private TechnicalSkill ConvertToDBModel(TechnicalSkillVM ts)
        {
            return new TechnicalSkill
            {
                ID = ts.ID,
                LoginID = ts.LoginID,
                Title = ts.Title,
                Version = ts.Version,
                LastUsed = ts.LastUsed,
                ExperienceYear = ts.ExperienceYear,
                ExperienceMonth = ts.ExperienceMonth,
                ProfileID = ts.ProfileID,
                DisplayOrder = ts.DisplayOrder,
                UpdatedOn = ts.UpdatedOn
            };
        }
        // GET: api/UserProfiles
        public IQueryable<TechnicalSkillVM> GetTechnicalSkill()
        {
            var technicalSkill = db.TechnicalSkills;
            List<TechnicalSkillVM> response = new List<TechnicalSkillVM>();
            foreach (var p in technicalSkill)
            {
                response.Add(ConvertToViewModel(p));
            }
            return response.AsQueryable();
        }

        // GET: api/UserProfiles/5
        [ResponseType(typeof(TechnicalSkillVM))]
        public async Task<IHttpActionResult> GetTechnicalSkill(int id)
        {
            TechnicalSkill technicalSkill = await db.TechnicalSkills.FindAsync(id);
            if (technicalSkill == null)
            {
                return NotFound();
            }

            return Ok(ConvertToViewModel(technicalSkill));
        }

        // PUT: api/UserProfiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTechnicalSkill(int id, TechnicalSkillVM technicalSkillVM)
        {
            TechnicalSkill technicalSkill = ConvertToDBModel(technicalSkillVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != technicalSkill.ID)
            {
                return BadRequest();
            }

            db.Entry(technicalSkill).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TechnicalSkillExists(id))
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
        [ResponseType(typeof(TechnicalSkill))]
        public async Task<IHttpActionResult> PostTechnicalSkill(TechnicalSkillVM technicalSkill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TechnicalSkills.Add(ConvertToDBModel(technicalSkill));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = technicalSkill.ID }, technicalSkill);
        }

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(TechnicalSkillVM))]
        public async Task<IHttpActionResult> DeleteTechnicalSkill(int id)
        {
            TechnicalSkill technicalSkill = await db.TechnicalSkills.FindAsync(id);
            if (technicalSkill == null)
            {
                return NotFound();
            }

            db.TechnicalSkills.Remove(technicalSkill);
            await db.SaveChangesAsync();

            return Ok(ConvertToViewModel(technicalSkill));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TechnicalSkillExists(int id)
        {
            return db.TechnicalSkills.Count(e => e.ID == id) > 0;
        }
    }
}