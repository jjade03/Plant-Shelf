class PlantShelf
{
    /* Ensures user enters a valid option and returns the value */
    public int ValidOptCheck(int option)
    {
        string optionPrompt = " Enter a valid option (1-3): ";
        // Ensures user enters a valid option.
        while (option <= 0 || option > 3)
        {
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
                if (option <= 0 || option > 3)
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
    public void MenuLoop(int option, string path, string welcomeMsg)
    {
        bool userContinue = true;   // Defines the loop as true.
        ViewPlants viewObj = new();
        AddPlants addObj = new();

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
                        viewObj.OutputPlantInfo(path);
                    }
                    break;
                case 2:
                    addObj.UserPlantInfo(path, viewObj);
                    break;
                case 3:
                    Console.WriteLine("\n>> Exiting your plant shelf...\n\n\n>> Bye!\n");
                    userContinue = false;
                    break;
            }

            if (userContinue)
            {
                option = -1;    // Reinitializes option as an invalid number.
                Console.Write(welcomeMsg);
                option = ValidOptCheck(option);
            }
        }
    }
}