using AutoMapper;
using DataAccess.Data;
using Models1;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>();
            CreateMap<HotelRoom, HotelRoomDTO>();
            CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();
            // ReverseMap() property helps to achieve reversing to and fro that is from <HotelRoomImage to HotelRoomImageDTO and vice versa
            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ReverseMap();


        }
    }
}
