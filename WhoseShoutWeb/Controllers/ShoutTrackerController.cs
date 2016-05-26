using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using WhoseShoutWeb.Models;
using WhoseShout.Models;

namespace WhoseShoutWeb.Controllers
{
    public class ShoutTrackerController : TableController<ShoutTracker>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            WhoseShoutServiceContext context = new WhoseShoutServiceContext();
            DomainManager = new EntityDomainManager<ShoutTracker>(context, Request, enableSoftDelete: true);
        }

        // GET tables/ShoutTracker
        public IQueryable<ShoutTracker> GetAllShoutTrackerRequests()
        {
            return Query();
        }

        // GET tables/ShoutTracker/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ShoutTracker> GetShoutTrackerRequest(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ShoutTracker/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ShoutTracker> PatchShoutTrackerRequest(string id, Delta<ShoutTracker> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/ShoutTracker
        public async Task<IHttpActionResult> PostUserItem(ShoutTracker item)
        {
            ShoutTracker current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ShoutTracker/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteShoutTrackerRequest(string id)
        {
            return DeleteAsync(id);
        }
    }
}