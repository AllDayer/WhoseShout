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
    public class FriendItemController : TableController<Friend>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Friend>(context, Request, enableSoftDelete: true);
        }

        // GET tables/FriendItem
        public IQueryable<Friend> GetAllFriendItems()
        {
            return Query();
        }

        // GET tables/FriendItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Friend> GetFriendItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/FriendItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Friend> PatchFriendItem(string id, Delta<Friend> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/FriendItem
        public async Task<IHttpActionResult> PostFriendItem(Friend item)
        {
            Friend current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/FriendItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFriendItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}