﻿using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Mappers;
using RombiBack.Entities.ROM.LOGIN.Country;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using RombiBack.Repository.ROM.LOGIN.MGM_Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN.MGM_Country
{
    public class CountryServices : ICountryServices
    {
        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        public CountryServices(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public Task<Country> Add(Country entity)
        {
            throw new NotImplementedException();
        }

        public Task<Country> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Country>> GetAll()
        {
            return await _countryRepository.GetAll();
        }

        public Task<bool> Remove(Country entity)
        {
            throw new NotImplementedException();
        }

        public Task<Country> Update(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}
