using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ScheduleUpdateSpec : BaseSpec<ScheduleUpdateSpec, Schedule>
{
    public ScheduleUpdateSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
}