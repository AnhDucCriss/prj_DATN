using prj_QLPKDK.Entities;
using prj_QLPKDK.Models.Resquest;

namespace prj_QLPKDK.Services.Abstraction
{
    public interface IPatientService 
    {
        public Task<List<Patients>> GetAll();
        public Task<Patients> GetById(string id);
        public Task<List<Patients>> GetByName(string name);
        public Task<List<MedicalRecords>> GetListMC(string id);

        public Task<string> Create(PatientResquestModel model);
        public Task<string> Update(string id, PatientResquestModel model);
        public Task<string> Delete(string id);

    }
}
