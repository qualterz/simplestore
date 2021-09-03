using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Web.Areas.Administration.ViewModels
{
    public class CharacteristicViewModel
    {
        public int CharacteristicId { get; set; }
        public int ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
