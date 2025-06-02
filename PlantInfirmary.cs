class PlantInfirmary
{
    // Creates objects for the referenced classes.
    private static readonly Navigation navObj = new();
    public void InfirmaryMenu(int option, string navMsg)
    {
        bool userContinue = true;
        while (userContinue)
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine("option 1\n");
                    break;
                case 2:
                    Console.WriteLine("option 2\n");
                    break;
                case 3:
                    Console.WriteLine("option 3\n");
                    break;
                case 4:
                    Console.WriteLine("\n>> Exiting your infirmary...\n");
                    navObj.WelcomeMenu(Program.plantLog, Program.welcomeMsg, Program.navMsg, 6, false);
                    userContinue = false;
                    break;
            }

            if (userContinue)
            {
                option = -1;    // Reinitializes option as an invalid number.
                Console.Write(navMsg);
                option = navObj.ValidOptCheck(option, 0, 4, 0);
            }
        }
    }
}