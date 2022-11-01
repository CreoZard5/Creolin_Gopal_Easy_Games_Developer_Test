using Creolin_Gopal_Easy_Games_Developer_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creolin_Gopal_Easy_Games_Developer_Test.Utility
{

    public class ValidationHndeler
    {
        private static EasyGames_Developer_AssesmentContext CONTEXT = new EasyGames_Developer_AssesmentContext();


        public static long getTransID()
        {
            var ID = CONTEXT.TransactionTbls.OrderByDescending(x => x.TransactionId).Select(x => x).FirstOrDefault();

            if (ID != null)
            {
                return (ID.TransactionId +1);
            }
            else
            {
                return 0;
            }
        }
    }
}
