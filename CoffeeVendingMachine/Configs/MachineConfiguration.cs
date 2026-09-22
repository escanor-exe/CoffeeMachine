using CoffeeVendingMachine.Models;
using CoffeeVendingMachine.Models.Enums;

namespace CoffeeVendingMachine.Configs
{
    public static class MachineConfiguration
    {
        public static readonly Dictionary<CoffeeType, List<StageConfig>> Recipes = new()
    {
        {
            CoffeeType.Espresso, new List<StageConfig> {
                new("Grinding", TimeSpan.FromSeconds(5)),
                new("Heating", TimeSpan.FromSeconds(3)),
                new("Extraction", TimeSpan.FromSeconds(15)),
                new("Serving", TimeSpan.FromSeconds(2)),
            }
        },
        {
            CoffeeType.Americano, new List<StageConfig> {
                new("Grinding", TimeSpan.FromSeconds(5)),
                new("Heating", TimeSpan.FromSeconds(5)),
                new("Extraction", TimeSpan.FromSeconds(15)),
                new("Mixing", TimeSpan.FromSeconds(5)),
            }
        },
        {
            CoffeeType.Cappuccino, new List<StageConfig> {
                new("Grinding", TimeSpan.FromSeconds(5)),
                new("Heating", TimeSpan.FromSeconds(4)),
                new("Extraction", TimeSpan.FromSeconds(15)),
                new("Milk Preparation", TimeSpan.FromSeconds(12)),
                new("Mixing", TimeSpan.FromSeconds(4)),
                new("Serving", TimeSpan.FromSeconds(2)),
            }
        },
        {
            CoffeeType.Latte, new List<StageConfig> {
                new("Grinding", TimeSpan.FromSeconds(5)),
                new("Heating", TimeSpan.FromSeconds(4)),
                new("Extraction", TimeSpan.FromSeconds(15)),
                new("Milk Preparation", TimeSpan.FromSeconds(8)),
                new("Mixing", TimeSpan.FromSeconds(5)),
                new("Serving", TimeSpan.FromSeconds(2)),
            }
        }
    };

        public static TimeSpan CalculateTotalTime(CoffeeType type, CoffeeSize size, CoffeeStrength strength)
        {
            if (!Recipes.TryGetValue(type, out var stages))
                return TimeSpan.FromSeconds(30);

            double sizeMultiplier = size switch
            {
                CoffeeSize.Medium => 1.2,
                CoffeeSize.Large => 1.5,
                _ => 1.0
            };

            double strengthMultiplier = strength switch
            {
                CoffeeStrength.Strong => 1.4,
                CoffeeStrength.Normal => 1.0,
                _ => 0.8
            };

            double totalSeconds = 0;
            foreach (var stage in stages)
            {
                double duration = stage.BaseDuration.TotalSeconds;

                if (stage.Name == "Extraction" || stage.Name == "Milk Preparation" || stage.Name == "Mixing")
                {
                    duration *= sizeMultiplier;
                }

                if (stage.Name == "Grinding" || stage.Name == "Extraction")
                {
                    duration *= strengthMultiplier;
                }

                totalSeconds += duration;
            }

            return TimeSpan.FromSeconds(Math.Round(totalSeconds));
        }
    }

}
