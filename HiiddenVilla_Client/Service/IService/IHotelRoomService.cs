using Models1;

namespace HiiddenVilla_Client.Service.IService
{
    public interface IHotelRoomService
    {
        public Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate);
        public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate);
    }
}

