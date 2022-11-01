using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creolin_Gopal_Easy_Games_Developer_Test.Objects
{
    public class Transaction_Display
    {
        public Transaction_Display(int transactionId, string transactionAmount, string transactionType, string client, string transactionComment)
        {
            TransactionId = transactionId;
            TransactionAmount = transactionAmount;
            TransactionType = transactionType;
            Client = client;
            TransactionComment = transactionComment;
        }

        public int TransactionId { get; set; }
        public string TransactionAmount { get; set; }
        public string TransactionType { get; set; } 
        public string Client { get; set; }
        public string TransactionComment { get; set; }
    }
}
