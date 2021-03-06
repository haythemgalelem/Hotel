﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Markers;
using Core.Store;
using MongoDB.Driver.Linq;

namespace EventsStore.StreamReader
{
    public class EventStore : IEventStore
    {
        public List<IEvent> LoadStream(Guid streamId)
        {
            var db = new MongoDbFactory().CreateDb(new Configuration().MongoDbName);

            var events = db.GetCollection<EventDescriptor>("Events");

            var stream = events.AsQueryable().Where(r => r.StreamId == streamId);

            return stream.ToList().Select(e => e.Event).ToList();
        }

        public void AppendToStream(Guid streamId, IEvent ev)
        {
            var db = new MongoDbFactory().CreateDb(new Configuration().MongoDbName);

            var events = db.GetCollection<EventDescriptor>("Events");

            events.Save(new EventDescriptor(streamId,ev));

        }

        public void AppendToStream(Guid streamId, List<IEvent> evs)
        {
            var db = new MongoDbFactory().CreateDb(new Configuration().MongoDbName);

            var events = db.GetCollection<EventDescriptor>("Events");

            evs.ForEach(ev => events.Save(new EventDescriptor(streamId,ev)));
        }
    }
}