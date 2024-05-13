using DashBoard.API.Models.DTO;
using DashBoard.API.Repositories.Implementation;
using DashBoard.API.Repositories.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository notificationRepository;
        public NotificationController(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        [HttpGet("GetEmployeeAnniversary")]
        public async Task<ActionResult<IEnumerable<NotificationEmployee>>> GetEmployeeAnniversary([FromQuery] int daysLimit)
        {
            List<NotificationEmployee> view = new List<NotificationEmployee>();
            try
            {
                var employments = await notificationRepository.GetEmployeesAnniversaryInfo(daysLimit);
                foreach (var employee in employments)
                {
                    view.Add(new NotificationEmployee
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("CountVacationEmployee")]
        public async Task<ActionResult<IEnumerable<NotificationEmployee>>> GetVacationEmployeeThisYear([FromQuery] int minimumDays = 12)
        {
            List<NotificationEmployee> view = new List<NotificationEmployee>();
            try
            {
                var employments = await notificationRepository.GetEmployeesWithAccumulatedVacationDays(minimumDays);
                foreach (var employee in employments)
                {
                    view.Add(new NotificationEmployee
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetEmployeeBirthday")]
        public async Task<ActionResult<IEnumerable<NotificationEmployee>>> GetEmployeeBirthday([FromQuery] int daysLimit = 300)
        {
            List<NotificationEmployee> view = new List<NotificationEmployee>();
            try
            {
                var employments = await notificationRepository.GetEmployeesWithBirthdaysThisMonth(daysLimit);
                foreach (var employee in employments)
                {
                    view.Add(new NotificationEmployee
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getAllVacation")]
        public async Task<ActionResult<IEnumerable<NotificationEmployee>>> GetAllVacation()
        {
            List<NotificationEmployee> view = new List<NotificationEmployee>();
            try
            {
                var employmentAccumulatedVacation = await notificationRepository.GetEmployeesWithAccumulatedVacationDays(12);
                var employmentWithBirthday = await notificationRepository.GetEmployeesWithBirthdaysThisMonth(15);
                var employmentAnniversary = await notificationRepository.GetEmployeesAnniversaryInfo(15);

                foreach (var employee in employmentAccumulatedVacation)
                {
                    view.Add(new NotificationEmployee
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                foreach (var employee in employmentWithBirthday)
                {
                    view.Add(new NotificationEmployee
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                foreach (var employee in employmentAnniversary)
                {
                    view.Add(new NotificationEmployee
                    {
                        FullName = employee.FullName,
                        Department = employee.Department,
                        Content = employee.Content,
                    });
                }
                return Ok(view);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
