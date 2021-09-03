namespace SimpleStore.Core.Entities
{
    public class ItemCharacteristic : Entity
    {
        public int ItemId { get; set; }
        public int CharacteristicId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Characteristic Characteristic { get; set; }
    }
}
