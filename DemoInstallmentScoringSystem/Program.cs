using Installment.Domain;
using Installment.States;

namespace Installment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Register();

            Console.Write("Enter your PassportID: ");
            string id = Console.ReadLine() ?? string.Empty;

            if(!Extensions.IsValidCitizen(id))
            {
                Console.WriteLine("We don't have like this passportID");
                return;
            }

            var task = Extensions.TotalScore(id);

            task.RunSynchronously();

            Console.WriteLine();
            ReadKey();
        }


        static void Register()
        {
            Console.Write("Enter PassportId: ");
            string id = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter FirstName: ");
            string firstName = Console.ReadLine() ?? string.Empty;

            Console.Write("LastName: ");
            string lastName = Console.ReadLine() ?? string.Empty;

            Console.Write("MidName: ");
            string midName = Console.ReadLine() ?? string.Empty;

            Console.Write("Region: ");
            string region = Console.ReadLine() ?? string.Empty;

            Console.Write("BirthDate: ");
            string birthDate = Console.ReadLine() ?? string.Empty;

            DateOnly date;
            DateOnly.TryParse(birthDate, out date);

            CitizenModel model = new()
            {
                PassportId = id,
                FirstName = firstName,
                LastName = lastName,
                MidName = midName,
                Region = region,
                BirthDate = date
            };

            ReadKey();

            Console.Write("IsConvicted - [y/n]: ");
            string convicted = Console.ReadLine() ?? string.Empty;

            Console.Write("IsDivorced - [y/n] : ");
            string divorced = Console.ReadLine() ?? string.Empty;

            FederalInfo info = new()
            {
                PassportId = id,
                IsConvicted = convicted == "y" ? true : false,
                IsDivorced = divorced == "y" ? true : false,
            };

            ReadKey();

            Console.Write("1.Employed\n2.UnEmployed\n3.Retired\nSocial status: ");
            string socialStatus = Console.ReadLine() ?? string.Empty;

            Console.Write("Income: ");
            string income = Console.ReadLine() ?? string.Empty;

            Console.Write("Debt: ");
            string debt = Console.ReadLine() ?? string.Empty;

            Taxpayer taxpayer = new()
            {
                Income = decimal.Parse(income),
                Debt = decimal.Parse(debt),
                PassportId = id,
                SocialStatus = socialStatus == "1" ? SocialStatus.Employed : socialStatus == "2" ?
                    SocialStatus.UnEmployed : socialStatus == "3" ? SocialStatus.Retired : SocialStatus.UnEmployed
            };

            Task.Run(() => Extensions.WriteData(model, taxpayer, info));

            ReadKey();
        }
        static void ReadKey()
        {
            Console.WriteLine("Successfull\nplease enter to continue...");
            Console.ReadKey();

            Console.Clear();

        }

    }
}