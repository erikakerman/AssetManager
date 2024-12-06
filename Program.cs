using System;

class Program
{
    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.Clear(); // Clear console for better readability
            Console.WriteLine("Asset Manager");
            Console.WriteLine("------------");
            Console.WriteLine("1. Add Asset");
            Console.WriteLine("2. Remove Asset");
            Console.WriteLine("3. View Assets");
            Console.WriteLine("4. Exit");
            Console.Write("\nSelect an option (1-4): ");

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
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

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

        var asset = new Asset
        {
            Type = type,
            Brand = brand,
            ModelName = model,
            Price = price,
            PurchaseDate = DateTime.Now,
            EndOfLife = DateTime.Now.AddYears(3)
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

    static void RemoveAsset()
    {
        Console.Clear();
        Console.WriteLine("Remove Asset");
        Console.WriteLine("------------");

        // First show all assets
        using (var context = new AssetDbContext())
        {
            var assets = context.Assets.ToList();
            foreach (var a in assets)
            {
                Console.WriteLine($"Id: {a.Id}, Type: {a.Type}, Brand: {a.Brand}, Model: {a.ModelName}, Price: {a.Price}");
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
                Console.WriteLine($"Id: {a.Id}, Type: {a.Type}, Brand: {a.Brand}, Model: {a.ModelName}, Price: {a.Price}, PurchaseDate: {a.PurchaseDate:yyyy-MM-dd}");
            }
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();
    }
}