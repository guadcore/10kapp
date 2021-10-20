using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Client
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
    }
}
