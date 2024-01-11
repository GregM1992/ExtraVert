using System.Net.Sockets;

List<Plant> plants = new()
{
    new Plant()
    {   
        Species = "Spider Plant",
        LightNeeds = 3,
        AskingPrice = 35.00m,
        City = "Nashville",
        ZIP = 37206, 
        Sold = false
    },
    new Plant()
    {
        Species = "Snake Plant",
        LightNeeds = 2,
        AskingPrice = 45.00m,
        City = "Asheville",
        ZIP = 37854,
        Sold = true
    },
    new Plant()
    {
        Species = "Peace Lily",
        LightNeeds = 1,
        AskingPrice = 35.00m,
        City = "Nashville",
        ZIP = 37206,
        Sold = false
    },
    new Plant()
    {
        Species = "Fiddle Leaf Fig",
        LightNeeds = 3,
        AskingPrice = 35.00m,
        City = "Nashville",
        ZIP = 37206,
        Sold = false
    },
    new Plant()
    {
        Species = "Pothos",
        LightNeeds = 5,
        AskingPrice = 15.00m,
        City = "Pennsylvania",
        ZIP = 84585,
        Sold = true
    },
};

Random random = new Random();

void Greeting()
{
    Console.WriteLine($"Welcome to the Plant Store!\n");
    PlantOfTheDay();
}
void ShowPlants()
{
    int numIndex = 1;// starts the numbering at one


    foreach (Plant plant in plants)
    {
        Console.WriteLine($@"{numIndex++}. A {plant.Species} in {plant.City} {(plant.Sold ? "was sold" :"is available")} for {plant.AskingPrice} dollars. 
" );
    }
}
void ShowMenu()
   {
    string choice = null;
    Console.WriteLine(@"Please select a following option:

0. Exit

1. Show all plants

2. Post a plant to be adopted

3. Adopt a plant

4. Delist a plant

5. Search plants based on light needs

");
   choice = Console.ReadLine();
        switch (choice) 
        {
            case "0":
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
                break;
             case "1":
            Console.WriteLine($@"Here are our currently listed plants:
");
                ShowPlants();
                break;
            case "2":
            PostPlant();
                break;
            case "3":
            AdoptPlant();
                break;
            case "4":
            RemoveAPlant();
                break;
        case "5":
            SearchForLightShade();
            break;
        default: Console.WriteLine($"That is not an option\n");
            ShowMenu();
            break;

        }
    }
void PostPlant()
{
    Console.WriteLine("Enter the species of the plant:");
    string species = Console.ReadLine();
    Console.WriteLine("Enter the amount of light needed as 1-5 (1 being shady and 5 being bright light):");
    int lightNeeded = Convert.ToInt32( Console.ReadLine() );
    Console.WriteLine("Please enter the asking price for the plant:");
    decimal askingPrice = Convert.ToDecimal(Console.ReadLine());
    Console.WriteLine("What city is this being sold in?");
    string city = Console.ReadLine();
    Console.WriteLine("Please enter the zip code");
    int zipCode = Convert.ToInt32( Console.ReadLine());

    Plant newPlant = new()
    {
        Species = species,
        LightNeeds = lightNeeded,
        AskingPrice = askingPrice,
        City = city,
        ZIP = zipCode
    };
    plants.Add(newPlant);
    Console.Clear();
    Console.WriteLine("Plant Added!");
    ShowMenu();

}
void AdoptPlant()
{
    string plantChoice = null;

    int numIndex = 1;

    int plantSelected = 0; // index of the plant selected

    Console.WriteLine("Please choose the number of the plant you would like to adopt:");

    List<Plant> availablePlants = plants.Where(p => !p.Sold).ToList(); 

    foreach(Plant plant in availablePlants)
    {
        Console.WriteLine($@"{numIndex++}. {plant.Species}, Price: {plant.AskingPrice} dollars");   
    }

      plantChoice = Console.ReadLine();

    if (plantChoice != null && plantChoice != "0" && Convert.ToInt32(plantChoice) < availablePlants.Count)
      
        {
        plantSelected = Convert.ToInt32(plantChoice) - 1;

        Plant plantBeingAdopted = availablePlants[plantSelected];

        plantBeingAdopted.Sold = true;

        Console.WriteLine($"Enjoy your new {plantBeingAdopted.Species}!!!");

    }
    else 
    {
        Console.WriteLine("That is not an option please select another.");
        AdoptPlant();
    }
     
}
void RemoveAPlant()
{   
    
    string plantChoice = null;
    Console.WriteLine("Please choose the number of the plant listing you would like to remove.\n");
    int numIndex = 1;// starts the numbering at one


    foreach (Plant plant in plants)
    {
        Console.WriteLine($@"{numIndex++}.  {plant.Species} in {plant.City}. 
"); 
    }
        plantChoice = Console.ReadLine();
        if(plantChoice != null && Convert.ToInt32(plantChoice) < plants.Count && plantChoice != "0") 
        {
            plants.RemoveAt(Convert.ToInt32(plantChoice) - 1);
        Console.WriteLine("Listing has been removed.");
        ShowPlants() ;
        

        }
        else
        {
            Console.WriteLine("That is not an option please select another");
            RemoveAPlant() ;
        }

}
void PlantOfTheDay()
{
    List<Plant> availablePlants = plants.Where(p => !p.Sold).ToList();
    int randomPlantInt = random.Next(1, availablePlants.Count);
    Plant randomPlant = availablePlants[randomPlantInt];
    Console.WriteLine($"{randomPlant.Species}\n");
    
}

void SearchForLightShade()
{
    int searchInput = 0;

    Console.WriteLine("Please enter the light need for the plant you would like. Using 1-5 ( 1 being the least light ) :");
    try
    {
    searchInput = Convert.ToInt32(Console.ReadLine());
    List<Plant> searchedPlants = plants.Where(p => p.LightNeeds == searchInput).ToList();

    foreach(Plant plant in searchedPlants)
    { 
        Console.WriteLine($"{plant.Species}\n");
    }

    if(searchedPlants.Count == 0 || searchInput > 5)
    {
        Console.WriteLine($"Sorry, we dont have plants that meet that light requirement\n\n");
    } 
    } 
    catch
    {
        Console.WriteLine($"That is not a valid input\n");
        SearchForLightShade();
    }
    
  
   








}

   

Greeting();
ShowMenu();


