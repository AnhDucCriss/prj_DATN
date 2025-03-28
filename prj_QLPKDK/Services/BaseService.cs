using System.Security.Claims;
using prj_QLPKDK.Data;
using prj_QLPKDK.Entities.BaseEntities;
using prj_QLPKDK.Services.Abstraction;

namespace prj_QLPKDK.Services
{
    public class BaseService
    {
        private readonly IHttpContextAccessor _accessor;
        private WebContext db;

        protected BaseService(IServiceProvider provider)
        {
            _accessor = provider.GetRequiredService<IHttpContextAccessor>();
        }

        public BaseService(WebContext db)
        {
            this.db = db;
        }

        protected virtual void PrepareInsertModel<T>(T entity) where T : BaseEntity
        {
            entity.CreatedBy = GetUser();
            entity.CreatedAt = DateTime.Now;
            entity.LastModifyAt = DateTime.Now;
        }

        protected virtual void PrepareReplaceModel<T>(T entity) where T : BaseEntity
        {
            entity.LastModifyBy = GetUser();
            entity.LastModifyAt = DateTime.Now;
        }

        protected virtual string GetUser()
        {
            try
            {
                return _accessor.HttpContext?.User.FindFirstValue("preferred_username") ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
