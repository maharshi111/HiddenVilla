using System.ComponentModel.DataAnnotations;

namespace DataAccess.Data
{
    public class HotelRoom
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public double RegularRate { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        //Have a look at HotelRoomDTO , so what are DTO, DTO stands for  Data Transfer Object. It's a design pattern used to transfer data
        //between software application subsystems or layers. The primary goal of a DTO is to encapsulate data and transport it between different
        //parts of a system efficiently and in a straightforward manner.
        //See when we will be using API than we should not pass all the  field  in this class as a user might not need all the fields for deleting or 
        //creating a new room so at that time we will only pass limited fields that are important for deleting or  creating (have a look at HotelRoomDTO )
        //It only contains the important fields, so that our API response are faster 

        //public double TotalDays { get; set; }

        //public double TotalAmount { get; set; }
        public string Email { get; set; }

        public virtual ICollection<HotelRoomImage> HotelRoomImages { get; set; }
        // when we are adding a virtual property there is no need to to do migration
    }
}
