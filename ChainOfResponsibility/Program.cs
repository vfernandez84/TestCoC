using System;

namespace ChainOfResponsibility {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Scenario 1: Copper cable");
            Console.WriteLine("----------------------------------------");

            Processor furnace1 = new Furnace();
            Processor copperCableFactory1 = new CopperCableFactory();
            Processor storehouse = new Storehouse();

            furnace1.SetSuccessor(copperCableFactory1);
            copperCableFactory1.SetSuccessor(storehouse);

            Console.WriteLine("Input: Copper ore");
            Material input1 = new Material("Copper ore");
            furnace1.ProcessMaterial(input1);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Scenario 2: Iron gear");
            Console.WriteLine("----------------------------------------");

            Processor electricFurnace1 = new ElectricFurnace();
            Processor ironGearFactory1 = new IronGearFactory();

            electricFurnace1.SetSuccessor(ironGearFactory1);
            ironGearFactory1.SetSuccessor(storehouse);

            Console.WriteLine("Input: Iron ore");
            Material input2 = new Material("Iron ore");
            electricFurnace1.ProcessMaterial(input2);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Scenario 3: Refining factory");
            Console.WriteLine("----------------------------------------");

            furnace1.SetSuccessor(electricFurnace1);
            electricFurnace1.SetSuccessor(storehouse);

            Console.WriteLine("Input: Iron ore");
            Material input3 = new Material("Iron ore");
            furnace1.ProcessMaterial(input3);
            Console.WriteLine("Input: Copper ore");
            Material input4 = new Material("Copper ore");
            furnace1.ProcessMaterial(input4);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Scenario 4: Universal Factory");
            Console.WriteLine("----------------------------------------");

            electricFurnace1.SetSuccessor(copperCableFactory1);
            copperCableFactory1.SetSuccessor(ironGearFactory1);
            ironGearFactory1.SetSuccessor(storehouse);

            Console.WriteLine("Input: Iron ore");
            Material input5 = new Material("Iron ore");
            electricFurnace1.ProcessMaterial(input5);
            Console.WriteLine("Input: Copper ore");
            Material input6 = new Material("Copper ore");
            electricFurnace1.ProcessMaterial(input6);



        }
    }

    class Material {
        public string Name { get; set; }
        public int Price { get; set; } = 0;

        public Material (string name) {
            this.Name = name;
        }
    }

    abstract class Processor {
        protected Processor successor;

        public void SetSuccessor(Processor successor) {
            this.successor = successor;
        }

        public abstract void ProcessMaterial(Material material);
    }

    class Furnace : Processor {
        public override void ProcessMaterial(Material material) {
            if (material.Name == "Copper ore") {
                Console.WriteLine("Furnace working");
                material.Name = "Copper ingot";
                material.Price += 500; 
            }

            if (successor != null) {
                successor.ProcessMaterial(material);
            }
        }
    }

    class ElectricFurnace : Processor {
        public override void ProcessMaterial(Material material) {
            if (material.Name == "Copper ore") {
                Console.WriteLine("ElectricFurnace working");
                material.Name = "Copper ingot";
                material.Price += 1000;
            }
            else if (material.Name == "Iron ore") {
                Console.WriteLine("ElectricFurnace working");
                material.Name = "Iron ingot";
                material.Price += 1000;
            }

            if (successor != null) {
                successor.ProcessMaterial(material);
            }
        }

    }

    class IronGearFactory : Processor {
        public override void ProcessMaterial(Material material) {
            if (material.Name == "Iron ingot") {
                Console.WriteLine("IronGearFactory working");
                material.Name = "Iron gear";
                material.Price += 2000;
            }

            if (successor != null) {
                successor.ProcessMaterial(material);
            }
        }
    }

    class CopperCableFactory : Processor {
        public override void ProcessMaterial(Material material) {
            if (material.Name == "Copper ingot") {
                Console.WriteLine("CopperCableFactory working");
                material.Name = "Copper cable";
                material.Price += 1500;
            }

            if (successor != null) {
                successor.ProcessMaterial(material);
            }
        }
    }

    class Storehouse : Processor {
        public override void ProcessMaterial(Material material) {
            Console.WriteLine($"New product finished: 1 unit of {material.Name} for {material.Price}$");
        }
    }


}
