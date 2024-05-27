using Models1;

namespace Business.Repository.Irepository
{
    public interface IHotelImageRepository
    {
        public Task<int> CreateHotelRoomImage(HotelRoomImageDTO image);
        public Task<int> DeleteHotelRoomImageByImageId(int imageId);
        public Task<int> DeleteHotelRoomImageByRoomId(int roomId);
        public Task<int> DeleteHotelImageByImageUrl(string imageUrl);
        public Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId);

    }
}
