using System.Runtime.CompilerServices;

class EditPlants
{
    public void EditPlantInfo(string path, int plantIndex, ViewPlants viewObj)
    {
        // Formats the line to be edited.
        string fileLine = File.ReadLines(path).Skip(plantIndex).Take(1).First();    // The file line being edited.
        string[] dividedInfo = fileLine.Split(',', StringSplitOptions.RemoveEmptyEntries);

        string[] plantInfo = viewObj.FormatOutput(dividedInfo);                     // The formatted information.
        string[,] printPlantInfo = new string[plantInfo.Length, 2];                 // Declares a 2D array to hold the new and old plant information.

        // Creates objects for referenced classes.
        AddPlants addObj = new();
        Navigation navObj = new();

        string confirmMsg = "> Enter 'yes' or 'no' to continue: ";
        string welcomeMsg = "What would you like to do?\n1. View Plants\n2. Add New Plant\n3. Edit Plant\n4. Exit" +
    "\n\n> Enter the number corresponding with your selection: ";
        string newConfirm = "";

        // Prompts user to confirm their selection.
        Console.WriteLine("\n\nIs this the plant you wish to edit?");
        foreach (string element in plantInfo)
        {
            Console.WriteLine(element);
        }
        Console.Write(confirmMsg);
        string confirm = Console.ReadLine().ToLower();
        confirm = addObj.YNCheck(confirm, confirmMsg);      // Ensures valid answer is entered.

        // If the information is incorrect, repeat the loop.
        if (confirm == "no")
        {
            navObj.MenuLoop(3, path, welcomeMsg);
        }

        // Loops through the appropriate redirections until the user is satisfied with their result.
        while (confirm == "yes" || newConfirm == "yes")
        {
            printPlantInfo = ReenterInfo(plantInfo, printPlantInfo);

            Console.Write("\nIs this information correct?\n" + confirmMsg);
            newConfirm = Console.ReadLine().ToLower();
            newConfirm = addObj.YNCheck(newConfirm, confirmMsg);      // Ensures valid answer is entered.

            if (newConfirm != "yes")
            {
                EditPlantInfo(path, plantIndex, viewObj);             // Returns to start of method.
            }
            else if (confirm == "yes" || newConfirm == "yes")
            {
                EditLine(path, plantIndex, printPlantInfo);           // Writes the line to the file.
                break;
            }
        }
    }

    /* Returns the new and old plant information in a formatted 2D array */
    private static string[,] ReenterInfo(string[] plantInfo, string[,] printPlantInfo)
    {
        // Creates objects for referenced classes.
        AddPlants addObj = new();
        ViewPlants viewObj = new();

        string[] newPlantInfo = new string[12];   // Declare new array to hold final values.
        int usedIndxs = 0;                        // Initialize the actual index to be incremented on.

        // Adds the original information into the new array.
        for (; usedIndxs < plantInfo.Length; usedIndxs++)
        {
            newPlantInfo[usedIndxs] = plantInfo[usedIndxs];
        }

        // Prompts user to re-enter plant information with a side-by-side of the old information.
        for (int i = 0; i < plantInfo.Length; i++)
        {
            // Inserts tag for first index.
            if (i == 0)
            {
                newPlantInfo[usedIndxs] = "Name:                       ";
                Console.WriteLine("\nPress 'enter' to keep the original value, otherwise enter the desired information:");
            }

            newPlantInfo[usedIndxs] += plantInfo[i];

            string newVal = addObj.CharLimit(newPlantInfo[usedIndxs] + " --> ", true);
            if (newVal == "")
            {
                newPlantInfo[usedIndxs] = plantInfo[i];     // Inserts original value if no changes.
            }
            else
            {
                // Adds the new value and the tag corresponding with it.
                newPlantInfo[usedIndxs] = addObj.msgPrompt[i];
                newPlantInfo[usedIndxs] += newVal;
            }
            usedIndxs++;
        }
        // Reformats the information and adds it into a 2D array for clarity.
        newPlantInfo = viewObj.FormatOutput(newPlantInfo);
        printPlantInfo = viewObj.ReindexArray(printPlantInfo, plantInfo.Length, newPlantInfo, plantInfo.Length);
        // Prints a side-by-side comparison.
        Console.WriteLine($"\n{"OLD:",20}{"-->",30}{"NEW:",30}\n");
        viewObj.OutputPerRow(2, printPlantInfo, 2, false);

        return printPlantInfo;
    }

    /* Removes the old information and inserts the new information in it's place */
    private static void EditLine(string path, int plantIndex, string[,] printPlantInfo)
    {
        string line = null;     // Holds the contents of the current line.
        int currentLine = -1;

        // Reads the contents from the file.
        using (StreamReader reader = new StreamReader(path))
        {
            using (StreamWriter writer = new StreamWriter("EditedPlantList.txt"))
            {
                // Removes the appropriate line from the file.
                while ((line = reader.ReadLine()) != null)
                {
                    currentLine++;
                    if (currentLine != plantIndex)
                    {
                        writer.WriteLine(line);
                    }
                    else
                    {
                        
                        for (int row = 1; row <= printPlantInfo.GetLength(1) - 1; row++)
                        {
                            Console.WriteLine("\nRewriting file...");
                            writer.Write(",");
                            for (int cols = 0; cols < printPlantInfo.GetLength(0); cols++)
                            {
                                writer.Write(printPlantInfo[cols, row] + ",");
                            }
                            writer.Write("\n");
                        }
                    }
                }
            }
        }

        File.Replace("EditedPlantList.txt", path, "OldPlantListBackup.txt");    // Overwrites the previous content in the file.
        Console.WriteLine("\n\n>> File line replaced.\n");
    }
}