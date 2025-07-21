using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NadinSoft.Model
{
    public class User
    {
        public User() { }

        public User(string name, string nationalCode, string phoneNumber, DateTime? birthDay, string role, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            NationalCode = nationalCode;
            PhoneNumber = phoneNumber;
            BirthDay = birthDay;
            Role = role;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string NationalCode { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime? BirthDay { get; private set; }
        public string Role { get; private set; }
        public string Email { get; private set; }
        public string? Otp { get; private set; } // کد موقت برای بازنشانی رمز عبور
        public DateTime? OtpExpiry { get; private set; } // تاریخ انقضا کد یک بار مصرف
    }
}
