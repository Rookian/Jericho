namespace Jericho.Core.Domain
{
    public abstract class Entity : IEntity
    {
        public virtual int Id { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;
            
            if (IsPersistentObject())
            {
                return (other != null) && (Id == other.Id);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return IsPersistentObject() ? Id.GetHashCode() : base.GetHashCode();
        }

        private bool IsPersistentObject()
        {
            return (Id != 0);
        }
    }
}