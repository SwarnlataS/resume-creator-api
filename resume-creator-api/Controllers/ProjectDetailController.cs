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
    public class ProjectDetailController : ApiController
    {
        private ResumeCreatorEntities db = new ResumeCreatorEntities();

        private ProjectDetailVM ConvertToViewModel(ProjectDetail pd)
        {
            return new ProjectDetailVM
            {
                ID = pd.ID,
                LoginID = pd.LoginID,
                Client = pd.Client,
                ProjectTitle = pd.ProjectDetails,
                FromMonth = pd.FromMonth,
                FromYear = pd.FromMonth,
                ToMonth = pd.ToMonth,
                ToYear = pd.ToYear,
                IsCurrent = pd.IsCurrent,
                ProjectLocation = pd.ProjectLocation,
                IsOnsite = pd.IsOnsite,
                EmploymentType = pd.EmploymentType,
                ProjectDetails = pd.ProjectDetails,
                Role = pd.Role,
                RoleDescription = pd.RoleDescription,
                TeamSize = pd.TeamSize,
                SkillsUsed = pd.SkillsUsed,
                DisplayOrder = pd.DisplayOrder,
                UpdatedOn = pd.UpdatedOn
            };
        }

        private ProjectDetail ConvertToDBModel(ProjectDetailVM pd)
        {
            return new ProjectDetail
            {
                ID = pd.ID,
                LoginID = pd.LoginID,
                Client = pd.Client,
                ProjectTitle = pd.ProjectDetails,
                FromMonth = pd.FromMonth,
                FromYear = pd.FromMonth,
                ToMonth = pd.ToMonth,
                ToYear = pd.ToYear,
                IsCurrent = pd.IsCurrent,
                ProjectLocation = pd.ProjectLocation,
                IsOnsite = pd.IsOnsite,
                EmploymentType = pd.EmploymentType,
                ProjectDetails = pd.ProjectDetails,
                Role = pd.Role,
                RoleDescription = pd.RoleDescription,
                TeamSize = pd.TeamSize,
                SkillsUsed = pd.SkillsUsed,
                DisplayOrder = pd.DisplayOrder,
                UpdatedOn = pd.UpdatedOn
            };
        }
        // GET: api/UserProfiles
        public IQueryable<ProjectDetailVM> GetProjectDetails()
        {
            var projectDetail = db.ProjectDetails;
            List<ProjectDetailVM> response = new List<ProjectDetailVM>();
            foreach (var p in projectDetail)
            {
                response.Add(ConvertToViewModel(p));
            }
            return response.AsQueryable();
        }

        // GET: api/UserProfiles/5
        [ResponseType(typeof(ProjectDetailVM))]
        public async Task<IHttpActionResult> GetProjectDetails(int id)
        {
            ProjectDetail projectDetail = await db.ProjectDetails.FindAsync(id);
            if (projectDetail == null)
            {
                return NotFound();
            }

            return Ok(ConvertToViewModel(projectDetail));
        }

        // PUT: api/UserProfiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProjectDetail(int id, ProjectDetailVM projectDetailVM)
        {
            ProjectDetail projectDetail = ConvertToDBModel(projectDetailVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectDetail.ID)
            {
                return BadRequest();
            }

            db.Entry(projectDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectDetailExists(id))
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
        [ResponseType(typeof(ProjectDetail))]
        public async Task<IHttpActionResult> PostProjectDetail(ProjectDetailVM projectDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectDetails.Add(ConvertToDBModel(projectDetail));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = projectDetail.ID }, projectDetail);
        }

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(ProjectDetailVM))]
        public async Task<IHttpActionResult> DeleteProjectDetail(int id)
        {
            ProjectDetail projectDetail = await db.ProjectDetails.FindAsync(id);
            if (projectDetail == null)
            {
                return NotFound();
            }

            db.ProjectDetails.Remove(projectDetail);
            await db.SaveChangesAsync();

            return Ok(ConvertToViewModel(projectDetail));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectDetailExists(int id)
        {
            return db.ProjectDetails.Count(e => e.ID == id) > 0;
        }
    }
}