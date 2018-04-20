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
    public class CertificateDetailController : ApiController
    {
        private ResumeCreatorEntities db = new ResumeCreatorEntities();

        private CertificateDetailVM ConvertToViewModel(CertificateDetail cd)
        {
            return new CertificateDetailVM
            {
                ID = cd.ID,
                LoginID = cd.LoginID,
                Title = cd.Title,
                Institute = cd.Institute,
                FromYear = cd.FromYear,
                ToYear = cd.ToYear,
                HasNoExpiry = cd.HasNoExpiry,
                DisplayOrder = cd.DisplayOrder,
                UpdatedOn = cd.UpdatedOn
            };
        }

        private CertificateDetail ConvertToDBModel(CertificateDetailVM cd)
        {
            return new CertificateDetail
            {
                ID = cd.ID,
                LoginID = cd.LoginID,
                Title = cd.Title,
                Institute = cd.Institute,
                FromYear = cd.FromYear,
                ToYear = cd.ToYear,
                HasNoExpiry = cd.HasNoExpiry,
                DisplayOrder = cd.DisplayOrder,
                UpdatedOn = cd.UpdatedOn
            };
        }
        // GET: api/UserProfiles
        public IQueryable<CertificateDetailVM> GetCertificateDetails()
        {
            var CertificateDetail = db.CertificateDetails;
            List<CertificateDetailVM> response = new List<CertificateDetailVM>();
            foreach (var p in CertificateDetail)
            {
                response.Add(ConvertToViewModel(p));
            }
            return response.AsQueryable();
        }

        // GET: api/UserProfiles/5
        [ResponseType(typeof(CertificateDetailVM))]
        public async Task<IHttpActionResult> GetCertificateDetails(int id)
        {
            CertificateDetail certificateDetail = await db.CertificateDetails.FindAsync(id);
            if (certificateDetail == null)
            {
                return NotFound();
            }

            return Ok(ConvertToViewModel(certificateDetail));
        }

        // PUT: api/UserProfiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCertificateDetail(int id, CertificateDetailVM CertificateDetailVM)
        {
            CertificateDetail CertificateDetail = ConvertToDBModel(CertificateDetailVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != CertificateDetail.ID)
            {
                return BadRequest();
            }

            db.Entry(CertificateDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificateDetailExists(id))
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
        [ResponseType(typeof(CertificateDetail))]
        public async Task<IHttpActionResult> PostCertificateDetail(CertificateDetailVM CertificateDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CertificateDetails.Add(ConvertToDBModel(CertificateDetail));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = CertificateDetail.ID }, CertificateDetail);
        }

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(UserProfileVM))]
        public async Task<IHttpActionResult> DeleteCertificateDetail(int id)
        {
            CertificateDetail CertificateDetail = await db.CertificateDetails.FindAsync(id);
            if (CertificateDetail == null)
            {
                return NotFound();
            }

            db.CertificateDetails.Remove(CertificateDetail);
            await db.SaveChangesAsync();

            return Ok(ConvertToViewModel(CertificateDetail));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CertificateDetailExists(int id)
        {
            return db.UserProfiles.Count(e => e.ID == id) > 0;
        }
    }
}