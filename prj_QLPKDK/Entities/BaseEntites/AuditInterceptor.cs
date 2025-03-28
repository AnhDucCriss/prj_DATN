using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using prj_QLPKDK.Entities.BaseEntities;

namespace prj_QLPKDK.Entities.BaseEntites
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _accessor;

        public AuditInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return result;

            var user = _accessor.HttpContext?.User.FindFirst("UserName")?.Value;

            Console.WriteLine($" AuditInterceptor Running - User: {user}"); // Debug log
            foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = user;
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.LastModifyBy = user;
                    entry.Entity.LastModifyAt = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifyBy = user;
                    entry.Entity.LastModifyAt = DateTime.Now;
                }
            }
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
