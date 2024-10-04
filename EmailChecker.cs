using System.Net.Mail;

namespace ETHShop;

public static class EmailChecker
{
    public static bool IsValidEmail(string email)
    {
        try
        {
            // Використовуємо клас MailAddress для перевірки
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
