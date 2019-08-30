using System;
using System.Collections.Generic;

namespace ChainOfResponisibilityBis {
    class Program {
        static void Main(string[] args) {
            Bouncer entranceMinion = new ListMinion();
            Bouncer eliteMinion = new EliteMinion();
            Bouncer lastMinion = new BlackListMinion();
            entranceMinion.SetSuccessor(eliteMinion);
            eliteMinion.SetSuccessor(lastMinion);


            Drunktard drunktard = new Drunktard();
            Console.WriteLine("Welcome!");
            Console.WriteLine("Name?");
            drunktard.Name = Console.ReadLine();
            Console.WriteLine("Pass?");
            drunktard.Pass = Console.ReadLine();
            entranceMinion.Check(drunktard);

        }
    }

    class Drunktard {
        public string Name { get; set; }
        public string Pass { get; set; }
    }

    abstract class Bouncer {
        protected Bouncer successor;

        public void SetSuccessor(Bouncer successor) {
            this.successor = successor;
        }

        public abstract void Check(Drunktard drunktard);
    }

    class ListMinion : Bouncer {

        List<string> TheList = new List<string>();

        public ListMinion() {
            TheList.Add("Pepe");
            TheList.Add("John");
            TheList.Add("Jerry");
        }

        public override void Check(Drunktard drunktard) {
            if (TheList.Contains(drunktard.Name)) {
                this.successor.Check(drunktard);
            }
            else {
                Console.WriteLine($"I'm sorry {drunktard.Name} but you are not on the list.");
            }
        }
    }

    class EliteMinion : Bouncer {

        Dictionary<string, string> PasswordNotebook = new Dictionary<string, string>();

        public EliteMinion() {
            PasswordNotebook.Add("Pepe", "1234");
            PasswordNotebook.Add("John", "123456");
            PasswordNotebook.Add("Jerry", "imajerk");
        }

        public override void Check(Drunktard drunktard) {
            if (PasswordNotebook[drunktard.Name] == drunktard.Pass) {
                this.successor.Check(drunktard);
            }
            else {
                Console.WriteLine($"Wrong password {drunktard.Name}");
            }
        }
    }

    class BlackListMinion : Bouncer {

        List<string> BlackList = new List<string>();

        public BlackListMinion() {
            BlackList.Add("Jerry");
        }

        public override void Check(Drunktard drunktard) {
            if (!BlackList.Contains(drunktard.Name)) {
                this.Enter(drunktard);
            }
            else {
                Console.WriteLine($"FUCK OFF {drunktard.Name}!");
            }
        }

        public void Enter(Drunktard drunktard) {
            Console.WriteLine($"Drunktard {drunktard.Name} has entered");
        }
    }
}
