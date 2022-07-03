namespace SimpleStore.Application.Models
{
    public class ItemCharacteristicModel
    {
        public int ItemId { get; set; }
        public int CharacteristicId { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
