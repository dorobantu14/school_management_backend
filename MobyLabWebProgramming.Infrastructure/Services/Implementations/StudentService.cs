using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public StudentService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    
    public async Task<ServiceResponse<StudentDTO>> GetStudentById(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var student = await _repository.GetAsync(new StudentSpec(id), cancellationToken);
        
        return student != null ?
            ServiceResponse<StudentDTO>.ForSuccess(student) :
            ServiceResponse<StudentDTO>.FromError(CommonErrors.StudentNotFound);
        
    }

    // this method should add the student also in the students list of that classroom
    public async Task<ServiceResponse> AddStudent(StudentDTO student, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        await _repository.AddAsync(new Student
        {
            Name = student.Name,
            Email = student.Email,
            ClassroomId = student.ClassroomId
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateStudent(StudentUpdateDTO student, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default)
    {
        var existingStudent = await _repository.GetAsync(new StudentUpdateSpec(student.Id), cancellationToken);
        
        if (existingStudent == null)
        {
            return ServiceResponse.FromError(CommonErrors.StudentNotFound);
        }
        
        existingStudent.Name = student.Name ?? existingStudent.Name;
        existingStudent.Email = student.Email ?? existingStudent.Email;
        existingStudent.ClassroomId = student.ClassroomId ?? existingStudent.ClassroomId;
        
        await _repository.UpdateAsync(existingStudent, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteStudent(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
      await  _repository.DeleteAsync<Student>(id, cancellationToken);
      return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse<List<GetStudentsByClassDTO>>> GetStudentByClassroomId(Guid classroomId, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var students = await _repository.ListAsync(new GetStudentsByClassIdSpec(classroomId), cancellationToken);
        
        return students.Count > 0 ?
            ServiceResponse<List<GetStudentsByClassDTO>>.ForSuccess(students) :
            ServiceResponse<List<GetStudentsByClassDTO>>.FromError(CommonErrors.StudentNotFound);
    }
}