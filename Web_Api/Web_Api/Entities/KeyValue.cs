using System.ComponentModel.DataAnnotations;

namespace Web_Api.Entities
{
    public class KeyValue
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
