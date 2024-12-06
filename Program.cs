using System;
using System.Linq; 

class Program
{
    // Main menu loop
    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Asset Manager");
            Console.WriteLine("------------");
            Console.WriteLine("1. Add Asset");
            Console.WriteLine("2. Remove Asset");
            Console.WriteLine("3. View Assets");
            Console.WriteLine("4. Sort by Purchase date"); 
            Console.WriteLine("5. Exit");
            Console.Write("\nSelect an option (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddAsset();
                    break;
                case "2":
                    RemoveAsset();
                    break;
                case "3":
                    ViewAssets();
                    break;
                case "4":
                    ViewSortedAssets();
                    break;
                case "5": 
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Displays assets sorted by purchase date, marks old assets in red
    static void ViewSortedAssets()
    {
        Console.Clear();
        Console.WriteLine("Assets Sorted by Purchase Date");
        Console.WriteLine("-----------------------------");

        using (var context = new AssetDbContext())
        {
            // Get assets and sort them by purchase date
            var sortedAssets = context.Assets
                .OrderBy(a => a.PurchaseDate)
                .ToList();

            Console.WriteLine($"Total assets found: {sortedAssets.Count}\n");

            foreach (var a in sortedAssets)
            {
                // Check if purchase date is more than 3 years ago
                if (a.PurchaseDate < DateTime.Now.AddYears(-3))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine($"{a.Id,-5} {a.Type,-12} {a.Brand,-15} {a.ModelName,-15} ${a.Price,-10} {a.PurchaseDate:yyyy-MM-dd}");

                // Reset color back to default
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }

    // Adds new asset to database with user input validation
    static void AddAsset()
    {
        Console.Clear();
        Console.WriteLine("Add New Asset");
        Console.WriteLine("------------");

        Console.Write("Enter asset type (Computer/Phone): ");
        string type = Console.ReadLine();

        Console.Write("Enter brand: ");
        string brand = Console.ReadLine();

        Console.Write("Enter model: ");
        string model = Console.ReadLine();

        Console.Write("Enter price: ");
        int price = int.Parse(Console.ReadLine());

        Console.Write("Enter purchase date (yyyy-MM-dd): ");
        DateTime purchaseDate;
        while (!DateTime.TryParse(Console.ReadLine(), out purchaseDate))
        {
            Console.Write("Invalid date format. Please enter date as yyyy-MM-dd: ");
        }

        var asset = new Asset
        {
            Type = type,
            Brand = brand,
            ModelName = model,
            Price = price,
            PurchaseDate = purchaseDate,
            EndOfLife = purchaseDate.AddYears(3)
        };

        using (var context = new AssetDbContext())
        {
            context.Assets.Add(asset);
            context.SaveChanges();
            Console.WriteLine("\nAsset saved successfully!");
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }

    // Removes Asset by ID
    static void RemoveAsset()
    {
        Console.Clear();
        Console.WriteLine("Remove Asset");
        Console.WriteLine("------------");

       
        using (var context = new AssetDbContext())
        {
            var assets = context.Assets.ToList();
            foreach (var a in assets)
            {
                Console.WriteLine($"{a.Id,-5} {a.Type,-12} {a.Brand,-15} {a.ModelName,-15} ${a.Price,-10}");
            }

            Console.Write("\nEnter the Id of the asset to remove: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var assetToRemove = context.Assets.Find(id);
                if (assetToRemove != null)
                {
                    context.Assets.Remove(assetToRemove);
                    context.SaveChanges();
                    Console.WriteLine("Asset removed successfully!");
                }
                else
                {
                    Console.WriteLine("Asset not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid Id!");
            }
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }

    // Displays all assets, marks assets older than 3 years in red
    static void ViewAssets()
    {
        Console.Clear();
        Console.WriteLine("All Assets in Database");
        Console.WriteLine("--------------------");

        using (var context = new AssetDbContext())
        {
            var assets = context.Assets.ToList();
            Console.WriteLine($"Total assets found: {assets.Count}\n");

            foreach (var a in assets)
            {
                // Check if purchase date is more than 3 years ago
                if (a.PurchaseDate < DateTime.Now.AddYears(-3))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine($"{a.Id,-5} {a.Type,-12} {a.Brand,-15} {a.ModelName,-15} ${a.Price,-10} {a.PurchaseDate:yyyy-MM-dd}");
                // Reset color back to default
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }
}