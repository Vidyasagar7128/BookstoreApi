using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreModel
{
    public class ResetPasswordModel
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
