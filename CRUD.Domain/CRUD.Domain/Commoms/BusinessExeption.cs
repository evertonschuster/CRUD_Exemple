namespace CRUD.Domain.Commoms
{
    public class BusinessExeption : Exception
    {
        public string? Property { get; set; }
        public BusinessExeption(string? property)
        {
            Property = property;
        }

        public BusinessExeption(string? message, string? property) : base(message)
        {
            Property = property;
        }

        public BusinessExeption(string? message, Exception? innerException, string? property) : base(message, innerException)
        {
            Property = property;
        }
    }
}
