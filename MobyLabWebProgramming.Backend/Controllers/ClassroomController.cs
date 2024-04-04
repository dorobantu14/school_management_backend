using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

public class ClassroomController : AuthorizationController
{
    readonly IClassroomService _classroomService;
    public ClassroomController(IClassroomService classroomService, IUserService userService) : base(userService)
    {
        _classroomService = classroomService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ClassroomDTO>>> GetById([FromRoute] Guid id) {
        return this.FromServiceResponse( await _classroomService.GetClassroomById(id));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ClassroomDTO classroom)
    {
        return this.FromServiceResponse(await _classroomService.AddClassroom(classroom));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ClassroomUpdateDTO classroom)
    {
        return this.FromServiceResponse(await _classroomService.UpdateClassroom(classroom));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _classroomService.DeleteClassroom(id));
    }
}