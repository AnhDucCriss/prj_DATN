using prj_QLPKDK.Entities.BaseEntities;

namespace prj_QLPKDK.Entities
{
    public class Departments : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
