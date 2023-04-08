//using SCB.Surkova.CreditApprovalSystem.Common;
//using SCB.Surkova.CreditApprovalSystem.Entities;
//using System;
//using static SCB.Surkova.CreditApprovalSystem.Entities.ScanType;
//using static SCB.Surkova.CreditApprovalSystem.Entities.LoanStatus;
//using static SCB.Surkova.CreditApprovalSystem.Entities.Roles;

namespace SCB.Surkova.CreditApprovalSystem.PL
{
    public class Program
    {
        public static void Main()
        {
            //            while (true)
            //            {
            //                Console.WriteLine("Select an action:\n\t1.Sign in\n\t2.Sign up\n\t3.Sign out");
            //                int str = GetNumber("INPUT NUMBER: ");
            //                switch (str)
            //                {
            //                    case 1:
            //                        SignIn();
            //                        break;
            //                    case 2:
            //                        SignUp();
            //                        break;
            //                    case 3:
            //                        return;
            //                    default:
            //                        break;
            //                }
            //            }
            //        }

            //        private static void SignUp()
            //        {
            //            UserView user;
            //            do
            //            {
            //                Console.Write("Username: ");
            //                string username = Console.ReadLine();
            //                Console.Write("Password: ");
            //                string password = Console.ReadLine();
            //                user = null;
            //                try
            //                {
            //                    user = DependencyResolver.UserLogic.GetUserByLoginAndPassword(new UserView { Login = username, Password = password });
            //                }
            //                catch (Exception ex)
            //                {
            //                    Console.WriteLine(ex.Message);
            //                    FogotPassword();
            //                }
            //            } while (user == null);

            //            if (user.Position == Role.Underwriter)
            //            {
            //                FuncForUndewriter(user);
            //            }
            //            else if (user.Position == Role.User)
            //            {
            //                FuncForUser(user);
            //            }
            //            else if (user.Position == Role.Admin)
            //            {
            //                FuncForAdmin();
            //            }
            //            else
            //            {
            //                Console.WriteLine("The user was not found");
            //            }
            //        }

            //        private static void FogotPassword()
            //        {
            //            Console.WriteLine("Forgot your password?\n\t1.Yes\n\t2.No");
            //            int choice = GetNumber("INPUT NUMBER OF POSITION: ");
            //            if (choice == 1)
            //            {
            //                Console.WriteLine("Enter series and number of your passport");
            //                Console.Write("Enter a passport series: ");
            //                string series = Console.ReadLine();
            //                Console.Write("Enter a passport number: ");
            //                string number = Console.ReadLine();
            //                try
            //                {
            //                    Passport passport = DependencyResolver.PassportLogic.GetPassportBySeriesAndNumber(new Passport { Series = series, Number = number });
            //                    UserView user = DependencyResolver.UserLogic.GetUserByPassport(passport);
            //                    UpdatePasswordForUser(user);
            //                }
            //                catch (Exception ex)
            //                {
            //                    Console.WriteLine(ex.Message);
            //                }
            //            }
            //        }

            //        private static void FuncForAdmin()
            //        {
            //            int choice = GetNumber("Select the user ID: ");
            //            Console.WriteLine("Select the role number\n\t1. User\n\t2. Undewriter");
            //            int roleNumber;
            //            do
            //            {
            //                roleNumber = GetNumber("INPUT NUMBER: ");
            //                if (roleNumber == 2)
            //                {
            //                    try
            //                    {
            //                        DependencyResolver.UserLogic.UpdateRole(new UserView { Id = choice }, Role.Underwriter);
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        Console.WriteLine(ex.Message);
            //                    }
            //                }
            //                else if (roleNumber == 1)
            //                {
            //                    try
            //                    {
            //                        DependencyResolver.UserLogic.UpdateRole(new UserView { Id = choice }, Role.User);
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        Console.WriteLine(ex.Message);
            //                    }
            //                }
            //            } while (roleNumber != 1 || roleNumber != 2);
            //        }

            //        private static void FuncForUser(UserView user)
            //        {
            //            while (true)
            //            {
            //                Console.WriteLine("Select an action:\n\t1.View the loan history\n\t2.View profile\n\t3.Update password\n\t4.Add loan\n\t5.Add scans\n\t6.Delete account\n\t7.Exit");
            //                int choice = GetNumber("INPUT NUMBER OF POSITION: ");
            //                switch (choice)
            //                {
            //                    case 1:
            //                        GetLoanHistoryOfUser(user);
            //                        break;
            //                    case 2:
            //                        Console.WriteLine(user);
            //                        break;
            //                    case 3:
            //                        UpdatePasswordForUser(user);
            //                        break;
            //                    case 4:
            //                        AddLoan(user);
            //                        break;
            //                    case 5:
            //                        AddScanFile(user);
            //                        break;
            //                    case 6:
            //                        DeleteUserAccount(user);
            //                        return;
            //                    case 7:
            //                        return;
            //                    default:
            //                        break;
            //                }
            //            }
            //        }

            //        private static void DeleteUserAccount(UserView user)
            //        {
            //            DependencyResolver.UserLogic.DeleteUser(user);
            //        }

            //        private static int GetNumber(string mes)
            //        {
            //            bool flag;
            //            int res;
            //            do
            //            {
            //                Console.Write(mes);
            //                flag = int.TryParse(Console.ReadLine(), out res);
            //                if (flag)
            //                {
            //                    flag = res > 0 ? true : false;
            //                }
            //            } while (!flag);

            //            return res;
            //        }

            //        private static void AddLoan(UserView user)
            //        {
            //            Console.WriteLine("Make sure that you have added a scan of your passport and one of the additional documents (SNILS or driver's license)\n\t1. Yes\n\t2. No (Go to the add page)");
            //            int choice = GetNumber("Enter number of choice: ");
            //            if (choice == 2)
            //            {
            //                AddScanFile(user);
            //            }
            //            else
            //            {
            //                long sum = GetNumber("Enter sum: ");
            //                try
            //                {
            //                    DependencyResolver.LoanLogic.AddLoan(new Loan
            //                    {
            //                        UserId = user.Id,
            //                        Sum = sum,
            //                        DateCreate = DateTime.Now,
            //                        Status = Status.InWaiting,
            //                        AdditionalScanId = user.AdditionalFile == null ? 0 : user.AdditionalFile.Id,
            //                        PassportId = user.Passport.Id
            //                    });
            //                }
            //                catch (Exception ex)
            //                {
            //                    Console.WriteLine(ex.Message);
            //                }
            //            }
            //        }

            //        private static void AddScanFile(UserView user)
            //        {
            //            Console.WriteLine("Select the scan of which document you want to add\n\t1. Passport\n\t2. SNILS\n\t3. Driver's license");
            //            int choice = GetNumber("Enter number: ");
            //            Random rnd = new Random();
            //            try
            //            {
            //                ScanFile file;
            //                switch (choice)
            //                {
            //                    case 1:
            //                        file = new ScanFile
            //                        {
            //                            Title = TypeTitles.Passport,
            //                            Link = new byte[user.Login.Length],
            //                        };

            //                        rnd.NextBytes(file.Link);
            //                        file.Id = DependencyResolver.ScanLogic.AddScan(file);
            //                        DependencyResolver.PassportLogic.AddScan(user.Passport, file);
            //                        user.AdditionalFile = file;
            //                        break;
            //                    case 2:
            //                        file = new ScanFile
            //                        {
            //                            Title = TypeTitles.SNILS,
            //                            Link = new byte[user.Login.Length]
            //                        };

            //                        rnd.NextBytes(file.Link);
            //                        file.Id = DependencyResolver.ScanLogic.AddScan(file);
            //                        DependencyResolver.UserLogic.AddScan(user, file);
            //                        user.AdditionalFile = file;
            //                        break;
            //                    case 3:
            //                        file = new ScanFile
            //                        {
            //                            Title = TypeTitles.DriversLicense,
            //                            Link = new byte[user.Login.Length]
            //                        };

            //                        rnd.NextBytes(file.Link);
            //                        file.Id = DependencyResolver.ScanLogic.AddScan(file);
            //                        DependencyResolver.UserLogic.AddScan(user, file);
            //                        user.AdditionalFile = file;
            //                        break;
            //                    default:
            //                        break;
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(ex.Message);
            //            }
            //        }

            //        private static void UpdatePasswordForUser(UserView user)
            //        {
            //            Console.Write("Enter new password: ");
            //            var newPassword = Console.ReadLine();
            //            try
            //            {
            //                UserView viewUser = new UserView
            //                {
            //                    Id = user.Id,
            //                    FirstName = user.FirstName,
            //                    Surname = user.Surname,
            //                    Patronymic = user.Patronymic,
            //                    Login = user.Login,
            //                    Passport = user.Passport,
            //                    AdditionalFile = user.AdditionalFile,
            //                    Position = user.Position,
            //                    Password = newPassword
            //                };

            //                DependencyResolver.UserLogic.UpdatePassword(viewUser);
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(ex.Message);
            //            }
            //        }

            //        private static void GetLoanHistoryOfUser(UserView user)
            //        {
            //            foreach (var application in DependencyResolver.LoanLogic.GetLoansOfUser(user))
            //            {
            //                Console.WriteLine(application);
            //            }
            //        }

            //        private static void FuncForUndewriter(UserView admin)
            //        {
            //            while (true)
            //            {
            //                Console.WriteLine("Select an action:\n\t1.View the application history\n\t2.View the list of current applications\n\t3.Make a decision on the application\n\t4.View the user's application\n\t5. Exit");
            //                int choice = GetNumber("INPUT NUMBER OF POSITION: ");
            //                switch (choice)
            //                {
            //                    case 1:
            //                        GetHistoryOfApplications();
            //                        break;
            //                    case 2:
            //                        GetCurrentApplications();
            //                        break;
            //                    case 3:
            //                        UpdateStatus();
            //                        break;
            //                    case 4:
            //                        GetApplicationsOfUser();
            //                        break;
            //                    case 5:
            //                        return;
            //                    default:
            //                        break;
            //                }
            //            }
            //        }

            //        private static void UpdateStatus()
            //        {
            //            Console.WriteLine("Enter the series and number of the user's passport, the amount of the application and the date of registration");
            //            Console.Write("Enter series: ");
            //            string series = Console.ReadLine();
            //            Console.Write("Enter number: ");
            //            string number = Console.ReadLine();
            //            try
            //            {
            //                Passport passport = DependencyResolver.PassportLogic.GetPassportBySeriesAndNumber(new Passport { Series = series, Number = number });
            //                UserView user = DependencyResolver.UserLogic.GetUserByPassport(passport);

            //                long sum = GetNumber("Enter sum: ");
            //                DateTime date = GetDate();
            //                var loan = DependencyResolver.LoanLogic.GetLoanById(new Loan { UserId = user.Id, Sum = sum, DateCreate = date });
            //                int status = GetNumber("Set the status (1-Approved, 2-Denied): ");

            //                if (status == 1)
            //                {
            //                    loan.Status = Status.Approved;
            //                    DependencyResolver.LoanLogic.UpdateStatus(loan);
            //                }
            //                else if (status == 2)
            //                {
            //                    loan.Status = Status.Denied;
            //                    DependencyResolver.LoanLogic.UpdateStatus(loan);
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(ex.Message);
            //            }
            //        }

            //        private static DateTime GetDate()
            //        {
            //            bool flag;
            //            DateTime date;
            //            do
            //            {
            //                Console.Write("Enter date (in format dd.mm.yyyy): ");
            //                flag = DateTime.TryParse(Console.ReadLine(), out date);
            //            } while (!flag);

            //            return date;
            //        }

            //        private static void GetApplicationsOfUser()
            //        {
            //            Console.WriteLine("Enter the passport series and number: ");
            //            Console.Write("Enter series: ");
            //            string series = Console.ReadLine();
            //            Console.Write("Enter number: ");
            //            string number = Console.ReadLine();
            //            try
            //            {
            //                Passport passport = DependencyResolver.PassportLogic.GetPassportBySeriesAndNumber(new Passport { Series = series, Number = number });
            //                UserView user = DependencyResolver.UserLogic.GetUserByPassport(passport);
            //                var applications = DependencyResolver.LoanLogic.GetLoansOfUser(user);
            //                foreach (var item in applications)
            //                {
            //                    Console.WriteLine(item);
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(ex.Message);
            //            }
            //        }

            //        private static void GetCurrentApplications()
            //        {
            //            foreach (var application in DependencyResolver.LoanLogic.GetCurrentLoans())
            //            {
            //                Console.WriteLine(application);
            //            }
            //        }

            //        private static void GetHistoryOfApplications()
            //        {
            //            foreach (var application in DependencyResolver.LoanLogic.GetHistoryOfLoans())
            //            {
            //                Console.WriteLine(application);
            //            }
            //        }

            //        private static void SignIn()
            //        {
            //            Console.Write("Enter a name: ");
            //            string name = Console.ReadLine();
            //            Console.Write("Enter a surname: ");
            //            string surname = Console.ReadLine();
            //            Console.Write("Enter a patronic: ");
            //            string patronic = Console.ReadLine();

            //            Console.Write("Enter a passport series: ");
            //            string series = Console.ReadLine();
            //            Console.Write("Enter a passport number: ");
            //            string number = Console.ReadLine();

            //            Console.Write("Enter a login: ");
            //            string login = Console.ReadLine();
            //            Console.Write("Enter a password: ");
            //            string password = Console.ReadLine();

            //            try
            //            {
            //                DependencyResolver.UserLogic.AddUser(new UserView { FirstName = name, Surname = surname, Patronymic = patronic, Passport = new Passport { Series = series, Number = number }, Login = login, Password = password });
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(ex.Message);
            //            }
        }
    }
}
