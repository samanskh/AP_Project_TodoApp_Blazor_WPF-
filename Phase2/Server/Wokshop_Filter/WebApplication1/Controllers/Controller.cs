using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Model;
using WebApplication1.Model.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTasksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public TodoTasksController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("tasks")]
        public async Task<ActionResult<IEnumerable<GetTaskDto>>> GetTodoTasks()
        {
            var todotasks = await _db.TodoTasks.ToListAsync();
            return Ok(_mapper.Map<List<GetTaskDto>>(todotasks));
        }

        [HttpGet("tasks/{id:int}")]
        public async Task<ActionResult<GetTaskDto>> GetTodoTask(int id)
        {
            var todotask = await _db.TodoTasks.FirstOrDefaultAsync(f => f.Id == id);
            if (todotask == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetTaskDto>(todotask));
        }

        [HttpPost("tasks")]
        public async Task<ActionResult<TodoTask>> CreateTodoTask([FromBody] CreateTaskDto todotaskDto)
        {
            var todotask = _mapper.Map<TodoTask>(todotaskDto);
            await _db.TodoTasks.AddAsync(todotask);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoTask), new { id = todotask.Id }, todotask);
        }

        [HttpPut("tasks/{id:int}")]
        public async Task<ActionResult<TodoTask>> UpdateTodoTask(int id, [FromBody] UpdateTaskDto todotaskDto)
        {
            var todotask = await _db.TodoTasks.FirstOrDefaultAsync(f => f.Id == id);
            if (todotask == null)
            {
                return NotFound();
            }
            _mapper.Map(todotaskDto, todotask);
            _db.TodoTasks.Update(todotask);
            await _db.SaveChangesAsync();
            return Ok(todotask);
        }

        [HttpDelete("tasks/{id:int}")]
        public async Task<ActionResult<TodoTask>> DeleteTodoTask(int id)
        {
            var todotask = await _db.TodoTasks.FirstOrDefaultAsync(f => f.Id == id);
            if (todotask == null)
            {
                return NotFound();
            }
            _db.TodoTasks.Remove(todotask);
            await _db.SaveChangesAsync();
            return Ok(todotask);
        }

        [HttpGet("todoLists")]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoLists()
        {
            return await _db.TodoLists.ToListAsync();
        }

        [HttpGet("todoLists/{id:int}")]
        public async Task<ActionResult<TodoList>> GetTodoList(int id)
        {
            var todolist = await _db.TodoLists.FirstOrDefaultAsync(c => c.Id == id);
            if (todolist == null)
            {
                return NotFound();
            }
            return Ok(todolist);
        }

        [HttpPost("todoLists")]
        public async Task<ActionResult<TodoList>> AddList([FromBody] TodoList todolist)
        {
            await _db.TodoLists.AddAsync(todolist);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoList), new { id = todolist.Id }, todolist);
        }
        
        [HttpPut("todoLists/{id:int}")]
        public async Task<ActionResult<TodoList>> UpdateTodoList(int id, [FromBody] TodoList todotaskDto)
        {
            var todotask = await _db.TodoLists.FirstOrDefaultAsync(f => f.Id == id);
            if (todotask == null)
            {
                return NotFound();
            }
            todotask.Name = todotaskDto.Name;
            _db.TodoLists.Update(todotask);
            await _db.SaveChangesAsync();
            return Ok(todotask);
        }

        [HttpDelete("todoLists/{id:int}")]
        public async Task<ActionResult<TodoList>> DeleteTodoList(int id)
        {
            var todolist = await _db.TodoLists.FirstOrDefaultAsync(c => c.Id == id);
            if (todolist == null)
            {
                return NotFound();
            }
            _db.TodoLists.Remove(todolist);
            await _db.SaveChangesAsync();
            return Ok(todolist);
        }

        [HttpGet("filterListId/{id:int}")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> FilterList(int? id)
        {
            var todotasks = await _db.TodoTasks.Where(b => b.Id == id).ToListAsync();
            return Ok(_mapper.Map<List<TodoTask>>(todotasks));
        }

        [HttpGet("filterNotCompleted")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> FilterNotCompleted()
        {
            var todotasks = await _db.TodoTasks.Where(b => !b.IsCompleted).ToListAsync();
            return Ok(_mapper.Map<List<TodoTask>>(todotasks));
        }

        [HttpGet("filterIsStarred")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> FilterIsStarred()
        {
            var todotasks = await _db.TodoTasks.Where(b => b.IsStarred).ToListAsync();
            return Ok(_mapper.Map<List<TodoTask>>(todotasks));
        }
    }
}
