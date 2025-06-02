class Navigation
{
    // Creates objects for the referenced classes.
    private static readonly ViewPlants viewObj = new();
    private static readonly AddPlants addObj = new();
    private static readonly EditPlants editObj = new();
    private static readonly PlantInfirmary infirmObj = new();

    public void WelcomeMenu(string path, string welcomeMsg, string navMsg, int numOfOpts, bool infirmCheck)
    {
        int option = -1;    // Initializes 'option' as an invalid number.

        // Checks if the file already exists. If it doesn't, creates the file and closes it.
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        Console.Write(welcomeMsg + navMsg);                 // Prints a welcome message for the user, prompting them for input.
        option = ValidOptCheck(option, 0, numOfOpts, 0);    // Redefines the option as the user's input.

        // Navigates to the appropriate menu.
        if (infirmCheck == false)
        {
            MenuLoop(option, path, navMsg, numOfOpts);
        }
        else
        {
            infirmObj.InfirmaryMenu(option, navMsg);
        }
    }

    /* Ensures user enters a valid option and returns the value */
    public int ValidOptCheck(int option, int lowVal, int highVal, int holdVal)
    {
        string optionPrompt = " Enter a valid option (" + (lowVal + 1 + holdVal) + "-" + highVal + ") : ";
        // Ensures user enters a valid option.
        while (option <= lowVal || option > highVal - holdVal)
        {
            try
            {
                option = Convert.ToInt32(Console.ReadLine()) - holdVal;
                if (option <= lowVal || option > highVal - holdVal)
                {
                    Console.Write(">> Invalid option entered." + optionPrompt);
                }
            }
            catch (Exception e)
            {
                Console.Write(">> " + e.Message + optionPrompt);
            }
        }
        return option;
    }

    /* Loops through the menu, calling the appropriate methods corresponding with the user's choice */
    public void MenuLoop(int option, string path, string navMsg, int numOfOpts)
    {
        bool userContinue = true;   // Defines the loop as true.

        // Loops through the menu until the user decides to exit.
        while (userContinue)
        {
            switch (option)
            {
                case 1:
                    long fileLength = new FileInfo(path).Length;
                    if (fileLength == 0)
                    {
                        Console.WriteLine("\n> No plants currently exist.\n");
                    }
                    else
                    {
                        viewObj.OutputPlantInfo(path, false);
                    }
                    break;
                case 2:
                    addObj.UserPlantInfo(path);
                    break;
                case 3:
                    int numLines = File.ReadAllLines(path).Length - 1;      // Holds the amount of lines in the file.
                    viewObj.OutputPlantInfo(path, true);
                    Console.Write("> Enter the plant's index that you wish to edit: ");
                    int plantIndex = ValidOptCheck(-1, -1, numLines, 1);
                    editObj.EditPlantInfo(path, plantIndex, numOfOpts, false);
                    break;
                case 4:
                    numLines = File.ReadAllLines(path).Length - 1;
                    viewObj.OutputPlantInfo(path, true);
                    Console.Write("> Enter the plant's index that you wish to remove: ");
                    plantIndex = ValidOptCheck(-1, -1, numLines, 1);
                    editObj.EditPlantInfo(path, plantIndex, numOfOpts, true);
                    break;
                case 5:
                    string infirmPath = "InfirmaryLog.txt";
                    string infirmWelcome = "\nWelcome to your infimary. ";
                    string infirmMsg = "What would you like to do?\n1. View Sick Plants\n2. Log Sick Plant\n3. Add Health Notes\n4. Leave Infirmary" +
                            "\n\n> Enter the number corresponding with your selection: ";
                    numOfOpts = 4;

                    WelcomeMenu(infirmPath, infirmWelcome, infirmMsg, numOfOpts, true);
                    break;
                case 6:
                    Console.WriteLine("\n>> Exiting your plant shelf...\n\n\n>> Bye!\n");
                    userContinue = false;
                    break;
            }

            if (userContinue)
            {
                option = -1;    // Reinitializes option as an invalid number.
                Console.Write(navMsg);
                option = ValidOptCheck(option, 0, numOfOpts, 0);
            }
        }
    }
}