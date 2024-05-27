using System.ComponentModel.DataAnnotations;

namespace Models1
{
    public class HotelRoomDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "pls enter the room name")]  // if a user doesn't enter the room name then the following error will be displayed 
        [StringLength(25, ErrorMessage = "Room name must be at most 25 characters long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "pls enter the room occupancy")]
        public int Occupancy { get; set; }
        [Required]
        [Range(1, 3000, ErrorMessage = "Regular rate must between 1 and 3000")]
        public double RegularRate { get; set; }
        public string Details { get; set; }
        public string SqFt { get; set; }

        public double TotalDays { get; set; }
        public double TotalAmount { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address")]

        public string Email { get; set; }

        public virtual ICollection<HotelRoomImageDTO> HotelRoomImages { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}
