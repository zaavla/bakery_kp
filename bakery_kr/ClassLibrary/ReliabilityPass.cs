using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public class ReliabilityPass
    {
        public static string Check_Password(string password)
        {
            int ReliabilityCounter = 0;

            Regex capitalltr = new Regex(@"([A-Z])");
            if (capitalltr.IsMatch(password))
            {
                ReliabilityCounter++;
            }
            Regex capitalrltr = new Regex(@"([А-Я])");
            if (capitalrltr.IsMatch(password))
            {
                ReliabilityCounter++;
            }
            Regex uppercaseltr = new Regex(@"([a-z])");
            if (uppercaseltr.IsMatch(password))
            {
                ReliabilityCounter++;
            }
            Regex uppercaserltr = new Regex(@"([а-я])");
            if (uppercaserltr.IsMatch(password))
            {
                ReliabilityCounter++;
            }
            Regex lenghtpas = new Regex(@".{8,}");
            if (lenghtpas.IsMatch(password))
            {
                ReliabilityCounter++;
            }
            Regex number = new Regex(@"([0-9])");
            if (number.IsMatch(password))
            {
                ReliabilityCounter++;
            }
            Regex symbol = new Regex(@"([_@#$%^!*])");
            if (symbol.IsMatch(password))
            {
                ReliabilityCounter++;
            }

            if (ReliabilityCounter < 2)
            {
                return "Уровень надежности пароля: Ненадежный.";
            }
            else if ((ReliabilityCounter < 4) && (ReliabilityCounter > 1))
            {
                return "Уровень надежности пароля: Простой.";
            }
            else if(ReliabilityCounter == 4)
            {
                return "Уровень надежности пароля: Хороший.";
            }
            else
            {
                return "Уровень надежности пароля: Сложный.";
            }
        }
    }
}
