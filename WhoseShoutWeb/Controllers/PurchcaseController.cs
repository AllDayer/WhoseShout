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
    public class PurchaseController : TableController<Purchase>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            WhoseShoutServiceContext context = new WhoseShoutServiceContext();
            DomainManager = new EntityDomainManager<Purchase>(context, Request, enableSoftDelete: true);
        }

        // GET tables/Purchase
        public IQueryable<Purchase> GetAllPurchaseRequests()
        {
            return Query();
        }

        // GET tables/Purchase/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Purchase> GetPurchase(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Purchase/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Purchase> PatchPurchase(string id, Delta<Purchase> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Purchase
        public async Task<IHttpActionResult> PostUserItem(Purchase item)
        {
            Purchase current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Purchase/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFriendRequest(string id)
        {
            return DeleteAsync(id);
        }
    }
}