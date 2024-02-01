using AutoMapper;
using RombiBack.Entities.ROM.LOGIN.Company;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using RombiBack.Repository.ROM.LOGIN.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN.Company
{
    public class CompanyServices : ICompanyServices
    {
        private readonly ICompanyRepository _companyRepository;

        private  IMapper _mapper;

        public CompanyServices(ICompanyRepository companytRepository, IMapper mapper)
        {
            _companyRepository = companytRepository;
            _mapper = mapper;
        }
        public async Task<List<CompanyDTO>> GetCompany()
        {
            var company = await _companyRepository.GetCompany();
            return _mapper.Map<List<CompanyDTO>>(company);        }
        }
    }
