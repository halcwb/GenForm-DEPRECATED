namespace Informedica.GenForm.Library.DomainModel.Databases
{
    public class EmptyDatabase: Entity<int, EmptyDatabaseDto>
    {
        protected EmptyDatabase() : base(new EmptyDatabaseDto{ IsEmpty = false }) {}

        public EmptyDatabase(EmptyDatabaseDto dto) : base(dto) {}

        public virtual bool IsEmpty { get { return Dto.IsEmpty; } set { Dto.IsEmpty = value; } }

        public override bool IdIsDefault(int id)
        {
            return id == 0;
        }
    }
}