
using System;

public class RandomeNumber 
{
    public static int GenerateNumber(int range)
    {
        var random = new Random();

        return random.Next(1, range);
    }
  
}
