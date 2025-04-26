using System.Linq.Expressions;
using prj_QLPKDK.Entities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace prj_QLPKDK.Models.FilterResquest
{
    public class StaffFilterResquest : EmptyFilterRequest<Staffs>
    {
        public string name { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;
        public override async Task<Expression<Func<UploadDetail, bool>>> ToExpression(IServiceProvider provider)
        {
            var filter = await base.ToExpression(provider);
            if (!string.IsNullOrEmpty(Date))
            {
                filter = filter.And(x => x.CreatedDate.ToString().Contains(Date));
            }
            if (!string.IsNullOrEmpty(ContractNumber))
            {
                filter = filter.And(x => x.ContractNumber == ContractNumber);
            }
            if (!string.IsNullOrEmpty(Username))
            {
                filter = filter.And(x => x.Username == Username);
            }
            if (OverdueDay.ToString() != "01/01/0001 00:00:00")
            {
                filter = filter.And(x => x.OverdueDays == OverdueDay);
            }
            if (!string.IsNullOrEmpty(Text))
            {
                var text = Text.ToLower().RemoveDiacritics();
                filter = filter.And(x => x.Text.ToLower().Contains(text));
            }
            if (AmlContract.HasValue)
            {
                filter = filter.And(x => x.AmlContract == AmlContract.Value);
            }

            return filter;
        }
    }
}
     
