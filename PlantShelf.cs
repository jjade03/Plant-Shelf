using System;
using System.IO;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace PlantInformationProject {
    class PlantShelf {
        static void Main(string[] args) {
            int option = -1;        // Initializes 'option' as an invalid number.
            string optionPrompt = " Enter a valid option (1-2): ";

            string path = @"PlantList.txt";

            // Creates a path and checks if the file already exists. If it doesn't, creates the file and closes it.
            if(!File.Exists(path)) {
                File.Create(path).Close();
            }

            Console.Write("Welcome to your plant shelf! What would you like to do?\n1. View Plants\n2. Add New Plant" +
            "\n\nEnter the number corresponding with your selection: ");

            // Ensures user enters a valid option.
            while(option <= 0 || option > 2) {
                try {
                    option = Convert.ToInt32(Console.ReadLine());
                    if(option <= 0 || option > 2) {
                        Console.Write("Invalid option entered." + optionPrompt);
                    }
                } catch (Exception e) {
                    Console.Write(e.Message + optionPrompt);
                }
            }
            
            switch(option) {
                case 1: 
                    long fileLength = new FileInfo(path).Length;
                    if(fileLength == 0) {
                        Console.WriteLine("\nNo plants currently exist.\n");
                    } else {
                        outputPlantInfo(path);
                    }
                    break;
                case 2: 
                    userPlantInfo(path);
                    break;
            }
        }

        /* Prompts the user to enter their plant's information */
        private static void userPlantInfo(string path) {
            string plantInfo;       // Creates a FileStream object to write to the text file.
            int plantAge = -1;      // Sets the default age as an invalid value.

            /* RECEIVES PLANT INFORMATION [START] */
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
            string checkName = Console.ReadLine().ToLower();
            checkName = YNCheck(checkName, checkNamePrompt);

            if(checkName == "yes") {
                string nicknamePrompt = "Enter Nickname: ";
                string plantNickname = charLimit(nicknamePrompt);

                plantInfo = plantNickname + ",";        // Adds the plant's nickname to the string.
            } else {
                plantInfo = "null" + ",";               // Adds temporary name to the string.
            }
            /* RECEIVES PLANT INFORMATION [END] */
            // Appends user's plant information to the string.
            plantInfo += $"Species:                    {plantSpecies},Age:                        {plantAge},Watering Frequency:         {waterFrequency}," +
            $"Sunlight Requirement:       {sunRequirement},Room Location:              {plantLocation},\n,";
            
            string[] checkInfo = plantInfo.Split(",", StringSplitOptions.RemoveEmptyEntries);   // Splits the string.
            string[] formatInfo = new string[checkInfo.Length];                                 // Holds the contents of the file with uppercases.

            FormatOutput(checkInfo, formatInfo);

            // Prints the formatted information.
            Console.WriteLine();
            foreach(string element in formatInfo) {
                Console.WriteLine(element);
            }

            // Checks if the information entered is correct and writes it to the file.
            string plantInfoPrompt = "Is the plant's information correct? (Enter 'yes' or 'no'): ";
            Console.Write(plantInfoPrompt);
            string confirm = Console.ReadLine().ToLower();
            confirm = YNCheck(confirm, plantInfoPrompt);

            if(confirm == "yes") {
                File.AppendAllText(path, plantInfo);        // Writes information to the file, then closes it.
                Console.WriteLine("Information Saved!");
            } else {
                userPlantInfo(path);
            }
        }

        /* Ensures user enters either 'yes' or 'no'. */
        private static string YNCheck(string check, string checkPrompt) {
            while(check != "yes" && check != "no") {
                Console.Write("Invalid answer given: '" + check + "'. " + checkPrompt);
                check = Console.ReadLine().ToLower();
            }
            return check;
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
            /* Formatting variables */
            // Divides the file based on the parameters provided in 'separators'.
            string fileContent = File.ReadAllText(path);
            string[] dividedInfo = fileContent.Split(",", StringSplitOptions.RemoveEmptyEntries);

            string[] formatInfo = new string[dividedInfo.Length];          // Holds the contents of the file with uppercases.
            
            int numElements = 6;                                           // Max number of variables entered into file on each line.
            int fileRows = File.ReadLines(path).Count() - 1;               // Gets the numbers of lines in the file.
            int countElem = 0;                                             // Holds the current actual element position.
            int countCols = 0;                                      
            string[,] readPlantInfo = new string[numElements, fileRows];   // Formatted version of the file contents.

            /* Output Variables */
            int perRow = Console.WindowWidth/80;                           // Defines the max number of columns relative to the console's width.
            int evenRows = readPlantInfo.GetLength(1)/2;                   // Defines the amount of full rows.
            int y = 0;                                                     // Initializes the starting index.
            int startIndex = 0;                                            // Saves the starting position for 'y'.
            int maxIndex = perRow;                                         // Defines the max index to be printed on a single row in the console.
            int countRows = 0;                                             // Holds the number of rows printed to the console.

            // Increments through each element in the array, making the first character of each element uppercase.
            FormatOutput(dividedInfo, formatInfo);

            // Increments through the array by 8 so that matching variables are inputted into the new array on the same row.
            for(int i = 0; i < numElements; i++) {
                for(int k = countElem; k < formatInfo.Length; k += 7) {
                    readPlantInfo[countElem, countCols] = formatInfo[k];
                    countCols++;
                }
                countElem++;
                countCols = 0;
            }
            
            if(readPlantInfo.GetLength(1) < perRow) {
                maxIndex = readPlantInfo.GetLength(1);      // Reinitializes 'maxIndex' if the length of dimension 1 in the array is '1'.
            }

            // Increments through the array, outputting two columns per row in the console.
            while (readPlantInfo.GetLength(1) > y) {
                for (int x = 0; x < readPlantInfo.GetLength(0); x++) {
                    for (; y < maxIndex; y++) {
                        //Console.WriteLine($"y: {y}, perRow: {perRow}, maxIndex: {maxIndex}, index: {readPlantInfo[x, y]}, length: {readPlantInfo.GetLength(1)}, countRows: {countRows}, evenRows: {evenRows}");
                        Console.Write($"{readPlantInfo[x, y],-60}");
                        // Add a new line when the end of the row is reached in the array.
                        if (maxIndex - 1 == y) {
                            Console.WriteLine("");
                        }
                    }
                    y = startIndex;     // Reinitializes the starting position.
                }
                Console.WriteLine("");

                y += perRow;            // Increments 'y' by the number of columns to be printed on one row.
                startIndex = y;         // Reinitializes the starting position of 'y'.
                countRows++;
                maxIndex = (countRows == evenRows) ? maxIndex += 1 : maxIndex += perRow;
            }
        }

        private static void FormatOutput(string[] dividedInfo, string[] formatInfo) {
            bool spaceCheck = false;                                       // Checks if the current character is a space.

            for(int i = 0; i < dividedInfo.Length; i++) {
                // If the nickname is null, add the species name instead.
                if(dividedInfo[i] == "null") {
                    dividedInfo[i] = "";    // Empties the current index in the array.
                    // Receives the species name from the fixed position.
                    for(int newIndex = 28; newIndex < dividedInfo[i + 1].Length; newIndex++) {
                        dividedInfo[i] += dividedInfo[i + 1][newIndex];
                    }
                }

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
        }
    }
}