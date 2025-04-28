using System;
using System.IO;
using System.Text;
using System.Threading.Tasks.Dataflow;

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

            Console.Write("Enter Plant's Location: ");
            string plantLocation = Console.ReadLine() ?? "null";

            Console.Write("Does your plant have a nickname? (Enter 'yes' or 'no'): ");
            string checkName = Console.ReadLine() ?? "null";

            if(checkName == "yes") {
                Console.Write("Enter Nickname: ");
                string plantNickname = Console.ReadLine() ?? "null";

                // Adds the plant's nickname to the string.
                plantInfo = plantNickname + ",";
            } 
            /* RECEIVE USER INPUT [END] */
            // Appends user's plant information to the string.
            plantInfo += $"Species:                    {plantSpecies},Age:                        {plantAge},Watering Frequency:         {waterFrequency}," +
            $"Sunlight Requirement:       {sunRequirement},Location:                   {plantLocation},\n,";

            // Write information to the file, then close it.
            File.AppendAllText(path, plantInfo);
        }

        /* Formats and outputs the user's plant information*/
        // TO DO: Simplify and improve efficiency of method
        private static void outputPlantInfo(string path) {
            // Divides the file based on the parameters provided in 'separators'.
            string fileContent = File.ReadAllText(path);
            char[] separators = {','};
            string[] dividedInfo = fileContent.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            string[] formatInfo = new string[dividedInfo.Length];   // Holds the contents of the file with uppercases.
            bool spaceCheck = false;                                // Checks if the current character is a space.

            int numElements = 6;                                    // Max number of variables entered into file on each line.
            int fileRows = File.ReadLines(path).Count() - 1;        // Gets the numbers of lines in the file.
            int countElem = 0;                                      // Holds the current actual element position.
            int countCols = 0;                                      
            string[,] readPlantInfo = new string[numElements, fileRows];   // Formatted version of the file contents.

            // Increments through each element in the array, making the first character of each element uppercase.
            // Incremements through each element in the array.
            for(int i = 0; i < dividedInfo.Length; i++) {
                // Increments through each character of the current element in the array.
                for(int k = 0; k < dividedInfo[i].Length; k++) {
                    if(k == 0 || spaceCheck == true) {
                        // Converts the first letter of each element to be uppercase.
                        formatInfo[i] += char.ToUpper(dividedInfo[i][k]);
                        spaceCheck = false;
                    } else {
                        formatInfo[i] += dividedInfo[i][k];
                    }
                    // Checks if the current position is whitespace and saves the answer as a bool.
                    if(char.IsWhiteSpace(dividedInfo[i][k])) {
                        spaceCheck = true;
                    }
                }
            }

            // Increments through the array by 8 so that matching variables are inputted into the new array on the same row.
            for(int i = 0; i < numElements; i++) {
                for(int k = countElem; k < formatInfo.Length; k += 7) {
                    readPlantInfo[countElem, countCols] = formatInfo[k];
                    countCols++;
                }
                countElem++;
                countCols = 0;
            }
            Console.WriteLine($"rows: {fileRows}, cols: {countCols}");
            // Outputs and further formats the file's contents for the user.
            foreach(string element in readPlantInfo) {
                // Compares the max number of columns to the current.
                if(/*ConsoleSize(countCols) == true*/countCols == fileRows) {
                    Console.WriteLine("");
                    countCols = 0;
                }
                Console.Write($"{element, -60}");
                countCols++;
            }
        }

        /* Finds the consoles current width and height */
        private static void ConsoleSize(int countCols) {
            var width = Console.WindowWidth;
            var height = Console.WindowHeight;
            int buffer = 80;

            //Console.WriteLine($"width: {width}, height: {height}");
            if(buffer * countCols > width) {
                //Console.WriteLine("");
            }
        }
    }
}