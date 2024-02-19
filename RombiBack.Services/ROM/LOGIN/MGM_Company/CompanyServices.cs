using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using RombiBack.Entities.ROM.LOGIN.Company;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using RombiBack.Repository.ROM.LOGIN.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN.Company
{
    public class CompanyServices : ICompanyServices
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IDistributedCache _cache;

        private IMapper _mapper;

        public CompanyServices(IDistributedCache cache, ICompanyRepository companytRepository, IMapper mapper)
        {
            _cache = cache;
            _companyRepository = companytRepository;
            _mapper = mapper;
        }
        public async Task<List<CompanyDTO>> GetCompany()
        {
            // Verificar si los datos están en caché
            var cachedCompanies = await _cache.GetStringAsync("AllCompanies");

            if (cachedCompanies != null)
            {
                // Los datos están en caché, los deserializamos y los devolvemos
                var companiesDTO = JsonSerializer.Deserialize<List<CompanyDTO>>(cachedCompanies);
                return companiesDTO;
            }
            else
            {
                // Los datos no están en caché, los obtenemos de la base de datos
                var companiesFromDatabase = await _companyRepository.GetCompany();

                // Mapeamos los datos al DTO
                var companiesDTO = _mapper.Map<List<CompanyDTO>>(companiesFromDatabase);

                // Serializamos los datos antes de guardarlos en caché
                var serializedCompanies = JsonSerializer.Serialize(companiesDTO);

                // Guardamos los datos en caché
                await _cache.SetStringAsync("AllCompanies", serializedCompanies);

                // Devolvemos los datos obtenidos
                return companiesDTO;
            }
        }

    }
}
