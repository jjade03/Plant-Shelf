using System;
using System.IO;
using System.Text;

namespace PlantInformationProject {
    class PlantShelf {
        static void Main(string[] args) {
            // Creates a path and checks if the file already exists. If it doesn't, creates the file and closes it.
            string path = @"PlantList.txt";
            if(!File.Exists(path)) {
                File.Create(path).Close();
            }
            /*Console.Write("Welcome to your plant shelf! What would you like to do?\n1. View Plants\n2. Add New Plant\n3. View Infirmary\n4. View Graveyard" +
            "\n\nEnter the number corresponding with your selection: ");
            int option = Convert.ToInt32(Console.ReadLine());*/
            
            //userPlantInfo(path);
            outputPlantInfo(path);
        }

        // Future goal: Rather than have user enter any data they want, have them select from provided options unless stated otherwise
        /* Prompts the user to enter their plant's information */
        private static void userPlantInfo(string path) {
            // Creates a FileStream object to write to the text file.
            string plantInfo = "";

            /* RECEIVES USER INPUT [START] */
            Console.Write("Enter Plant Species: ");
            string plantSpecies = Console.ReadLine() ?? "null";

            Console.Write("Enter Plant's Age: ");
            int plantAge = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Watering Frequency: ");
            string waterFrequency = Console.ReadLine() ?? "null";

            Console.Write("Enter Sunlight Requirement: ");
            string sunRequirement = Console.ReadLine() ?? "null";

            Console.Write("Enter Your Region: ");
            string userRegion = Console.ReadLine() ?? "null";

            Console.Write("Enter Plant's Location: ");
            string plantLocation = Console.ReadLine() ?? "null";

            Console.Write("Does your plant have a nickname? (Enter 'yes' or 'no'): ");
            string checkName = Console.ReadLine() ?? "null";

            if(checkName == "yes") {
                Console.Write("Enter Nickname: ");
                string plantNickname = Console.ReadLine() ?? "null";

                // TO DO: If no nickname, make species the heading
                Console.WriteLine("\n" + plantNickname + "'s Information:");
                // Inserts the plants nickname at the end of the file.
                plantInfo = plantNickname + ",";
            } 
            /* RECEIVE USER INPUT [END] */
            // Appends user's plant information to the string.
            plantInfo += plantSpecies + "," + plantAge + "," + waterFrequency + "," + sunRequirement + "," + userRegion + "," + plantLocation + "\n";

            // Write information to the file, then close it.
            File.AppendAllText(path, plantInfo);
        }

        /* Formats and outputs the user's plant information*/
        private static void outputPlantInfo(string path) {
            // Divides the file based on the parameters provided in 'separators'.
            string fileContent = File.ReadAllText(path);
            char[] separators = {',', '\n'};
            string[] dividedInfo = fileContent.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string[] readPlantInfo = new string[dividedInfo.Length];    // Formatted version of the file contents.

            // Incremements through each element in the array.
            for(int i = 0; i < dividedInfo.Length; i++) {
                // Increments through each character of the given element in the array.
                for(int k = 0; k < dividedInfo[i].Length; k++) {
                    if(k == 0) {
                        // Converts the first letter of each element to be uppercase.
                        readPlantInfo[i] += char.ToUpper(dividedInfo[i][k]);
                    } else {
                        readPlantInfo[i] += dividedInfo[i][k];
                    }
                }
            }

            foreach(string element in readPlantInfo) {
                Console.WriteLine(element);
            }
        }
    }
}