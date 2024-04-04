using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

public class ScheduleController : AuthorizationController
{
    readonly IScheduleService _scheduleService;
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
        return this.FromServiceResponse(await _scheduleService.AddSchedule(schedule));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ScheduleUpdateDTO schedule)
    {
        return this.FromServiceResponse(await _scheduleService.UpdateSchedule(schedule));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _scheduleService.DeleteSchedule(id));
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