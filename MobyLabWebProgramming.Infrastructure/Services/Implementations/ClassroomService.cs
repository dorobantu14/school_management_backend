using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
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

    public async Task<ServiceResponse> AddClassroom(ClassroomDTO classroom, CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(new Classroom
        {
            Name = classroom.Name,
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateClassroom(ClassroomUpdateDTO classroom, CancellationToken cancellationToken = default)
    {
        var existingClassroom = await _repository.GetAsync(new ClassroomUpdateSpec(classroom.Id), cancellationToken);
        
        if (existingClassroom == null)
        {
            return ServiceResponse.FromError(CommonErrors.ClassroomNotFound);
        }
        
        existingClassroom.Name = classroom.Name ?? existingClassroom.Name;
        existingClassroom.ScheduleId = classroom.ScheduleId ?? existingClassroom.ScheduleId;
        
        await _repository.UpdateAsync(existingClassroom, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteClassroom(Guid id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync<Classroom>(id, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
}