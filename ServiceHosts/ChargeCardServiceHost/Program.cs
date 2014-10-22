﻿using System;
using System.Collections.Generic;
using Core.Payments;
using MessageTransport.Channels;
using MessageTransport.Receivers;
using MessageTransport.Sender;

namespace ChargeCardServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {

            var messageExchanges = new List<MessageExchange>()
                {
                    new MessageExchange(typeof (ChargeCard).Name, "hotel.card.charges"),
                };

            new MessagingInfrastructure()
                .SetExchanges(messageExchanges)
                .SetMonitoring("hotel.process.monitoring");

            new MessageSender(messageExchanges).Send(new ChargeCard() { Id = Guid.NewGuid() });

            new Subscriber(new ChargeCard(), null).Start();
            
        }
    }
}
