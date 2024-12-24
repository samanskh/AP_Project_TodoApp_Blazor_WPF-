using AutoMapper;
using WebApplication1.Model;
using WebApplication1.Model.DTO;

namespace WebApplication1
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<TodoTask,GetTaskDto>().ReverseMap();
            CreateMap<TodoTask, CreateTaskDto>().ReverseMap();
            CreateMap<TodoTask,UpdateTaskDto>().ReverseMap();
        }
    }
}
