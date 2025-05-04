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
            
            userPlantInfo(path);
            //outputPlantInfo(path);
        }

        // Future goal: Rather than have user enter any data they want, have them select from provided options unless stated otherwise
        /* Prompts the user to enter their plant's information */
        private static void userPlantInfo(string path) {
            string plantInfo;       // Creates a FileStream object to write to the text file.
            int plantAge = -1;      // Sets the default age as an invalid value.

            /* RECEIVES USER INPUT [START] */
            string speciesPrompt = "Enter Plant Species: ";
            string plantSpecies = charLimit(speciesPrompt);

            // Checks if the plant's age is a valid input.
            while(plantAge < 0 || plantAge.ToString().Length > 2) {
                try {
                    string agePrompt = "Enter Plant's Age: ";
                    plantAge = Convert.ToInt32(charLimit(agePrompt));
                    if(plantAge < 0 || plantAge.ToString().Length > 2) {
                        Console.WriteLine("Invalid age entered.");
                    }
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }

            string waterPrompt = "Enter Watering Frequency: ";
            string waterFrequency = charLimit(waterPrompt);

            string sunPrompt = "Enter Sunlight Requirement: ";
            string sunRequirement = charLimit(sunPrompt);

            string locationPrompt = "Enter Plant's Location in Room: ";
            string plantLocation = charLimit(locationPrompt);

            string checkNamePrompt = "Does your plant have a nickname? (Enter 'yes' or 'no'): ";
            Console.Write(checkNamePrompt);
            string checkName = Console.ReadLine() ?? "null".ToLower();

            // Ensures user enters either 'yes' or 'no'.
            while(checkName != "yes" && checkName != "no") {
                Console.Write("Invalid answer given: '" + checkName+ "'. " + checkNamePrompt);
                checkName = Console.ReadLine() ?? "null".ToLower();
            }

            if(checkName == "yes") {
                string nicknamePrompt = "Enter Nickname: ";
                string plantNickname = charLimit(nicknamePrompt);

                plantInfo = plantNickname + ",";        // Adds the plant's nickname to the string.
            } else {
                plantInfo = "null" + ",";               // Adds temporary name to the string.
            }
            /* RECEIVE USER INPUT [END] */
            // Appends user's plant information to the string.
            plantInfo += $"Species:                    {plantSpecies},Age:                        {plantAge},Watering Frequency:         {waterFrequency}," +
            $"Sunlight Requirement:       {sunRequirement},Room Location:              {plantLocation},\n,";

            File.AppendAllText(path, plantInfo);        // Writes information to the file, then closes it.
        }

        /* Checks if the input is within the character limit */
        private static string charLimit(string prompt) {
            bool underLimit = false;
            string answer = "";
            // Enforces a character limit and checks if the input is null.
            while(underLimit == false || answer == "null") {
                Console.Write(prompt);
                answer = Console.ReadLine() ?? "null";
                if(answer.Length > 30) {
                    Console.WriteLine("Description exceeds the 30 character limit.");
                    underLimit = false;
                } else if(answer.Length == 0 || answer == "null") {
                    Console.WriteLine("Invalid input entered.");
                } else {
                    underLimit = true;
                }
            }
            return answer;
        }

        /* Formats and outputs the user's plant information*/
        // TO DO: Simplify and improve efficiency of method
        private static void outputPlantInfo(string path) {
            // Divides the file based on the parameters provided in 'separators'.
            string fileContent = File.ReadAllText(path);
            char[] separators = {','};
            string[] dividedInfo = fileContent.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            string[] formatInfo = new string[dividedInfo.Length];          // Holds the contents of the file with uppercases.
            bool spaceCheck = false;                                       // Checks if the current character is a space.

            int numElements = 6;                                           // Max number of variables entered into file on each line.
            int fileRows = File.ReadLines(path).Count() - 1;               // Gets the numbers of lines in the file.
            int countElem = 0;                                             // Holds the current actual element position.
            int countCols = 0;                                      
            string[,] readPlantInfo = new string[numElements, fileRows];   // Formatted version of the file contents.
            //int maxCols = readPlantInfo.GetLength(1);                    // Holds the max number of columns in the array.  


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

            int evenRows = readPlantInfo.GetLength(1)/2;    // Defines the amount of full rows.
            int y = 0;                  // Initializes the starting index.
            int startIndex = 0;         // Saves the starting position for 'y'.
            int perRow = 2;             // Defines the max number of columns.
            int maxIndex = perRow;      // Defines the max index to be printed on a single row in the console.
            int countRows = 0;          // Holds the number of rows printed to the console.

            // Increments through the array, outputting two columns per row in the console.
            while(readPlantInfo.GetLength(1) > y) {
                for(int x = 0; x < readPlantInfo.GetLength(0); x++) {
                    for(; y < maxIndex; y++) {

                        Console.Write($"{readPlantInfo[x, y], -60}");
                        // Add a new line when the end of the row is reached in the array.
                        if(maxIndex - 1 == y) {
                            Console.WriteLine("");
                        }
                    }
                    y = startIndex;     // Reinitializes the starting position.
                }
                Console.WriteLine("");

                y += perRow;            // Increments 'y' by the number of columns to be printed on one row.
                startIndex = y;         // Reinitializes the starting position of 'y'.
                countRows++;
                maxIndex = (countRows == evenRows) ? maxIndex += 1: maxIndex += perRow;
            }
        }

        /* Finds the difference between the max number of columns and the number of columns exceeding one row in the output. */
        /*private static int calcDifference(int size, int difference) {
            int buffer = 80;        // Amount of space to reserve for each column in the output.
            
            // Determines the value of 'difference' .
            for(int i = 1; i <= size; i++) {
                if(buffer * i > Console.WindowWidth) {
                    difference++;
                }
            }

            return difference;
        }*/
    }
}