﻿namespace PokemonReviewApp.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PokemonReviewApp.Dto;
    using PokemonReviewApp.Interfaces;
    using PokemonReviewApp.Models;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());
            if(!ModelState.IsValid)
            {
                return BadRequest(string.Empty);
            }
            return Ok(countries);
        }

        [HttpGet("{countryId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int countryId)
        {
            if(!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            var country = _mapper.Map<CountryDto>(_countryRepository.getCountry(countryId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(country);
        }

        [HttpGet("owners/{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryByOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>(_countryRepository.getCountryByOwner(ownerId));
            if(!ModelState.IsValid)
            { 
                return BadRequest(ModelState); 
            }
            return Ok(country);
        }

        [HttpGet("ownersFromACountry/{countryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnersFromACountry(int countryId)
        {
            if(!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }
            var owners = _mapper.Map<List<OwnerDto>>(_countryRepository.GetOwnersFromACountry(countryId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owners);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
            {
                return BadRequest(ModelState);
            }

            var country = _countryRepository.GetCountries()
                                                    .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper())
                                                    .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countrytMap = _mapper.Map<Country>(countryCreate);

            if (!_countryRepository.CreateCountry(countrytMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{countryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null)
            {
                return BadRequest(ModelState);
            }

            if (countryId != updatedCountry.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_countryRepository.CountryExists(countryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var countryMap = _mapper.Map<Country>(updatedCountry);

            if (!_countryRepository.updateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
