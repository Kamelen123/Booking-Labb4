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
    public class AppointmentController : ControllerBase
    {
        private IAppointment _appointment;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointment appointment, IMapper mapper)
        {
            _appointment = appointment;
            _mapper = mapper;
        }
        [HttpPost("AddAppointment")]
        public async Task<ActionResult<AppointmentDto>> AddAppointment(AppointmentDto newAppointmentDto)
        {
            try
            {
                if (newAppointmentDto == null)
                {
                    return BadRequest();
                }

                // Map the DTO to the entity
                var newAppointment = _mapper.Map<Appointment>(newAppointmentDto);

                // Add the entity to the database
                var createdAppoinment = await _appointment.Add(newAppointment);

                // Map the created entity back to DTO
                var createdAppoinmentDto = _mapper.Map<AppointmentDto>(createdAppoinment);

                return CreatedAtAction(nameof(GetAppoinment), new { createdAppoinmentDto.AppointmentId }, createdAppoinmentDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error posting data to the database.");
            }
        }
        [HttpGet("GetAppointmentById/{AppointmentId}")]
        public async Task<IActionResult> GetAppoinment(int AppointmentId)
        {
            try
            {
                var appointment = await _appointment.GetSingel(AppointmentId);
                var result = _mapper.Map<AppointmentDto>(appointment);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound($"Company with ID {AppointmentId} Not Found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("DeleteAppointment/{id:int}")]
        public async Task<ActionResult<Appointment>> DeleteAppointment(int id)
        {
            try
            {
                var deleteAppointment = await _appointment.GetSingel(id);
                if (deleteAppointment == null)
                {
                    return NotFound($"Appointment with ID {id} not found");
                }
                return await _appointment.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error When trying to Delete Data from Database.......");
            }

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var appointment = await _appointment.GetAll();
                var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointment);
                return Ok(appointmentDtos);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error to get Data from Database.......");
            }
        }
        [HttpPut("UpdateAppointment/{id:int}")]
        public async Task<ActionResult<AppointmentDto>> UpdateAppointment(int id, AppointmentDto appointmentDto)
        {
            try
            {
                if (id != appointmentDto.AppointmentId)
                {
                    return BadRequest("Appointment ID Does Not Match...");
                }

                var appointmentToUpdate = await _appointment.GetSingel(id);
                if (appointmentToUpdate == null)
                {
                    return NotFound($"Appointment With ID {id} Not Found, No Update Occurred....");
                }

                // Map the AppointmentDto to the entity
                var updatedAppointment = _mapper.Map<Appointment>(appointmentDto);

                var result = await _appointment.Update(updatedAppointment);

                // Map the updated entity back to DTO
                var updatedAppointmentDto = _mapper.Map<AppointmentDto>(result);

                return updatedAppointmentDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Occurred when Trying to Update data in Database...");
            }
        }
        [HttpGet("GetAppointmentChanges")]
        public async Task<IActionResult> GetAppointmentChanges()
        {
            try
            {
                var appointmentChanges = await _appointment.GetAppointmentChanges(/*appointmentId*/);
                if (appointmentChanges == null || !appointmentChanges.Any())
                {
                    return NotFound($"No changes found for appointments");
                }
                return Ok(appointmentChanges);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving changes from the database.");
            }
        }
    }
}
