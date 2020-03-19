using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogGenetics
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            string dogName;
            int dogGenes = 100;
            int temp;
            int percentage = 0;
            int unit;
            int[] dogType = new int[5];

            Console.WriteLine("What is your dog's name? ");
            dogName = GetDogName();
            
            // get dog genetics percentage
            for (int i = 0; i < 5; i++)
            {
                if (i == 4)
                {
                    dogType[i] = (dogGenes * 100) / 100;
                    break;
                }
                unit = rand.Next(1, (dogGenes / 5));
                dogGenes -= unit;
                percentage = (unit * 100) / 100;
                dogType[i] = percentage;
            }
            // Display Results
            DisplayGeneticReport(dogName, dogType);
            Console.ReadLine();
        }

        private static void DisplayGeneticReport(string dogName, int[] dogType)
        {
            Console.WriteLine($@"Well then, I have this highly reliable report on {dogName}'s prestigious background right here.

        {dogName} is:

        {dogType[0]}% St. Bernard
        {dogType[1]}% Chihuahua
        {dogType[2]}% Dramatic RedNosed Asian Pug
        {dogType[3]}% Common Cur
        {dogType[4]}% King Doberman

        Wow, that's QUITE the dog!    ");
        }

        private static string GetDogName()
        {
            string dogName;
            while (true)
            {
                dogName = Console.ReadLine();
                if (string.IsNullOrEmpty(dogName))
                {
                    Console.WriteLine("You did not enter anything!\n");
                    Console.WriteLine("What is your dog's name? ");
                }
                else
                {
                    break;
                }
            }

            return dogName;
        }
    }
}
