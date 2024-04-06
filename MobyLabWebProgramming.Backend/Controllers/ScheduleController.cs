using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ScheduleController : AuthorizedController
{
    private readonly IScheduleService _scheduleService;
    public ScheduleController(IScheduleService scheduleService, IUserService userService) : base(userService)
    {
        _scheduleService = scheduleService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ScheduleDTO>>> GetById([FromRoute] Guid id) {
        return this.FromServiceResponse(await _scheduleService.GetScheduleById(id));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ScheduleDTO schedule)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _scheduleService.AddSchedule(schedule, currentUser.Result));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ScheduleUpdateDTO schedule)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _scheduleService.UpdateSchedule(schedule, currentUser.Result));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _scheduleService.DeleteSchedule(id, currentUser.Result));
    }
    
    [Authorize]
    [HttpGet("{classroomId:guid}")]
    public async Task<ActionResult<RequestResponse<List<GetScheduleForClassDTO>>>> GetSchedulesForClass([FromRoute] Guid classroomId)
    {
        return this.FromServiceResponse(await _scheduleService.GetSchedulesForClass(classroomId));
    }
    
    [Authorize]
    [HttpGet("{teacherId:guid}")]
    public async Task<ActionResult<RequestResponse<List<GetScheduleForTeacherDTO>>>> GetSchedulesForTeacher([FromRoute] Guid teacherId)
    {
        return this.FromServiceResponse(await _scheduleService.GetSchedulesForTeacher(teacherId));
    }
}