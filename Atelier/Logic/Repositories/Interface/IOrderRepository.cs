using Atelier.Logic.Models;

namespace Atelier.Logic.Repositories.Interface
{
    public interface IOrderRepository
    {
        void AddOrder(Product product, string lastname, string name, string surname, int workerId);

        void AddSupplierOrder(SupplierOrder supplierOrder);

        List<ClientOrderHelper> GetOrders();

        void UpdateFittingDate(int clientOrderId, string date);

        void UpdateExecutionDate(int clientOrderId, string date);

        void UpdatePayment(int clientOrderId, int payment);

        List<ClientOrder> GetDebtors();

        List<SupplierOrderHelper> GetSupplierOrders();

        void UpdateSupplierExecutionDate(int supplierOrderId, string date);

        void UpdateSupplierPayment(int supplierOrderId, int payment);
    }
}
