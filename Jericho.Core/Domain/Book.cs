namespace Jericho.Core.Domain
{
    public  class Book : LoanedItem
    {
        public virtual string Isbn { get; set; }
        public virtual string Author { get; set; }
    }
}