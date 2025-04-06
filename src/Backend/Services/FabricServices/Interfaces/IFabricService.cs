using FabricServices.Model;

namespace FabricServices.Interfaces
{
    public interface IFabricService
    {
        public Task<FabricOrder> SendToFabric(List<FabriOrderDetail> details);
    }
}
