using System;
using System.Collections.Generic;

namespace Creolin_Gopal_Easy_Games_Developer_Test.Models
{
    public partial class Client
    {
        public Client()
        {
            TransactionTbls = new HashSet<TransactionTbl>();
        }

        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public string ClientSurname { get; set; } = null!;
        public decimal ClientBalance { get; set; }

        public virtual ICollection<TransactionTbl> TransactionTbls { get; set; }
    }
}
