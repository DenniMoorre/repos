using System;
using System.Collections.Generic;
using AutoMapper;
using AccountDataService.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using passport;
using AccountDataService.SyncDataServices.Grpc;

namespace AccountDataService.SyncDataServices.Grpc
{
    public class PassportDataClient : IPassportDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PassportDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Passport> ReturnAllPassports()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcPassport"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcPassport"]);
            var client = new GrpcPassport.GrpcPassportClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllPassports(request);
                return _mapper.Map<IEnumerable<Passport>>(reply.Passport);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}