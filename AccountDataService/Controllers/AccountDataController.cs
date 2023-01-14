using System;
using System.Collections.Generic;
using AutoMapper;
using AccountDataService.Data;
using AccountDataService.Dtos;
using AccountDataService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountDataService.Controllers
{
    [Route("api/c/passports/{passportId}/[controller]")]
    [ApiController]
    public class AccountDataController : ControllerBase
    {
        private readonly IAccountDataRepo _repository;
        private readonly IMapper _mapper;

        public AccountDataController(IAccountDataRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccountDataReadDto>> GetAccountDataForPassport(int passportId)
        {
            Console.WriteLine($"--> Hit GetAccountDataForPassport: {passportId}");

            if (!_repository.PassportExits(passportId))
            {
                return NotFound();
            }

            var accountData = _repository.GetAccountDataForPassport(passportId);

            return Ok(_mapper.Map<IEnumerable<AccountDataReadDto>>(accountData));
        }

        [HttpGet("{acccountDataId}", Name = "GetAccountDataForPassport")]
        public ActionResult<AccountDataReadDto> GetAccountDataForPassport(int passportId, int accountDataId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform: {passportId} / {accountDataId}");

            if (!_repository.PassportExits(passportId))
            {
                return NotFound();
            }

            var accountData = _repository.GetAccountData(passportId, accountDataId);

            if(accountData == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountDataReadDto>(accountData));
        }

        [HttpPost]
        public ActionResult<AccountDataReadDto> CreateAccountDataForPAssport(int passportId, AccountDataCreateDto accountDataDto)
        {
             Console.WriteLine($"--> Hit CreateCommandForPlatform: {passportId}");

            if (!_repository.PassportExits(passportId))
            {
                return NotFound();
            }

            var accountData = _mapper.Map<AccountData>(accountDataDto);

            _repository.CreateAccountData(passportId, accountData);
            _repository.SaveChanges();

            var AccountDataReadDto = _mapper.Map<AccountDataReadDto>(accountData);

            return CreatedAtRoute(nameof(GetAccountDataForPassport),
                new {passportId = passportId, AccountDataId = AccountDataReadDto.Id}, AccountDataReadDto);
        }

    }
}