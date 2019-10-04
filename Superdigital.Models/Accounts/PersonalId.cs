namespace Superdigital.Models
{
    public class PersonalId : NumberValidator
    {
        internal override bool IsValid()
        {
            return true;
        }
    }
}