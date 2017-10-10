﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary1.Models
{
    public partial class Bank
    {
        public Bank()
        {
            BankAccount = new HashSet<BankAccount>();
            Customer = new HashSet<Customer>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("BIC")]
        [StringLength(50)]
        public string Bic { get; set; }

        [InverseProperty("Bank")]
        public ICollection<BankAccount> BankAccount { get; set; }
        [InverseProperty("Bank")]
        public ICollection<Customer> Customer { get; set; }
    }
}
