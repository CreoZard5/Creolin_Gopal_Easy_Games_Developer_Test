using System;
using System.Collections.Generic;

namespace Creolin_Gopal_Easy_Games_Developer_Test.Models
{
    public partial class TransactionTbl
    {

        public TransactionTbl(long transactionId, decimal? transactionAmount, short transactionTypeId, int clientId, string? transactionComment)
        {
            TransactionId = transactionId;
            TransactionAmount = transactionAmount;
            TransactionTypeId = transactionTypeId;
            ClientId = clientId;
            TransactionComment = transactionComment;
        }

        public long TransactionId { get; set; }
        public decimal? TransactionAmount { get; set; }
        public short TransactionTypeId { get; set; }
        public int ClientId { get; set; }
        public string? TransactionComment { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual TransactionType TransactionType { get; set; } = null!;
    }
}
