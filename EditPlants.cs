class EditPlants
{
    public void EditPlantInfo(string path, int plantIndex, ViewPlants viewObj)
    {
        string fileLine = File.ReadLines(path).Skip(plantIndex).Take(1).First();    // The file line being edited.
        string[] dividedInfo = fileLine.Split(",");

        string[] plantInfo = viewObj.FormatOutput(dividedInfo);

        AddPlants addObj = new();
        string confirmMsg = "Enter 'yes' or 'no' to continue: ";

        Navigation navObj = new();
        string welcomeMsg = "What would you like to do?\n1. View Plants\n2. Add New Plant\n3. Edit Plant\n4. Exit" +
    "\n\n> Enter the number corresponding with your selection: ";

        Console.WriteLine("\n\nIs this the plant you wish to edit?");
        foreach (string element in plantInfo)
        {
            Console.WriteLine(element);
        }
        Console.Write(confirmMsg);
        string confirm = Console.ReadLine().ToLower();
        confirm = addObj.YNCheck(confirm, confirmMsg);

        if (confirm == "yes")
        {
            Console.WriteLine("Confirmed.\n");        // Temporary test message.
        }
        else
        {
            navObj.MenuLoop(3, path, welcomeMsg);   // Repeat loop.
        }
    }
}