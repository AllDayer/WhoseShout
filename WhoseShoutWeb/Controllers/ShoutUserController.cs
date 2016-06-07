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
    public class ShoutUserController : TableController<ShoutUser>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            WhoseShoutServiceContext context = new WhoseShoutServiceContext();
            DomainManager = new EntityDomainManager<ShoutUser>(context, Request, enableSoftDelete: true);
        }

        // GET tables/ShoutUser
        public IQueryable<ShoutUser> GetAllShoutUserRequests()
        {
            return Query();
        }

        // GET tables/ShoutUser/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ShoutUser> GetShoutUserRequest(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ShoutUser/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ShoutUser> PatchShoutUserRequest(string id, Delta<ShoutUser> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/ShoutUser
        public async Task<IHttpActionResult> PostUserItem(ShoutUser item)
        {
            ShoutUser current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ShoutUser/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteShoutUserRequest(string id)
        {
            return DeleteAsync(id);
        }
    }
}