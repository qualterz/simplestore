using System.Collections.Generic;

namespace SimpleStore.Core.Entities
{
    public class Characteristic : Entity
    {
        public int CharacteristicId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual IList<ItemCharacteristic> ItemCharacteristics { get; set; }
    }
}