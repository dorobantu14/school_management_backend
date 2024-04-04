using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

public class CourseController : AuthorizationController
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
        return this.FromServiceResponse(await _courseService.AddCourse(course));
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] CourseUpdateDTO course)
    {
        return this.FromServiceResponse(await _courseService.UpdateCourse(course));
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        return this.FromServiceResponse(await _courseService.DeleteCourse(id));
    }
}