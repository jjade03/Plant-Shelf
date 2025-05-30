class ViewPlants
{
    /* Formats and outputs the user's plant information */
    public void OutputPlantInfo(string path, bool numCheck)
    {
        /* Formatting variables */
        // Divides the file based on the parameters provided in 'separators'.
        string fileContent = File.ReadAllText(path);
        string[] dividedInfo = fileContent.Split(",", StringSplitOptions.RemoveEmptyEntries);

        int numElements = 6;                                           // Max number of variables entered into file on each line.
        int fileRows = File.ReadLines(path).Count() - 1;               // Gets the numbers of lines in the file.
        string[,] readPlantInfo = new string[numElements, fileRows];   // Formatted version of the file contents.

        /* Output Variables */
        int perRow = Console.WindowWidth / 80;                         // Defines the max number of columns relative to the console's width.
        int maxIndex = perRow;                                         // Defines the max index to be printed on a single row in the console.

        // Increments through each element in the array, making the first character of each element uppercase.
        string[] formatInfo = FormatOutput(dividedInfo);

        readPlantInfo = ReindexArray(readPlantInfo, numElements, formatInfo, 7);

        // Reinitializes 'maxIndex' if the length of dimension 1 in the array is less than the number of indexes set to print per row.
        if (readPlantInfo.GetLength(1) < perRow)
        {
            maxIndex = readPlantInfo.GetLength(1);
        }
        OutputPerRow(maxIndex, readPlantInfo, perRow, numCheck);
    }

    /* Formats and returns a given string array */
    public string[] FormatOutput(string[] toFormat)
    {
        string[] formatInfo = new string[toFormat.Length];  // Holds the contents of the file with uppercases.

        bool spaceCheck = false;                            // Checks if the current character is a space.

        for (int i = 0; i < toFormat.Length; i++)
        {
            // If the nickname is null, add the species name instead.
            if (toFormat[i] == "null")
            {
                toFormat[i] = "";    // Empties the current index in the array.
                // Receives the species name from the fixed position.
                for (int newIndex = 28; newIndex < toFormat[i + 1].Length; newIndex++)
                {
                    toFormat[i] += toFormat[i + 1][newIndex];
                }
            }

            // Increments through each character of the current element in the array.
            for (int k = 0; k < toFormat[i].Length; k++)
            {
                if (k == 0 || spaceCheck == true)
                {
                    // Converts the first letter of each element to be uppercase.
                    formatInfo[i] += char.ToUpper(toFormat[i][k]);
                    spaceCheck = false;
                }
                else
                {
                    formatInfo[i] += toFormat[i][k];
                }
                // Checks if the current position is whitespace and saves the answer as a bool.
                if (char.IsWhiteSpace(toFormat[i][k]))
                {
                    spaceCheck = true;
                }
            }
        }
        return formatInfo;
    }

    public string[,] ReindexArray(string[,] indexedArray, int numElements, string[] toIndex, int increm)
    {
        int countElem = 0;      // Holds the current actual element position.
        int countCols = 0;

        // Increments through the array by the specified amount so that matching variables are inputted into the new array on the same row.
        for (int i = 0; i < numElements; i++)
        {
            for (int k = countElem; k < toIndex.Length; k += increm)
            {
                indexedArray[countElem, countCols] = toIndex[k];
                countCols++;
            }
            countElem++;
            countCols = 0;
        }
        return indexedArray;
    }

    public void OutputPerRow(int maxIndex, string[,] multiArray, int perRow, bool numCheck)
    {
        int evenRows = multiArray.GetLength(1) / 2;     // Defines the amount of full rows.
        int y = 0;                                      // Initializes the starting index.
        int startIndex = 0;                             // Saves the starting position for 'y'.
        int countRows = 0;                              // Holds the number of rows printed to the console.

        // Increments through the array, outputting two columns per row in the console.
        while (multiArray.GetLength(1) > y)
        {
            for (int x = 0; x < multiArray.GetLength(0); x++)
            {
                for (; y < maxIndex; y++)
                {
                    // Inserts number before each block of data with the proper indentation.
                    if (numCheck == true && x == 0)
                    {
                        Console.Write($"{y + 1}. ");
                        Console.Write($"{multiArray[x, y],-57}");
                    }
                    else
                    {
                        Console.Write($"{multiArray[x, y],-60}");
                    }
                    // Add a new line when the end of the row is reached in the array.
                    if (maxIndex - 1 == y)
                    {
                        Console.WriteLine("");
                    }
                }
                y = startIndex;     // Reinitializes the starting position.
            }
            Console.WriteLine("");

            y += perRow;            // Increments 'y' by the number of columns to be printed on one row.
            startIndex = y;         // Reinitializes the starting position of 'y'.
            countRows++;
            maxIndex = (countRows == evenRows) ? maxIndex += 1 : maxIndex += perRow;
        }
    }
}