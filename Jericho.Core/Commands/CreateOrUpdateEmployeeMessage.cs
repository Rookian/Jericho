namespace Jericho.Core.Commands
{
    public class CreateOrUpdateEmployeeMessage : ICommandMessage
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EMail { get; set; }
        public string Infos { get; set; } 
    }
}