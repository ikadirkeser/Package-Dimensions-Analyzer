using System;

namespace PackageExpress
{
    /// <summary>
    /// Main program class
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Display welcome message
            Console.WriteLine("Welcome to Package Express. Please follow the instructions below.");
            
            try
            {
                // Create a package object with user input
                Package package = new Package();
                package.GetPackageDetailsFromUser();
                
                // Validate the package
                if (!package.IsValidForShipping())
                {
                    // If invalid, the validation messages are already shown
                    return;
                }
                
                // Calculate and display shipping quote
                decimal quote = package.CalculateShippingQuote();
                Console.WriteLine($"Your estimated total for shipping this package is: ${quote:F2}");
                Console.WriteLine("Thank you!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter valid numeric values for all measurements.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                // Keep console window open
                Console.ReadLine();
            }
        }
    }
    
    /// <summary>
    /// Class representing a package with dimensions and weight
    /// </summary>
    class Package
    {
        // Maximum values for shipping
        private const float MAX_WEIGHT = 50.0f;
        private const float MAX_DIMENSIONS_SUM = 50.0f;
        
        // Package properties
        public float Weight { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
        public float Length { get; private set; }
        
        /// <summary>
        /// Gets all package details from user input
        /// </summary>
        public void GetPackageDetailsFromUser()
        {
            // Get weight
            Console.WriteLine("Please enter the package weight:");
            Weight = float.Parse(Console.ReadLine());
            
            // If weight is invalid, don't proceed with other inputs
            if (Weight > MAX_WEIGHT)
            {
                return;
            }
            
            // Get width
            Console.WriteLine("Please enter the package width:");
            Width = float.Parse(Console.ReadLine());
            
            // Get height
            Console.WriteLine("Please enter the package height:");
            Height = float.Parse(Console.ReadLine());
            
            // Get length
            Console.WriteLine("Please enter the package length:");
            Length = float.Parse(Console.ReadLine());
        }
        
        /// <summary>
        /// Validates if the package is eligible for shipping
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidForShipping()
        {
            // Check weight
            if (Weight > MAX_WEIGHT)
            {
                Console.WriteLine("Package too heavy to be shipped via Package Express. Have a good day.");
                return false;
            }
            
            // Check dimensions
            if (Width + Height + Length > MAX_DIMENSIONS_SUM)
            {
                Console.WriteLine("Package too big to be shipped via Package Express.");
                return false;
            }
            
            return true;
        }
        
        /// <summary>
        /// Calculates the shipping quote based on package dimensions and weight
        /// </summary>
        /// <returns>The calculated shipping quote</returns>
        public decimal CalculateShippingQuote()
        {
            // Use the formula: (Width * Height * Length * Weight) / 100
            decimal quote = (decimal)(Width * Height * Length * Weight) / 100m;
            
            // Return rounded to 2 decimal places
            return Math.Round(quote, 2);
        }
    }
}