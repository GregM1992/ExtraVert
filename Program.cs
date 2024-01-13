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
        Sold = false,
        AvailableUntil =  new DateTime(2023, 3, 4),
    },
    new Plant()
    {
        Species = "Snake Plant",
        LightNeeds = 3,
        AskingPrice = 45.00m,
        City = "Asheville",
        ZIP = 37854,
        Sold = false,
        AvailableUntil = new DateTime(2025, 4, 20)
    },
    new Plant()
    {
        Species = "Peace Lily",
        LightNeeds = 4,
        AskingPrice = 35.00m,
        City = "Nashville",
        ZIP = 37206,
        Sold = true,
        AvailableUntil = new DateTime(2029, 3, 14)
    },
    new Plant()
    {
        Species = "Fiddle Leaf Fig",
        LightNeeds = 5,
        AskingPrice = 35.00m,
        City = "Nashville",
        ZIP = 37206,
        Sold = false,
        AvailableUntil = new DateTime(2020, 3, 3)
    },
    new Plant()
    {
        Species = "Pothos",
        LightNeeds = 5,
        AskingPrice = 15.00m,
        City = "Pennsylvania",
        ZIP = 84585,
        Sold = false,
        AvailableUntil = new DateTime(1992, 9, 22)
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

6. Show statistics

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
            case "6":
            ShowStats();
            break;
        default: Console.WriteLine($"That is not an option\n");
            ShowMenu();
            break;

        }
    }
void PostPlant()
{
    try
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
    Console.WriteLine("Please enter the year available until. (yyyy)");
    int year = Convert.ToInt32( Console.ReadLine());
    Console.WriteLine("Month available until? (mm)");
    int month = Convert.ToInt32( Console.ReadLine());
    Console.WriteLine("Day available until? (dd)");
    int day = Convert.ToInt32( Console.ReadLine());

    
        Plant newPlant = new()
        {
            Species = species,
            LightNeeds = lightNeeded,
            AskingPrice = askingPrice,
            City = city,
            ZIP = zipCode,
            AvailableUntil = new DateTime(year, month, day)
        };
    
        plants.Add(newPlant);
    }
    catch 
    {
        Console.WriteLine($"There was an issue with your input. Returning to the main menu\n");
        ShowMenu();
    }
    
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

    List<Plant> unSoldPlants = plants.Where(p => !p.Sold).ToList();
    List<Plant> availablePlants = unSoldPlants.Where(a => a.AvailableUntil >= DateTime.Now).ToList();

    foreach(Plant plant in availablePlants)
    {
        Console.WriteLine($@"{numIndex++}. {plant.Species}, Price: {plant.AskingPrice} dollars");   
    }

      plantChoice = Console.ReadLine();

    if (plantChoice != null && plantChoice != "0" && Convert.ToInt32(plantChoice) <=  availablePlants.Count)
      
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
    Console.WriteLine($"Our Plant of the day is a {PlantDetails(randomPlant)}!\n");// uses the plantdetails method with parameters


    
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
void ShowStats()
{
    Console.WriteLine($"These are the current statistics:\n");
    ShowLowestPricedPlant();
    ShowNumberOfAvailablePlants(); 
    ShowHighestLightNeedPlant();
    ShowAverageLightNeed();
    ShowPercentOfAdoptedPlants();
}

void ShowLowestPricedPlant()
{
    List<Decimal> plantPrices = plants.Select(p => p.AskingPrice).ToList(); //grabs all the prices of all plants and lists them
    decimal lowestPlantPrice = plantPrices.Min(); //finds the lowest price out of all prices and stores it in a variable
    Plant lowestPricedPlant = plants.FirstOrDefault(p => p.AskingPrice == lowestPlantPrice); //runs that price against all plants (not just available plants can change in the future if needed)

    List<Plant> unSoldPlants = plants.Where(p => !p.Sold).ToList();
    List<Plant> availablePlants = unSoldPlants.Where(a => a.AvailableUntil >= DateTime.Now).ToList();

    Console.WriteLine($"{lowestPricedPlant.Species} is the lowest priced plant at {lowestPricedPlant.AskingPrice} dollars.\n");
}
void ShowNumberOfAvailablePlants()
{
    List<Plant> unSoldPlants = plants.Where(p => !p.Sold).ToList();
    List<Plant> availablePlants = unSoldPlants.Where(a => a.AvailableUntil >= DateTime.Now).ToList();

    int numberOfAvailablePlants = availablePlants.Count();

    Console.WriteLine($"We have {numberOfAvailablePlants} available plants right now.\n");
}
void ShowHighestLightNeedPlant()
{
    List<Int32> lightNeeds = plants.Select(p => p.LightNeeds).ToList();
   int highestLightNeedPlant =  lightNeeds.Max();
    List<Plant> highestLightNeededPlants = plants.Where(p => p.LightNeeds == highestLightNeedPlant).ToList();
    foreach (Plant plant in highestLightNeededPlants)
    {
        Console.WriteLine($"The {plant.Species} has the highest light need which is {plant.LightNeeds}.\n");
    }
}
void ShowAverageLightNeed()
{
    List<Int32> lightNeeds = plants.Select(p => p.LightNeeds).ToList();
    int totalLightNeed = 0;
    foreach (Int32 lightNeed in lightNeeds)
    {
        totalLightNeed += lightNeed; 
    }
    double averageLightNeed = 0;
    averageLightNeed = totalLightNeed / lightNeeds.Count;
    Console.WriteLine($"The Average Light Needs of all the plants is {averageLightNeed}.\n");
}
void ShowPercentOfAdoptedPlants()
{ 
    List<Plant> adoptedPlants = plants.Where(s => s.Sold).ToList();
    int numberOfAdoptedPlants = adoptedPlants.Count();

    double pointsOfTotalPlantsAdopted = (double)numberOfAdoptedPlants / plants.Count;
    double percentOfAdoptedPlants = pointsOfTotalPlantsAdopted * 100;
    Console.WriteLine($"{percentOfAdoptedPlants}% of plants have been sold!");

}
string PlantDetails(Plant plant)
{
    string plantString = plant.Species;

    return plantString;

}


Greeting();
ShowMenu();


