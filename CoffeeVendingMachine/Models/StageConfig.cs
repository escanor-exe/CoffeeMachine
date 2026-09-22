namespace CoffeeVendingMachine.Models
{
    public class StageConfig
    {
        public StageConfig(string name, TimeSpan baseDuration)
        {
            Name = name;
            BaseDuration = baseDuration;
        }

        public string Name { get; set; }

        public TimeSpan BaseDuration { get; set; }
    }
}
