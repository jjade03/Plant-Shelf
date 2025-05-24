class Program
{
    static void Main(string[] args)
    {
        string path = @"PlantList.txt";

        string welcomeMsg = "What would you like to do?\n1. View Plants\n2. Add New Plant\n3. Exit" +
        "\n\n> Enter the number corresponding with your selection: ";
        int option = -1;                 // Initializes 'option' as an invalid number.

        PlantShelf shelfObj = new();     // Creates an object of the 'PlantShelf' class.

        // Creates a path and checks if the file already exists. If it doesn't, creates the file and closes it.
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }

        Console.Write("Welcome to your plant shelf! " + welcomeMsg);     // Prints a welcome message for the user, prompting them for input.
        option = shelfObj.ValidOptCheck(option);                        // Redefines the option as the user's input.
        shelfObj.MenuLoop(option, path, welcomeMsg);                    // Calls the 'menuLoop' method from the 'PlantShelf' class
    }
}