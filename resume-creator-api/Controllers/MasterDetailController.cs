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
    public class MasterDetailController : ApiController
    {
        private ResumeCreatorEntities db = new ResumeCreatorEntities();

        private MasterDetailVM ConvertToViewModel(MasterDetail md)
        {
            return new MasterDetailVM
            {
                ID = md.ID,
                DisplayText = md.DisplayText,
                DisplayOrder = md.DisplayOrder,
                MasterType = md.MasterType,
                IsDeleted = md.IsDeleted
            };
        }

        private MasterDetail ConvertToDBModel(MasterDetailVM md)
        {
            return new MasterDetail
            {
                ID = md.ID,
                DisplayText = md.DisplayText,
                DisplayOrder = md.DisplayOrder,
                MasterType = md.MasterType,
                IsDeleted = md.IsDeleted
            };
        }
        // GET: api/UserProfiles
        public IQueryable<MasterDetailVM> GetMasterDetail()
        {
            var masterDetail = db.MasterDetails;
            List<MasterDetailVM> response = new List<MasterDetailVM>();
            foreach (var p in masterDetail)
            {
                response.Add(ConvertToViewModel(p));
            }
            return response.AsQueryable();
        }

        // GET: api/UserProfiles/5
        [ResponseType(typeof(MasterDetailVM))]
        public async Task<IHttpActionResult> GetMasterDetail(int id)
        {
            MasterDetail masterDetail = await db.MasterDetails.FindAsync(id);
            if (masterDetail == null)
            {
                return NotFound();
            }

            return Ok(ConvertToViewModel(masterDetail));
        }

        // PUT: api/UserProfiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMasterDetail(int id, MasterDetailVM masterDetailVM)
        {
            MasterDetail masterDetail = ConvertToDBModel(masterDetailVM);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != masterDetail.ID)
            {
                return BadRequest();
            }

            db.Entry(masterDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterDetailExists(id))
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
        [ResponseType(typeof(MasterDetail))]
        public async Task<IHttpActionResult> PostMasterDetail(MasterDetailVM masterDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MasterDetails.Add(ConvertToDBModel(masterDetail));
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = masterDetail.ID }, masterDetail);
        }

        // DELETE: api/UserProfiles/5
        [ResponseType(typeof(MasterDetailVM))]
        public async Task<IHttpActionResult> DeleteMasterDetail(int id)
        {
            MasterDetail masterDetail = await db.MasterDetails.FindAsync(id);
            if (masterDetail == null)
            {
                return NotFound();
            }

            db.MasterDetails.Remove(masterDetail);
            await db.SaveChangesAsync();

            return Ok(ConvertToViewModel(masterDetail));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MasterDetailExists(int id)
        {
            return db.MasterDetails.Count(e => e.ID == id) > 0;
        }
    }
}