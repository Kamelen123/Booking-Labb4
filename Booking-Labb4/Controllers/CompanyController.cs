﻿using AutoMapper;
using Booking_Labb4.Data.Dto;
using Booking_Labb4.Repository;
using Booking_Labb4.Services;
using BookingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Booking_Labb4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompany _company;
        private readonly IMapper _mapper;

        public CompanyController(ICompany company, IMapper mapper)
        {
            _company = company;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var companies = await _company.GetAll();
                var companyDtos = _mapper.Map<List<CompanyDto>>(companies);
                return Ok(companyDtos);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error to get Data from Database.......");
            }
        }
        [HttpGet("GetCompanyById/{CompanyId}")]
        public async Task<IActionResult> GetCompany(int CompanyId)
        {
            try
            {
                var company = await _company.GetSingel(CompanyId);
                var result = _mapper.Map<CompanyDto>(company);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound($"Company with ID {CompanyId} Not Found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("AddCompany")]
        public async Task<ActionResult<CompanyDto>> AddCompany(CompanyDto newCompanyDto)
        {
            try
            {
                if (newCompanyDto == null)
                {
                    return BadRequest();
                }

                // Map the DTO to the entity
                var newCompany = _mapper.Map<Company>(newCompanyDto);

                // Add the entity to the database
                var createdCompany = await _company.Add(newCompany);

                // Map the created entity back to DTO
                var createdCompanyDto = _mapper.Map<CompanyDto>(createdCompany);

                return CreatedAtAction(nameof(GetCompany), new { createdCompanyDto.CompanyId }, createdCompanyDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error posting data to the database.");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            try
            {
                var deleteCompany = await _company.GetSingel(id);
                if (deleteCompany == null)
                {
                    return NotFound($"Company with ID {id} not found");
                }
                return await _company.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error When trying to Delete Data from Database.......");
            }

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CompanyDto>> UpdateCompany(int id, CompanyDto companyDto)
        {
            try
            {
                if (id != companyDto.CompanyId)
                {
                    return BadRequest("Company ID Does Not Match...");
                }

                var companyToUpdate = await _company.GetSingel(id);
                if (companyToUpdate == null)
                {
                    return NotFound($"Company With ID {id} Not Found, No Update Occurred....");
                }

                // Map the CompanyDto to the entity
                var updatedCompany = _mapper.Map<Company>(companyDto);

                var result = await _company.Update(updatedCompany);

                // Map the updated entity back to DTO
                var updatedCompanyDto = _mapper.Map<CompanyDto>(result);

                return updatedCompanyDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Occurred when Trying to Update data in Database...");
            }
        }
        [HttpGet("date")]
        public async Task<IActionResult> GetAllByDate([FromQuery] string date)
        {
            if (!DateOnly.TryParse(date, out var parsedDate))
            {
                return BadRequest("Invalid date format. Please use 'yyyy-MM-dd'.");
            }

            var result = await _company.Search(parsedDate);

            if (!result.Any())
            {
                return NotFound("Not Found");
            }

            return Ok(result);
        }
        [HttpGet("{companyId}/appointmentsByDate")]
        public async Task<IActionResult> GetAppointmentsByCompanyIdAndMonth([FromRoute] int companyId, [FromQuery] int year, [FromQuery] int month)
        {
            if (companyId <= 0 || year <= 0 || month <= 0 || month > 12)
            {
                return BadRequest("Invalid company ID, year, or month.");
            }

            var appointmentDtos = await _company.Test(companyId, year, month);

            if (!appointmentDtos.Any())
            {
                return NotFound("No appointments found for the specified company ID and month.");
            }

            return Ok(appointmentDtos);
        }
    }
}