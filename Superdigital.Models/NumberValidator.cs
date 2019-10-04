namespace Superdigital.Models
{
    public abstract class NumberValidator
    {
        public string Number { get; set; }
        public string Validador { get; set; }
        internal abstract bool IsValid();
    }
}