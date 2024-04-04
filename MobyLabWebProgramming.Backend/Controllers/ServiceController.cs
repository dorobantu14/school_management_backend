using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

public class ServiceController : AuthorizationController
{
    readonly IScheduleService _scheduleService;
    public ServiceController(IScheduleService scheduleService, IUserService userService) : base(userService)
    {
        _scheduleService = scheduleService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ScheduleDTO>>> GetScheduleById([FromRoute] Guid id) {
        return this.FromServiceResponse( await _scheduleService.GetScheduleById(id));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> AddSchedule([FromBody] ScheduleDTO schedule)
    {
        return this.FromServiceResponse(await _scheduleService.AddSchedule(schedule));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> UpdateSchedule([FromBody] ScheduleUpdateDTO schedule)
    {
        return this.FromServiceResponse(await _scheduleService.UpdateSchedule(schedule));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> DeleteSchedule([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _scheduleService.DeleteSchedule(id));
    }
}