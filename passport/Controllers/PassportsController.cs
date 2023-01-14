using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using passport.AsyncDataServices;
using passport.Data;
using passport.Dtos;
using passport.Models;
using passport.SyncDataServices;
using passport.SyncDataServices.Http;

namespace passport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportsController : ControllerBase
    {
        private readonly IPassportRepo _repository;
        private readonly IMapper _mapper;
        private readonly IAccountDataClient _accountDataClient;
        
        private readonly IMessageBusClient _messageBusClient;
        

        public PassportsController(
            IPassportRepo repository, 
            IMapper mapper,
            IAccountDataClient accountDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _accountDataClient = accountDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PassportReadDto>> GetPassports()
        {
            Console.WriteLine("--> Getting Passports....");

            var passportItem = _repository.GetAllPassports();

            return Ok(_mapper.Map<IEnumerable<PassportReadDto>>(passportItem));
        }

        [HttpGet("{id}", Name = "GePassportById")]
        public ActionResult<PassportReadDto> GetPassportById(int id)
        {
            var passportItem = _repository.GetPassportById(id);
            if (passportItem != null)
            {
                return Ok(_mapper.Map<PassportReadDto>(passportItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PassportReadDto>> CreatePassport(PassportCreateDto passportCreateDto)
        {
            var passportModel = _mapper.Map<Passport>(passportCreateDto);
            _repository.CreatePassport(passportModel);
            _repository.SaveChange();

            var passportReadDto = _mapper.Map<PassportReadDto>(passportModel);

            // Send Sync Message
            try
            {
                await _accountDataClient.SendPassportToAccount(passportReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            //Send Async Message
            try
            {
                var passportPublishedDto = _mapper.Map<PassportPublishedDto>(passportReadDto);
                passportPublishedDto.Event = "Passport_Published";
                _messageBusClient.PublishNewPassport(passportPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetPassportById), new { Id = passportReadDto.Id}, passportReadDto);
        }
   
    
    }
}
