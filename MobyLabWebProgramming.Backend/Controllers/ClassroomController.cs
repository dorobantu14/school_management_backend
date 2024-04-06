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
public class ClassroomController : AuthorizedController 
{
    private readonly IClassroomService _classroomService;
    public ClassroomController(IClassroomService classroomService, IUserService userService) : base(userService)
    {
        _classroomService = classroomService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ClassroomDTO>>> GetById([FromRoute] Guid id) {
        return this.FromServiceResponse(await _classroomService.GetClassroomById(id));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] ClassroomDTO classroom)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _classroomService.AddClassroom(classroom, currentUser.Result));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] ClassroomUpdateDTO classroom)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _classroomService.UpdateClassroom(classroom, currentUser.Result));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _classroomService.DeleteClassroom(id, currentUser.Result));
    }
}