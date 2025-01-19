namespace SharedLibrary.Utils
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) &&
                   new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }
    }
}
