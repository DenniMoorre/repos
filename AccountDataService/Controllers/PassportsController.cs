using System;
using System.Collections.Generic;
using AutoMapper;
using AccountDataService.Data;
using AccountDataService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AccountDataService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PassportsController : ControllerBase
    {
        private readonly IAccountDataRepo _repository;
        private readonly IMapper _mapper;

        public PassportsController(IAccountDataRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PassportReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms from CommandsService");

            var passportItems = _repository.GetAllPassports();

            return Ok(_mapper.Map<IEnumerable<PassportReadDto>>(passportItems));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controler");
        }
    }
}