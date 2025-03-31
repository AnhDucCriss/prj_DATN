using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly WebContext _db;
        public InvoiceService(WebContext db)
        {
            _db = db;
        }

        public async Task<string> Create(Invoices model)
        {
            
            _db.Invoices!.Add(model);
            await _db.SaveChangesAsync();
            return model.Id.ToString();
        }

        public async Task<string> Delete(int id)
        {
            var dellData = _db.Invoices!.SingleOrDefault(x => x.Id == id);
            if (dellData != null)
            {
                _db.Invoices!.Remove(dellData);
                await _db.SaveChangesAsync();
                return "Xoá thành công user có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }

        public async Task<List<Invoices>> GetAll()
        {
            var datas = await _db.Invoices!.ToListAsync();
            return datas;
        }

        public async Task<Invoices> GetById(int id)
        {
            var data = _db.Invoices!.FirstOrDefault(x => x.Id == id);
            return data;
        }

        public async Task<string> Update(int id, Invoices model)
        {
            if (id == model.Id)
            {
                _db.Invoices!.Update(model);
                await _db.SaveChangesAsync();
                return "Cập nhật thành công cho Invoice có ID: " + id;
            }
            else
            {
                return "ID đưa vào không hợp lệ";
            }
        }
    }
}
