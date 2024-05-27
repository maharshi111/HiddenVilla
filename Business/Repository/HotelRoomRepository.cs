using AutoMapper;
using Business.Repository.Irepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models1;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            hotelRoom.UpdatedDate = DateTime.Now;
            hotelRoom.UpdatedBy = "";
            var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public IEnumerable<HotelRoomDTO> GetAllHotelRooms(string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs = _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(_db.HotelRooms.Include(x => x.HotelRoomImages));
                return (hotelRoomDTOs);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(await _db.HotelRooms.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));
                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()));
                    return hotelRoom;
                }

                else
                {
                    HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != roomId));
                    return hotelRoom;
                }




            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    //valid
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdatedDate = DateTime.Now;
                    room.UpdatedBy = "";
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    // invalid 
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<int> DeleteHotelRoom(int roomId)
        {

            var roomDetails = await _db.HotelRooms.FindAsync(roomId);
            if (roomDetails != null)
            {
                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;


        }

        public async Task<HotelRoomDTO> IsEmailUnique(string name)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(await _db.HotelRooms.FirstOrDefaultAsync(x => x.Email.ToLower() == name.ToLower()));
                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;
            }

        }



    }
}
