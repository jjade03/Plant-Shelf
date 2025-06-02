﻿class Program
{
    public static string welcomeMsg = "Welcome to your plant shelf! ";
    public static string navMsg = "What would you like to do?\n1. View Plants\n2. Add New Plant\n3. Edit Plant\n4. Remove Plant\n5. Enter Infirmary\n6. Exit" +
    "\n\n> Enter the number corresponding with your selection: ";
    public static string plantLog = @"PlantList.txt";

    static void Main(string[] args)
    {
        int numOfOpts = 6;

        Navigation navObj = new();

        navObj.WelcomeMenu(plantLog, welcomeMsg, navMsg, numOfOpts, false);
    }
}