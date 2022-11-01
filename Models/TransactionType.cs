using System;
using System.Collections.Generic;

namespace Creolin_Gopal_Easy_Games_Developer_Test.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            TransactionTbls = new HashSet<TransactionTbl>();
        }

        public short TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; } = null!;

        public virtual ICollection<TransactionTbl> TransactionTbls { get; set; }
    }
}
