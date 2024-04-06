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

public class TeacherController : AuthorizedController
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService, IUserService userService) : base(userService)
    {
        _teacherService = teacherService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<TeacherDTO>>> GetById([FromRoute] Guid id) {
        return this.FromServiceResponse(await _teacherService.GetTeacherById(id));
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<List<TeacherDTO>>>> GetTeachers()
    {
        return this.FromServiceResponse(await _teacherService.GetTeachers());
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] AddTeacherDTO teacher)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _teacherService.AddTeacher(teacher, currentUser.Result));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] TeacherUpdateDTO teacher)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _teacherService.UpdateTeacher(teacher, currentUser.Result));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _teacherService.DeleteTeacher(id, currentUser.Result));
    }
    
    [Authorize]
    [HttpGet("{courseId:guid}")]
    public async Task<ActionResult<RequestResponse<List<GetTeachersByCourseDTO>>>> GetTeachersByCourse([FromRoute] Guid courseId)
    {
        return this.FromServiceResponse(await _teacherService.GetTeachersByCourse(courseId));
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<List<GetTeacherScheduleByEmailDTO>>>> GetTeacherSchedule()
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _teacherService.GetTeacherSchedule(currentUser.Result));
    }
}

