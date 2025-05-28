class AddPlants
{
    /* Prompts the user to enter their plant's information */
    public void UserPlantInfo(string path, ViewPlants viewObj)
    {
        int plantAge = -1;      // Sets the default age as an invalid value.
        string plantInfo;       // Creates a FileStream object to write to the text file.

        /* RECEIVES PLANT INFORMATION [START] */
        string speciesPrompt = "\n> Enter Plant Species: ";
        string plantSpecies = CharLimit(speciesPrompt);

        // Checks if the plant's age is a valid input.
        while (plantAge < 0 || plantAge.ToString().Length > 2)
        {
            try
            {
                string agePrompt = "> Enter Plant's Age: ";
                plantAge = Convert.ToInt32(CharLimit(agePrompt));
                if (plantAge < 0 || plantAge.ToString().Length > 2)
                {
                    Console.WriteLine(">> Invalid age entered.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(">> " + e.Message);
            }
        }

        string waterPrompt = "> Enter Watering Frequency: ";
        string waterFrequency = CharLimit(waterPrompt);

        string sunPrompt = "> Enter Sunlight Requirement: ";
        string sunRequirement = CharLimit(sunPrompt);

        string locationPrompt = "> Enter Plant's Location in Room: ";
        string plantLocation = CharLimit(locationPrompt);

        string checkNamePrompt = "> Does your plant have a nickname? (Enter 'yes' or 'no'): ";
        Console.Write(checkNamePrompt);
        string checkName = Console.ReadLine().ToLower();
        checkName = YNCheck(checkName, checkNamePrompt);

        if (checkName == "yes")
        {
            string nicknamePrompt = "> Enter Nickname: ";
            string plantNickname = CharLimit(nicknamePrompt);

            plantInfo = plantNickname + ",";        // Adds the plant's nickname to the string.
        }
        else
        {
            plantInfo = "null" + ",";               // Adds temporary name to the string.
        }
        /* RECEIVES PLANT INFORMATION [END] */
        // Appends user's plant information to the string.
        plantInfo += $"Species:                    {plantSpecies},Age:                        {plantAge},Watering Frequency:         {waterFrequency}," +
        $"Sunlight Requirement:       {sunRequirement},Room Location:              {plantLocation},\n,";

        string[] checkInfo = plantInfo.Split(",", StringSplitOptions.RemoveEmptyEntries);   // Splits the string.
        //string[] formatInfo = new string[checkInfo.Length];                                 // Holds the contents of the file with uppercases.

        string[] formatInfo = viewObj.FormatOutput(checkInfo);

        // Prints the formatted information.
        Console.WriteLine();
        foreach (string element in formatInfo)
        {
            Console.WriteLine(element);
        }

        // Checks if the information entered is correct and writes it to the file.
        string plantInfoPrompt = "> Is the plant's information correct? (Enter 'yes' or 'no'): ";
        Console.Write(plantInfoPrompt);
        string confirm = Console.ReadLine().ToLower();
        confirm = YNCheck(confirm, plantInfoPrompt);

        if (confirm == "yes")
        {
            File.AppendAllText(path, plantInfo);        // Writes information to the file, then closes it.
            Console.WriteLine("\n>> Information Saved!\n\n");
        }
        else
        {
            UserPlantInfo(path, viewObj);
        }
    }

    /* Checks if the input is within the character limit and returns the value */
    private static string CharLimit(string prompt)
    {
        bool underLimit = false;
        string answer = "";
        // Enforces a character limit and checks if the input is null.
        while (underLimit == false || answer == "null")
        {
            Console.Write(prompt);
            answer = Console.ReadLine() ?? "null";
            if (answer.Length > 30)
            {
                Console.WriteLine(">> Description exceeds the 30 character limit.");
                underLimit = false;
            }
            else if (answer.Length == 0 || answer == "null")
            {
                Console.WriteLine(">> Invalid input entered.");
            }
            else
            {
                underLimit = true;
            }
        }
        return answer;
    }
    
    /* Ensures user enters either 'yes' or 'no' and returns the answer */
    public string YNCheck(string check, string checkPrompt)
    {
        while (check != "yes" && check != "no")
        {
            Console.Write(">> Invalid answer given: '" + check + "'.\n" + checkPrompt);
            check = Console.ReadLine().ToLower();
        }
        return check;
    }
}