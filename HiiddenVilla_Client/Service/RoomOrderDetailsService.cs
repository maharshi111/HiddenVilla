using HiiddenVilla_Client.Service.IService;
using Models1;

namespace HiiddenVilla_Client.Service
{
    public class RoomOrderDetailsService : IRoomOrderDetailsService
    {
        private readonly HttpClient _client;
        //hover over HttpClient  to known it's use
        public RoomOrderDetailsService(HttpClient client)
        {
            _client = client;
        }
        public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details)
        {
            throw new NotImplementedException();
        }

        public Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details)
        {
            throw new NotImplementedException();
        }
    }
}
