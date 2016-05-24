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
    public class FriendRequestController : TableController<FriendRequest>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<FriendRequest>(context, Request, enableSoftDelete: true);
        }

        // GET tables/FriendRequest
        public IQueryable<FriendRequest> GetAllFriendRequests()
        {
            return Query();
        }

        // GET tables/FriendRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<FriendRequest> GetFriendRequest(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/FriendRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<FriendRequest> PatchFriendRequest(string id, Delta<FriendRequest> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/FriendRequest
        public async Task<IHttpActionResult> PostUserItem(FriendRequest item)
        {
            FriendRequest current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/FriendRequest/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFriendRequest(string id)
        {
            return DeleteAsync(id);
        }
    }
}