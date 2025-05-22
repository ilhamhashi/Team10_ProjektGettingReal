namespace DATApp.MVVM.Model.Classes
{
    public class Module
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Number},{Name},{Description}";
        }

        public static Module FromString(string input)
        {
            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            return new Module
            {
                Number = parts[0],
                Name = parts[1],
                Description = parts[2]
            };
        }
    }
}
