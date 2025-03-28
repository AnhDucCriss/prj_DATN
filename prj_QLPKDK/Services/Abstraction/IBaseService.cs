using prj_QLPKDK.Entities;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IBaseService<T> 
    {
        public Task<List<T>> GetAll();
        public Task<T> GetById(string id);
        public Task<string> Create(T model);
        public Task<string> Update(string id, T model);
        public Task<string> Delete(string id);
    }
}
