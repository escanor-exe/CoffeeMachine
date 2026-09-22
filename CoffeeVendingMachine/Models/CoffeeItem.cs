namespace CoffeeVendingMachine.Models
{
    public class CoffeeItem
    {
        public CoffeeItem(string type, string size, string strength)
        {
            Type = type;
            Size = size;
            Strength = strength;
        }

        public string Type { get; set; }

        public string Size { get; set; }

        public string Strength { get; set; }
    }
}
