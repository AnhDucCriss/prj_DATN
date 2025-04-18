using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IPatientService 
    {
        public Task<List<Patients>> GetAll();
        public Task<Patients> GetById(int id);
        public Task<List<Patients>> GetByName(string name);

        public Task<string> Create(PatientResquestModel model);
        public Task<string> Update(int id, PatientResquestModel model);
        public Task<string> Delete(int id);
    }
}
