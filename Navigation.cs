class Navigation
{
    /* Ensures user enters a valid option and returns the value */
    public int ValidOptCheck(int option, int lowVal, int highVal, int holdVal)
    {
        string optionPrompt = " Enter a valid option (" + (lowVal + 1) + "-" + (highVal + holdVal) + ") : ";
        // Ensures user enters a valid option.
        while (option < lowVal || option > highVal)
        {
            try
            {
                option = Convert.ToInt32(Console.ReadLine()) - holdVal;
                if (option < lowVal || option > highVal)
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
        // Creates objects of the referenced classes.
        ViewPlants viewObj = new();
        AddPlants addObj = new();
        EditPlants editObj = new();

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
                    addObj.UserPlantInfo(path, viewObj);
                    break;
                case 3:
                    viewObj.OutputPlantInfo(path, true);
                    Console.Write("> Enter the plant's index that you wish to edit: ");
                    int plantIndex = ValidOptCheck(-1, 0, 5, 1);       // TEMP VALUES-- Ensures the user entered a valid option.
                    editObj.EditPlantInfo(path, plantIndex, viewObj);
                    break;
                case 4:
                    Console.WriteLine("\n>> Exiting your plant shelf...\n\n\n>> Bye!\n");
                    userContinue = false;
                    break;
            }

            if (userContinue)
            {
                option = -1;    // Reinitializes option as an invalid number.
                Console.Write(welcomeMsg);
                option = ValidOptCheck(option, 0, 4, 0);
            }
        }
    }
}