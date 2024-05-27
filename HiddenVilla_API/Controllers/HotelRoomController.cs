using Business.Repository.Irepository;
using Microsoft.AspNetCore.Mvc;
using Models1;
using System.Globalization;

namespace HiddenVilla_API.Controllers
{
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }

        // [Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(string checkInDate = null, string checkOutDate = null)
        {
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters  needed to supply"
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. valid format will be MM/dd/yyyy"
                });

            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtcheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkOutDate date format. valid format will be MM/dd/yyyy"
                });

            }


            var allRooms = _hotelRoomRepository.GetAllHotelRooms(checkInDate, checkOutDate);
            return Ok(allRooms);
        }
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoom(int? roomId, string checkInDate = null, string checkOutDate = null)
        {
            if (roomId == null)
            {
                return BadRequest(new ErrorModel
                {
                    Title = "",
                    ErrorMessage = "Invalid RoomID",
                    StatusCode = StatusCodes.Status400BadRequest

                });
            }


            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters  needed to supply"
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. valid format will be MM/dd/yyyy"
                });

            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtcheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid checkOutDate date format. valid format will be MM/dd/yyyy"
                });

            }


            var roomDetails = await _hotelRoomRepository.GetHotelRoom(roomId.Value, checkInDate, checkOutDate);
            if (roomDetails == null)
            {
                return BadRequest(new ErrorModel
                {
                    Title = "",
                    ErrorMessage = "No rooms are there",
                    StatusCode = StatusCodes.Status404NotFound

                });
            }
            return Ok(roomDetails);
        }
    }
}
