﻿using System.Collections.Generic;
using Core.Booking.TheRoom;
using Room = Inventory.HotelRoom.Room;

namespace Inventory.Services.Repositories
{
    public interface IRoomRepository
    {
        List<Room> Get(RoomType type);
        Room Get(int id);
    }
}