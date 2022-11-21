using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_ConsoleApp
{
    class ATM_ConsoleApp
    {
        FakeDB dBSys = new FakeDB();
        public void Run()
        {
        ATM_Start:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            string consoleMessage = "Welcome To F-ATM";
            Console.SetCursorPosition((Console.WindowWidth - consoleMessage.Length) / 2, Console.CursorTop);
            Console.WriteLine(consoleMessage);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please chose your operation...");
            Console.WriteLine(" 1- Use ATM");
            Console.WriteLine(" 2- Create Account");
            Console.WriteLine(" 3- Exit");
            while (true)
            {
                switch (GetKey())
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        goto ATM;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        goto CreateAccount;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        goto ATM_Exit;
                }
            }
        ATM:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            consoleMessage = "Welcome To F-ATM";
            Console.SetCursorPosition((Console.WindowWidth - consoleMessage.Length) / 2, Console.CursorTop);
            Console.WriteLine(consoleMessage);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Please Enter your Card Number: ");
            Console.ForegroundColor = ConsoleColor.White;
            string cardNumber = Console.ReadLine();
            Card card = dBSys.GetCard(cardNumber);
            if (card == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Your card number doesn't exist, do you want to create New Account?");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1- Yes\n2- No");
                while (true){
                    switch (GetKey())
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.Clear();
                            goto CreateAccount;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            goto ATM_Start;
                    }
                }
            }
            int failCount = 3;
        ATM_PinCode:
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Please Enter Pin Code: ");
            Console.ForegroundColor = ConsoleColor.White;
            if (Console.ReadLine() != card.pinCode)
            {
                failCount--;
                if (failCount <= 0 || !card.isValid)
                {
                    card.isValid = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    consoleMessage = "Your card is validated for the rest of the day, due to wrong PIN code attempted 3 times.";
                    Console.SetCursorPosition((Console.WindowWidth - consoleMessage.Length) / 2, Console.CursorTop);
                    Console.WriteLine(consoleMessage);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("1- Ok");
                    Console.ReadKey();
                    goto ATM_Start;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Wrong Pin Code, {failCount} Attempts left");
                goto ATM_PinCode;
            }

        ATM_SuccesLogin: 
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            consoleMessage = $"Hello {card.firstName} {card.lastName}";
            Console.SetCursorPosition((Console.WindowWidth - consoleMessage.Length) / 2, Console.CursorTop);
            Console.WriteLine(consoleMessage);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please chose your operation...");
            Console.WriteLine(" 1- Deposit");
            Console.WriteLine(" 2- Withdraw");
            Console.WriteLine(" 3- Show Balance");
            Console.WriteLine(" 4- Exit");
            while (true) { 
                switch (GetKey())
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        goto ATM_Deposit;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        goto ATM_Withdraw;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        goto ATM_ShowBalance;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        goto ATM_Start;
                }
            }
        ATM_Deposit:
            Console.ForegroundColor = ConsoleColor.Cyan;
            string message = "Please enter the amount you wont to deposit: ";
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                double depositAmount = Math.Abs(double.Parse(Console.ReadLine()));
                Console.WriteLine($"\nOperation success..." +
                    $"\nYour balance before: {card.balance}" +
                    $"\nYour current balance: {card.balance += depositAmount}" +
                    "\n1- Back" +
                    "\n2- Exit");
                while (true)
                {
                    switch (GetKey())
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.Clear();
                            goto ATM_SuccesLogin;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            goto ATM_Exit;
                    }
                }
            }
            catch
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, currentLineCursor - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor - 1);
                goto ATM_Deposit;
            }
        ATM_Withdraw:
            Console.ForegroundColor = ConsoleColor.Cyan;
            message = "Please enter the amount you wont to withdraw: ";
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                double withdrawAmount = Math.Abs(double.Parse(Console.ReadLine()));
                if (withdrawAmount > card.balance)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"No enough money in your balance to withdraw ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{withdrawAmount}.");
                    Console.WriteLine("1- Ok");
                    while (true)
                    {
                        switch (GetKey())
                        {
                            case ConsoleKey.D1:
                            case ConsoleKey.NumPad1:
                                goto ATM_SuccesLogin;
                        }
                    }
                }

                Console.WriteLine($"\nOperation success..." +
                    $"\nYour balance before: {card.balance}" +
                    $"\nYour current balance: {card.balance -= withdrawAmount}" +
                    "\n1- Back" +
                    "\n2- Exit");
                while (true)
                {
                    switch (GetKey())
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.Clear();
                            goto ATM_SuccesLogin;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            goto ATM_Exit;
                    }
                }
            }
            catch
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, currentLineCursor - 1);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor - 1);
                goto ATM_Deposit;
            }
        ATM_ShowBalance:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            consoleMessage = $"Hello {card.firstName} {card.lastName}";
            Console.SetCursorPosition((Console.WindowWidth - consoleMessage.Length) / 2, Console.CursorTop);
            Console.WriteLine(consoleMessage);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Your current balance: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(card.balance.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n1- Back\n2- Exit");
            while (true)
            {
                switch (GetKey())
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        goto ATM_SuccesLogin;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        goto ATM_Exit;
                }
            }

        CreateAccount:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            consoleMessage = "-Create Account-";
            Console.SetCursorPosition((Console.WindowWidth - consoleMessage.Length) / 2, Console.CursorTop);
            Console.WriteLine(consoleMessage);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Are you sure you want to create new account?");
            Console.WriteLine("1- Yes\n2- No");
            bool continueToCreateAccount = false;
            while (!continueToCreateAccount)
            {
                switch (GetKey())
                {
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        goto ATM_Start;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        continueToCreateAccount = true;
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Please Enter First Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            string firstName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Please Enter Last Name: ");
            Console.ForegroundColor = ConsoleColor.White;
            string lastName = Console.ReadLine();
            string pinCode = "";
            while (pinCode.Length < 4 || pinCode.Length > 4)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Please Enter Pin code (Note: Pin code must be 4 digits only and known to you only): ");
                Console.ForegroundColor = ConsoleColor.White;
                try
                {
                    pinCode = Console.ReadLine();
                    int.Parse(pinCode);
                    if(pinCode.Length > 4 || pinCode.Length < 4)
                    {
                        int currentLineCursor = Console.CursorTop;
                        Console.SetCursorPosition(0, currentLineCursor - 1);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, currentLineCursor - 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Pin code Must be 4 digits only.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                catch
                {
                    int currentLineCursor = Console.CursorTop;
                    Console.SetCursorPosition(0, currentLineCursor - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, currentLineCursor - 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Your pin code should be numbers only.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Card newCard = dBSys.CreateNewAccount(firstName, lastName, pinCode);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Your Account Details:\n" +
                $"First Name: {firstName}\n" +
                $"Last Name: {lastName}\n" +
                $"Card Number: {newCard.cardNumber}\n" +
                $"Pin Code: {pinCode}");
            Console.WriteLine("Please make sure to remember your card number and your pin code.\n1- Ok");
            var key = GetKey();
            while (key != ConsoleKey.D1 && key != ConsoleKey.NumPad1)
            {
                key = GetKey();
            }
            Console.Clear();
            goto ATM_Start;

        ATM_Exit:
            Environment.Exit(0);
        }
        private ConsoleKey GetKey()
        {
            ConsoleKey key = ConsoleKey.NoName;
            while (true)
            {
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        return key;
                }
            }
        }
    }
}
