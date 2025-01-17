﻿using AutoMapper;
using Business.Repository.Irepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models1;

namespace Business.Repository
{
    public class HotelImageRepository : IHotelImageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelImageRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO)
        {
            var image = _mapper.Map<HotelRoomImageDTO, HotelRoomImage>(imageDTO);
            await _db.HotelRoomImages.AddAsync(image);
            return await _db.SaveChangesAsync();

        }

        public async Task<int> DeleteHotelImageByImageUrl(string imageUrl)
        {
            var allImages = await _db.HotelRoomImages.FirstOrDefaultAsync
                                 (x => x.RoomImageUrl.ToLower() == imageUrl.ToLower());
            _db.HotelRoomImages.Remove(allImages);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
        {
            var image = await _db.HotelRoomImages.FindAsync(imageId);
            _db.HotelRoomImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
        {
            var image = await _db.HotelRoomImages.FindAsync(roomId);
            _db.HotelRoomImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId)
        {
            return _mapper.Map<IEnumerable<HotelRoomImage>, IEnumerable<HotelRoomImageDTO>>(await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync());

        }
    }
}
