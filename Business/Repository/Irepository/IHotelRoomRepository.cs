using Models1;

namespace Business.Repository.Irepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO);
        public Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDate = null, string checkOutDate = null);
        public Task<int> DeleteHotelRoom(int roomId);
        public IEnumerable<HotelRoomDTO> GetAllHotelRooms(string checkInDate = null, string checkOutDate = null);

        public Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0);

        public Task<HotelRoomDTO> IsEmailUnique(string name);

    }
}
