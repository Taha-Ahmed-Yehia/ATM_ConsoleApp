using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_ConsoleApp
{
    public class FakeDB
    {
        private List<Card> cards = new List<Card>();
        private Random random = new Random();
        private string[] fNames = {
            "Isabela", "Shirley", "Ashlyn", "Silas", "Malia", "Clara", "Ralph", "Madeleine", "Roderick", "Davin"
        };
        private string[] lNames =
        {
            "Angel", "Cannon", "Marcel", "Arnold", "Hebert", "Stein", "Hancock", "Powell", "Benson","Oneill", "Harry", "Lane", "Holland", "Eaton"
        };
        private const string chars = "0123456789";
        public FakeDB()
        {
            CreateFakeAccounts(GetRandomName(), GetRandomName(true), GetRandomPinCode());
            CreateFakeAccounts(GetRandomName(), GetRandomName(true), GetRandomPinCode());
            CreateFakeAccounts(GetRandomName(), GetRandomName(true), GetRandomPinCode());
            CreateFakeAccounts(GetRandomName(), GetRandomName(true), GetRandomPinCode());
            foreach (Card card in cards)
            {
                Debug.Print(card.ToString());
            }
        }
        private string GetRandomPinCode()
        {
            return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public Card GetCard(string cardNumber)
        {
            foreach (Card card in cards)
            {
                if (card.cardNumber == cardNumber) return card;
            }
            return null;
        }
        private string GetRandomName(bool lastName = false)
        {
            if (lastName)
            {
                return lNames[random.Next(0, lNames.Length)];
            }
            if (random.NextDouble() >= 0.5)
                return fNames[random.Next(0, fNames.Length)];
            return lNames[random.Next(0, lNames.Length)];
        }
        private void CreateFakeAccounts(string firstName, string lastName, string PinCode)
        {
            cards.Add(new Card()
            {
                balance = random.Next(0, 10000) + random.NextDouble(),
                cardNumber = new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray()),
                pinCode = PinCode,
                firstName = firstName,
                lastName = lastName,
                isValid = true
            });
        }
        public Card CreateNewAccount(string firstName, string lastName, string PinCode)
        {
            Card card = new Card()
            {
                balance = 0,
                cardNumber = new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray()),
                pinCode = PinCode,
                firstName = firstName,
                lastName = lastName,
                isValid = true
            };
            cards.Add(card);
            return card;
        }
    }
}
