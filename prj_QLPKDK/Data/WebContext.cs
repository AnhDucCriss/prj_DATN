using Microsoft.EntityFrameworkCore;
using prj_QLPKDK.Entities;
using prj_QLPKDK.Entities.BaseEntities;

namespace prj_QLPKDK.Data
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> opt) : base(opt)
        {
            
        }
        #region
        public DbSet<Appointments> Appointments { get; set; } //Lịch hẹn
        public DbSet<Departments> Departments { get; set; } //Khoa/Phòng ban
        public DbSet<Doctors> Doctors { get; set; } //Bác sĩ
        public DbSet<Invoices> Invoices { get; set; } //Hoá đơn
        public DbSet<MedicalRecords> MedicalRecords { get; set; } //Hồ sơ bệnh án
        public DbSet<Patients> Patients { get; set; } //Bệnh nhân
        public DbSet<Rooms> Rooms { get; set; } //Phòng khám
        public DbSet<Prescriptions> Prescriptions { get; set; } //Đơn thuốc
        public DbSet<Staffs> Staffs { get; set; } //Nhân viên

        public DbSet<Users> Users { get; set; } //Tài khoản người dùng

        #endregion

        
        
    }
}
