class EditPlants
{
    // Creates objects for the referenced classes.
    private static readonly AddPlants addObj = new();
    private static readonly ViewPlants viewObj = new();
    private static readonly Navigation navObj = new();

    public void EditPlantInfo(string path, int plantIndex, int numOfOpts, bool remove)
    {
        // Formats the line to be edited.
        string fileLine = File.ReadLines(path).Skip(plantIndex).Take(1).First();    // The file line being edited.
        string[] dividedInfo = fileLine.Split(',', StringSplitOptions.RemoveEmptyEntries);

        string[] plantInfo = viewObj.FormatOutput(dividedInfo);                     // The formatted information.
        string[,] printPlantInfo = new string[plantInfo.Length, 2];                 // Declares a 2D array to hold the new and old plant information.

        string confirmMsg = "> Enter 'yes' or 'no' to continue: ";
        string newConfirm = "no";
        string msg = "edit";    // Initializes the string as 'edit'.

        // Redefines the string if the option is set to remove.
        if (remove == true)
        {
            msg = "remove";
        }

        // Prompts user to confirm their selection.
        Console.WriteLine("\n\nIs this the plant you wish to " + msg + "?");
        foreach (string element in plantInfo)
        {
            Console.WriteLine(element);
        }
        Console.Write(confirmMsg);
        string confirm = (Console.ReadLine() ?? "null").ToLower();
        confirm = addObj.YNCheck(confirm, confirmMsg);      // Ensures valid answer is entered.

        // If the plant they selected is not the one they wish to edit, repeat the loop.
        if (confirm == "no")
        {
            navObj.MenuLoop(3, path, Program.navMsg, numOfOpts);
        }
        else if (confirm == "yes" && remove == false)
        {
            // Otherwise, prompts user to edit the plant, looping through the appropriate redirections until they are satisfied with the result.
            while (confirm == "yes" && newConfirm != "yes")
            {
                // Prompts user to enter the new information if the edit option is chosen.
                printPlantInfo = ReenterInfo(plantInfo, printPlantInfo);
                // Confirms the information the user entered.
                Console.Write("\nIs this information correct?\n" + confirmMsg);
                newConfirm = (Console.ReadLine() ?? "null").ToLower();
                newConfirm = addObj.YNCheck(newConfirm, confirmMsg);

                if (newConfirm != "yes")
                {
                    EditPlantInfo(path, plantIndex, numOfOpts, remove);     // Returns to start of method.
                }
                else if (newConfirm == "yes" && remove == false)
                {
                    EditLine(path, plantIndex, printPlantInfo, remove);     // Writes the line to the file.
                }
            }
        }
        else if (confirm == "yes" && remove == true)
        {

            EditLine(path, plantIndex, printPlantInfo, remove);             // Removes the line from the file.
        }
    }

    /* Returns the new and old plant information in a formatted 2D array */
    private static string[,] ReenterInfo(string[] plantInfo, string[,] printPlantInfo)
    {
        string[] newPlantInfo = new string[12];     // Declare new array to hold final values.
        int usedIndxs = 0;                          // Initialize the actual index to be incremented on.

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
    private static void EditLine(string path, int plantIndex, string[,] printPlantInfo, bool remove)
    {
        string? line = null;     // Holds the contents of the current line.
        int currentLine = -1;
        int fileLength = File.ReadAllLines(path).Length - 1;

        // Reads the contents from the file.
        using (StreamReader reader = new(path))
        {
            using StreamWriter writer = new("EditedPlantList.txt");
            // Removes the appropriate line from the file.
            while ((line = reader.ReadLine()) != null)
            {
                currentLine++;
                if (currentLine != plantIndex && currentLine != fileLength)
                {
                    writer.WriteLine(line);
                }
                else if (currentLine != plantIndex && currentLine == fileLength)
                {
                    writer.Write(",");
                }
                else
                {
                    if (remove == false)
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
                        Console.WriteLine("\n\n>> Plant information updated in the gallery.\n");
                    }
                    else
                    {
                        Console.WriteLine("\n\n>> Plant removed from the gallery.\n");
                    }
                }
            }
        }

        File.Replace("EditedPlantList.txt", path, "OldPlantListBackup.txt");    // Overwrites the previous content in the file.
    }
}