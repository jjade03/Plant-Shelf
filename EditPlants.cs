class EditPlants
{
    public void EditPlantInfo(string path, int plantIndex, ViewPlants viewObj)
    {
        // Formats the line to be edited.
        string fileLine = File.ReadLines(path).Skip(plantIndex).Take(1).First();    // The file line being edited.
        string[] dividedInfo = fileLine.Split(',', StringSplitOptions.RemoveEmptyEntries);

        string[] plantInfo = viewObj.FormatOutput(dividedInfo);

        // Creates objects for referenced classes.
        AddPlants addObj = new();
        Navigation navObj = new();

        string confirmMsg = "Enter 'yes' or 'no' to continue: ";
        string welcomeMsg = "What would you like to do?\n1. View Plants\n2. Add New Plant\n3. Edit Plant\n4. Exit" +
    "\n\n> Enter the number corresponding with your selection: ";

        // Prompts user to confirm their selection.
        Console.WriteLine("\n\nIs this the plant you wish to edit?");
        foreach (string element in plantInfo)
        {
            Console.WriteLine(element);
        }
        Console.Write(confirmMsg);
        string confirm = Console.ReadLine().ToLower();
        confirm = addObj.YNCheck(confirm, confirmMsg);      // Ensures valid answer is entered.

        if (confirm == "yes")
        {
            ReenterInfo(plantInfo);
        }
        else
        {
            navObj.MenuLoop(3, path, welcomeMsg);           // Repeat loop.
        }
    }

    private static void ReenterInfo(string[] plantInfo)
    {
        // Creates objects for referenced classes.
        AddPlants addObj = new();
        ViewPlants viewObj = new();

        string[] newPlantInfo = new string[12];   // Declare new array to hold final values.
        int usedIndxs = 0;                                                // Initialize the actual index to be incremented on.

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

        newPlantInfo = viewObj.FormatOutput(newPlantInfo);
        string[,] printPlantInfo = new string[plantInfo.Length, 2];
        printPlantInfo = viewObj.ReindexArray(printPlantInfo, plantInfo.Length, newPlantInfo, plantInfo.Length);
        Console.WriteLine($"\n{"OLD:", 20}{"-->", 30}{"NEW:", 30}\n");
        viewObj.OutputPerRow(2, printPlantInfo, 3, false);
    }
}