using FluentValidation;
using Moq;
using NUnit.Framework;
using SCB.Surkova.CreditApprovalSystem.BLL;
using SCB.Surkova.CreditApprovalSystem.BLL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.DAL.Interfaces;
using SCB.Surkova.CreditApprovalSystem.Entities;
using SCB.Surkova.CreditApprovalSystem.Hash.Interfaces;
using SCB.Surkova.CreditApprovalSystem.HashGenerator;
using SCB.Surkova.CreditApprovalSystem.Validation;
using SCB.Surkova.CreditApprovalSystem.Validation.Inter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCB.Surkova.CreditApprovalSystem.Tests
{
    public class Tests
    {
        private Mock<IUserDAL> _userMock;
        private Mock<IPassportDao> _passportMock;
        private Mock<ILoanDao> _loanMock;
        private Mock<IScanDao> _scanMock;
        private IUserLogic _userLogic;
        private IPassportLogic _passportLogic;
        private ILoanLogic _loanLogic;
        private IScanLogic _scanLogic;
        private ILoanValidator _loanValidator;
        private IScanValidator _scanValidator;
        private IUserValidator _userValidator;
        private IPassportValidator _passportValidator;
        private IHashing _hashing;


        [OneTimeSetUp]
        public void Setup()
        {
            _userMock = new Mock<IUserDAL>();
            _passportMock = new Mock<IPassportDao>();
            _loanMock = new Mock<ILoanDao>();
            _scanMock = new Mock<IScanDao>();
            _hashing = new Hashing();
            _scanValidator = new ScanValidator();
            _userValidator = new UserValidator();
            _passportValidator = new PassportValidator();
            _passportLogic = new PassportLogic(_passportMock.Object, _passportValidator, _scanValidator);
            _userLogic = new UserLogic(_userMock.Object, _passportLogic, _scanLogic, _userValidator, _scanValidator);
            _loanValidator = new LoanValidator(_passportLogic);
            _loanLogic = new LoanLogic(_loanMock.Object, _loanValidator, _userValidator);
            _scanLogic = new ScanLogic(_scanMock.Object, _scanValidator);
        }

        [Test]
        public void GetCurrentLoans_Correct_ListOfLoans()
        {
            _loanMock.Setup(a => a.GetCurrentLoans()).Returns(new List<Loan>
            {
                new Loan{ UserId=1, Sum=10000, AdditionalScanId=1, DateCreate=DateTime.Now, PassportId=1},
                new Loan{ UserId=2, Sum=50000, AdditionalScanId=2, DateCreate=DateTime.Now, PassportId=2},
            });

            var loans = _loanLogic.GetCurrentLoans().ToList();

            Assert.AreEqual(2, loans.Count());
            _loanMock.Verify(a => a.GetCurrentLoans(), Times.Once);
            Assert.IsTrue(loans[0].UserId == 1);
        }

        [TestCase("3243", "3243546")]
        [TestCase("32433", "324346")]
        public void AddUser_Exception_FailPassport(string series, string number)
        {
            Passport passport = new Passport { Id = 1, Series = series, Number = number };

            _passportMock.Setup(p => p.GetPassportBySeriesAndNumber(passport)).Returns(passport);

            User inputValue1 = new User
            {
                FirstName = "Petr",
                Surname = "Petrov",
                Login = "petr21",
                Passport = passport
            };

            Assert.Throws<ValidationException>(() => _userLogic.AddUser(inputValue1));
        }

        [TestCase("Petr", "petrov")]
        [TestCase("petr", "Petrov")]
        [TestCase("Петр", "Petrov")]
        public void AddUser_Exception_FailUser(string firstName, string surname)
        {
            Passport passport = new Passport { Id = 1, Series = "3245", Number = "547543" };

            User inputValue = new User
            {
                FirstName = firstName,
                Surname = surname,
                Login = "petr21",
                Passport = passport
            };

            Assert.Throws<ValidationException>(() => _userLogic.AddUser(inputValue));
        }

        [TestCase("32454", "547543")]
        [TestCase("3254", "5475443")]
        public void AddPassport_Exception_FailPassport(string series, string number)
        {
            Passport inputPassport = new Passport { Series = series, Number = number };

            _passportMock.Setup(p => p.AddPassport(inputPassport));

            Assert.Throws<ValidationException>(() => _passportLogic.AddPassport(inputPassport));
        }

        [Test]
        public void GetUserById_Exception_User()
        {
            Assert.AreEqual(null, _userLogic.GetUserById(int.MinValue));
        }

        [Test]
        public void AddScanForUser_Exception_FailUser()
        {
            Assert.Throws<ArgumentNullException>(() => _userLogic.AddScan(null, new ScanFile { Id = 1 }));
        }

        [Test]
        public void AddScanForUser_Exception_FailScan()
        {
            Assert.Throws<ArgumentNullException>(() => _userLogic.AddScan(new User(), null));
        }

        [Test]
        public void UpdatePassword_Exception_FailUser()
        {
            User user = new User
            {
                Login = "234",
                HashPassword = null
            };
            Assert.Throws<ArgumentNullException>(() => _userLogic.UpdatePassword(null));
            Assert.Throws<ValidationException>(() => _userLogic.UpdatePassword(user));
        }

        [TestCase("", "212")]
        [TestCase("323", "")]
        [TestCase(null, "323")]
        [TestCase("3232", null)]
        public void GetUserByLoginAndPassword_Exception_FailUser(string login, string password)
        {
            Assert.Throws<ArgumentNullException>(() => _userLogic.GetUserByLoginAndPassword(null));

            User userValue = new User
            {
                Login = login,
                HashPassword = _hashing.GetHash(password),
            };

            Assert.Throws<ValidationException>(() => _userLogic.GetUserByLoginAndPassword(userValue));
        }

        [TestCase(null, new byte[1] { 200 })]
        [TestCase(TypeTitles.Passport, new byte[0] { })]
        public void AddScanFor_Exception_FailScanFile(TypeTitles title, byte[] link)
        {
            ScanFile scan = new ScanFile { Title = title, Link = link };

            _scanMock.Setup(s => s.AddScan(scan));

            Assert.Throws<ValidationException>(() => _scanLogic.AddScan(scan));
            Assert.Throws<ArgumentNullException>(() => _scanLogic.AddScan(null));
        }

        [Test]
        public void AddScanForPassport_Exception_FailPassportOFScan()
        {
            Assert.Throws<ArgumentNullException>(() => _passportLogic.AddScan(null, new ScanFile()));
            Assert.Throws<ArgumentNullException>(() => _passportLogic.AddScan(new Passport(), null));
        }

        [Test]
        public void GetPassportBySeriesAndNumber_Correct_Passport()
        {
            Passport addPassport = new Passport { Series = "3232", Number = "234432" };

            _passportMock.Setup(p => p.GetPassportBySeriesAndNumber(addPassport)).Returns(addPassport);
            Passport getPassport = _passportLogic.GetPassportBySeriesAndNumber(addPassport);

            Assert.AreEqual(addPassport, getPassport);
        }

        [TestCase("43435", "322332")]
        [TestCase("4335", "3292332")]
        public void GetPassportBySeriesAndNumber_Exception_Passport(string series, string number)
        {
            Passport addPassport = new Passport { Series = series, Number = number };

            Assert.Throws<ValidationException>(() => _passportLogic.GetPassportBySeriesAndNumber(addPassport));
            Assert.Throws<ArgumentNullException>(() => _passportLogic.GetPassportBySeriesAndNumber(null));
        }

        [Test]
        public void GetPassportById_Exception_Passport()
        {
            Assert.AreEqual(null, _passportLogic.GetPassportById(int.MinValue));
        }

        [Test]
        public void GetPassportById_Correct_Passport()
        {
            Passport addPassport = new Passport { Id = 5 };

            _passportMock.Setup(p => p.GetPassportById(5)).Returns(addPassport);
            var getPassport = _passportLogic.GetPassportById(5);
            Assert.AreEqual(addPassport, getPassport);

            _passportMock.Verify(p => p.GetPassportById(5), Times.Once);
        }

        [Test]
        public void UpdateStatus_Exception_NullLoan()
        {
            Assert.Throws<ArgumentNullException>(() => _loanLogic.UpdateStatus(null));
        }

        [Test]
        public void GetLoan_Exception_NullLoan()
        {
            Assert.AreEqual(null, _loanLogic.GetLoanById(int.MinValue));
        }

        [Test]
        public void GetLoan_Correct_Loan()
        {
            Loan loan = new Loan
            {
                Id = 5,
                Sum = 5000,
                AdditionalScanId = 1,
                PassportId = 3,
                DateCreate = DateTime.Now,
                Status = Status.Denied
            };

            _loanMock.Setup(l => l.GetLoanById(4)).Returns(loan);
            Loan equalLoan = _loanLogic.GetLoanById(4);

            Assert.AreEqual(equalLoan, loan);

            _loanMock.Verify(l => l.GetLoanById(4), Times.Once);
        }

        [TestCase(0, 4, 5)]
        [TestCase(2, 0, 5)]
        [TestCase(1, 4, 0)]
        public void AddLoan_Exception_FailLoan(int userId, int passportId, int additionalScanId)
        {
            Loan loan = new Loan
            {
                Id = 5,
                Sum = 5000,
                AdditionalScanId = additionalScanId,
                PassportId = passportId,
                DateCreate = DateTime.Now,
                UserId = userId,
            };

            Assert.Throws<ArgumentNullException>(() => _loanLogic.AddLoan(null));
            Assert.Throws<ValidationException>(() => _loanLogic.AddLoan(loan));
        }
    }
}