using Reail_Shop_Backend.DTOs;
using Reail_Shop_Backend.Models;

namespace Reail_Shop_Backend.Interfaces
{
    public interface IInvoiceService
    {
        Task<CustomerInvoice> CreateInvoiceAsync(CreateInvoiceDto dto);
        //Task<CustomerInvoice?> GetInvoiceByIdAsync(int id);
    }
}
