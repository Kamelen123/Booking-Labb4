using AutoMapper;
using Booking_Labb4.Data.Dto;
using Booking_Labb4.Services;
using BookingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Labb4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer _customer;
        private readonly IMapper _mapper;

        public CustomerController(IMapper mapper, ICustomer customer)
        {
            _customer = customer;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customer = await _customer.GetAll();
                var customerDto = _mapper.Map<List<CustomerDto>>(customer);
                return Ok(customerDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error to get Data from Database.......");
            }
        }

        [HttpGet("GetCustomerById/{CustomerId}")]
        public async Task<IActionResult> GetCustomer(int CustomerId)
        {
            try
            {
                var customer = await _customer.GetSingel(CustomerId);
                var result = _mapper.Map<CustomerDto>(customer);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound($"Customer with ID {CustomerId} Not Found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("AddCompany")]
        public async Task<ActionResult<AddCustomerDto>> AddCustomer(AddCustomerDto newCustomerDto)
        {
            try
            {
                if (newCustomerDto == null)
                {
                    return BadRequest();
                }

                // Map the DTO to the entity
                var newCustomer = _mapper.Map<Customer>(newCustomerDto);

                // Add the entity to the database
                var createdCustomer = await _customer.Add(newCustomer);

                // Map the created entity to CustomerDto that shows id
                var createdCustomerDto = _mapper.Map<CustomerDto>(createdCustomer);

                return CreatedAtAction(nameof(GetCustomer), new { createdCustomerDto.CustomerId }, createdCustomerDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error posting data to the database.");
            }
        }
        [HttpDelete("DeleteCustomer/{id:int}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            try
            {
                var deleteCustomer = await _customer.GetSingel(id);
                if (deleteCustomer == null)
                {
                    return NotFound($"Customer with ID {id} not found");
                }
                return await _customer.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error When trying to Delete Data from Database.......");
            }

        }
        [HttpPut("Update/{id:int}")]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer(int id, CustomerDto customerDto)
        {
            try
            {
                if (id != customerDto.CustomerId)
                {
                    return BadRequest("Customer Id Does Not Match...");
                }

                var customerToUpdate = await _customer.GetSingel(id);
                if (customerToUpdate == null)
                {
                    return NotFound($"Customer With ID {id} Not Found, No Update Occurred....");
                }

                // Map the CustomerDto to the entity
                var updatedCustomer = _mapper.Map<Customer>(customerDto);

                var result = await _customer.Update(updatedCustomer);

                // Map the updated entity back to DTO
                var updatedCustomerDto = _mapper.Map<CustomerDto>(result);

                return updatedCustomerDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Occurred when Trying to Update data in Database...");
            }
        }
        [HttpGet("AppointmentInfo")]
        public async Task<IActionResult> GetCustomerAppointments([FromQuery] int customerId)
        {
            if (customerId ==null)
            {
                return BadRequest("Invalid customer ID, year, or month.");
            }

            var customerInfo = await _customer.CustomerAppointmentInfo(customerId);

            if (!customerInfo.Any())
            {
                return NotFound("No appointments found for the specified customer ID.");
            }

            return Ok(customerInfo);
        }
        [HttpGet("CustomerBookingsByDate")]
        public async Task<IActionResult> GetAppointmentsByMonth([FromQuery] int year, [FromQuery] int month)
        {
            if (year <= 0 || month <= 0 || month > 12)
            {
                return BadRequest("Invalid year or month.");
            }

            var customerList = await _customer.SearchByMonth(year, month);

            if (!customerList.Any())
            {
                return NotFound("No appointments found for the specified month.");
            }

            return Ok(customerList);
        }
        [HttpGet("{customerId}/HoursPerMonth")]
        public async Task<IActionResult> GetHours([FromRoute] int customerId, [FromQuery] int year, [FromQuery] int month)
        {
            if ( customerId <= 0|| year <= 0 || month <= 0 || month > 12)
            {
                return BadRequest("Invalid customer ID, year or month.");
            }

            var customer = await _customer.GetCustomerHours(customerId, year, month);

            if (customer ==null)
            {
                return NotFound("No appointments found for the specified month.");
            }

            return Ok(customer);
        }
    }
}
