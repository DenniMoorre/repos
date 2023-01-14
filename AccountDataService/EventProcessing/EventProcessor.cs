using System;
using System.Text.Json;
using AutoMapper;
using AccountDataService.Data;
using AccountDataService.Dtos;
using AccountDataService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AccountDataService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.PassportPublished:
                    addPassport(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch(eventType.Event)
            {
                case "Passport_Published":
                    Console.WriteLine("--> Passport Published Event Detected");
                    return EventType.PassportPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addPassport(string passportPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IAccountDataRepo>();
                
                var passportPublishedDto = JsonSerializer.Deserialize<PassportPublishedDto>(passportPublishedMessage);

                try
                {
                    var pass = _mapper.Map<Passport>(passportPublishedDto);
                    if(!repo.ExternalPassportExists(pass.ExternalID))
                    {
                        repo.CreatePassport(pass);
                        repo.SaveChanges();
                        Console.WriteLine("--> Platform added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Platform already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        PassportPublished,
        Undetermined
    }
}