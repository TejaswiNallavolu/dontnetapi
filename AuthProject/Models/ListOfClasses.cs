using System.ComponentModel.DataAnnotations;

namespace AuthProject.Models
{
    public class ListOfClasses
    {
        [Key]
        public string ListOfClassesID { get; set; }
        public string ClassName { get; set; }
        public string Image { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
    }
}
