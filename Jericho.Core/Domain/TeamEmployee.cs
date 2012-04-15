namespace Jericho.Core.Domain
{
    public class TeamEmployee : Entity
    {
        public virtual Employee Employee { get; set; }
        public virtual Team Team { get; set; }
    }
}