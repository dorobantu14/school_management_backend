using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ClassroomService : IClassroomService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public ClassroomService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ClassroomDTO>> GetClassroomById(Guid id, CancellationToken cancellationToken = default)
    {
        var classroom = await _repository.GetAsync(new ClassroomSpec(id), cancellationToken);

        return classroom == null ? ServiceResponse<ClassroomDTO>.FromError(CommonErrors.ClassroomNotFound) : ServiceResponse<ClassroomDTO>.ForSuccess(classroom);
    }

    public async Task<ServiceResponse> AddClassroom(ClassroomDTO classroom, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only the admin can add a classroom!"));
        }
        await _repository.AddAsync(new Classroom
        {
            Name = classroom.Name,
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateClassroom(ClassroomUpdateDTO classroom, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only the admin can update a classroom!"));
        }
        var existingClassroom = await _repository.GetAsync(new ClassroomUpdateSpec(classroom.Id), cancellationToken);
        
        if (existingClassroom == null)
        {
            return ServiceResponse.FromError(CommonErrors.ClassroomNotFound);
        }
        
        existingClassroom.Name = classroom.Name ?? existingClassroom.Name;
        
        await _repository.UpdateAsync(existingClassroom, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteClassroom(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.Forbidden, "Only the admin can delete a classroom!"));
        }
        await _repository.DeleteAsync<Classroom>(id, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
}