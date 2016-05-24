using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using WhoseShout.Models;
using WhoseShoutWeb.Models;

namespace WhoseShoutWeb.Controllers
{
    public class UserItemController : TableController<User>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            WhoseShoutServiceContext context = new WhoseShoutServiceContext();
            DomainManager = new EntityDomainManager<User>(context, Request, enableSoftDelete: true);
        }

        // GET tables/UserItem
        public IQueryable<User> GetAllUserItems()
        {
            return Query();
        }

        // GET tables/UserItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<User> GetUserItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/UserItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<User> PatchUserItem(string id, Delta<User> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/UserItem
        public async Task<IHttpActionResult> PostUserItem(User item)
        {
            User current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/UserItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUserItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}