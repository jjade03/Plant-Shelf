using System;

namespace PlantInformationProject {
    class PlantShelf {
        static void Main(string[] args) {
            Console.Write("Welcome to your plant shelf! What would you like to do?\n1. View Plants\n2. Add New Plant\n3. View Infirmary\n4. View Graveyard" +
            "Enter the number corresponding with your selection: ");
            int option = Convert.ToInt32(Console.ReadLine());
            
            plantInfo(); // Prompts the user to enter their plant's information
        }

        // Future goal: Rather than have user enter any data they want, have them select from provided options unless stated otherwise
        static void plantInfo() {
            Console.Write("Enter Plant Species: ");
            string plantSpecies = Console.ReadLine();

            Console.Write("Enter Plant's Age: ");
            int plantAge = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Watering Frequency: ");
            string waterFrequency = Console.ReadLine();

            Console.Write("Enter Sunlight Requirement: ");
            string sunRequirement = Console.ReadLine();

            Console.Write("Enter Your Region: ");
            string userRegion = Console.ReadLine();

            Console.Write("Enter Plant's Location: ");
            string plantLocation = Console.ReadLine();

            Console.Write("Does your plant have a nickname? (Enter 'yes' or 'no'): ");
            string checkName = Console.ReadLine();

            if(checkName == "yes") {
                Console.Write("Enter Nickname: ");
                string plantNickname = Console.ReadLine();

                // Heading for output. TO DO: If no nickname, make species the heading
                Console.WriteLine("\n" plantNickname + "'s Information:");
            }
            // TO DO: Tidy up output so user inputs are spaced out evenly
            Console.WriteLine("Plant Species: " + plantSpecies + "\nAge: " + plantAge + "\nWatering Frequency: " + waterFrequency
            + "\nSunlight Requirements: " + sunRequirement + "\nRegion: " + userRegion + "\nLocation: " + plantLocation);
        }
    }
}