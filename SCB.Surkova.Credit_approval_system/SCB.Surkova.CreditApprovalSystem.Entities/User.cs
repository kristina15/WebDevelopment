using System.Collections.Generic;

namespace SCB.Surkova.CreditApprovalSystem.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Login { get; set; }

        public byte[] HashPassword { get; set; }

        public Passport Passport { get; set; }

        public ScanFile AdditionalFile { get; set; }

        public List<string> Roles { get; set; }
    }
}
