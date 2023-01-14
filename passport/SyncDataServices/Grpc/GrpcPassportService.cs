using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using passport.Data;

namespace passport.SyncDataServices.Grpc
{
    public class GrpcPassportService : GrpcPassport.GrpcPassportBase
    {
        private readonly IPassportRepo _repository;
        private readonly IMapper _mapper;

        public GrpcPassportService(IPassportRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<PassportResponse> GetAllPassports(GetAllRequest request, ServerCallContext context)
        {
            var response = new PassportResponse();
            var passports = _repository.GetAllPassports();

            foreach(var pass in passports)
            {
                response.Passport.Add(_mapper.Map<GrpcPassportModel>(pass));
            }

            return Task.FromResult(response);
        }
    }
}