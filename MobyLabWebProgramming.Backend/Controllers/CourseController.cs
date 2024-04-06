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
public class CourseController : AuthorizedController
{
    readonly ICourseService _courseService;
    public CourseController(ICourseService courseService, IUserService userService) : base(userService)
    {
        _courseService = courseService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<CourseDTO>>> GetById([FromRoute] Guid id) {
        return this.FromServiceResponse( await _courseService.GetCourseById(id));
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] CourseDTO course)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _courseService.AddCourse(course, currentUser.Result));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] CourseUpdateDTO course)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _courseService.UpdateCourse(course, currentUser.Result));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return this.FromServiceResponse(await _courseService.DeleteCourse(id, currentUser.Result));
    }
    
    [Authorize]
    [HttpGet("{courseId:guid}")]
    public async Task<ActionResult<RequestResponse<List<GetSchedulesForCourseDTO>>>> GetSchedulesForCourse([FromRoute] Guid courseId)
    {
        return this.FromServiceResponse(await _courseService.GetSchedulesForCourse(courseId));
    }
}