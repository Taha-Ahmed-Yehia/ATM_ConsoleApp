using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_ConsoleApp
{
    public class Card
    {
        public string cardNumber { set; get; }
        public string pinCode { set; get; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public double balance { set; get; }
        public bool isValid { set; get; }

        public override string ToString()
        {
            return $"Customer Name: {firstName} {lastName}, Card Number: {cardNumber}, Pin {pinCode}\n";
        }
    }
}
