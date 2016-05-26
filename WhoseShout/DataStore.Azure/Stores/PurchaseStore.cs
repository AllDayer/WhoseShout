using WhoseShout.Models;
using WhoseShout.DataStore.Azure.Interfaces;

namespace WhoseShout.DataStore.Azure
{
    public class PurchaseStore : BaseStore<Purchase>, IPurchaseStore
    {
    }
}