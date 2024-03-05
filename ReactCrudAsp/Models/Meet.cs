using System.ComponentModel.DataAnnotations;

namespace ReactCrudAsp.Models
{
    public class Meet
    {
        [Key]
        public int id { get; set; }
       
        public string toplantiadi { get; set; }
        
        public string aciklama {  get; set; }

        [DataType(DataType.Date)]
        public DateTime baslangic { get; set; }
        public DateTime son { get; set; }
    }
}
