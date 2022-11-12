namespace Core.Domain.Base
{
    public abstract class EntityBase
    {
        public long Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        protected EntityBase()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
