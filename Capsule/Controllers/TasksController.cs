using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Capsule.Context;

namespace Capsule.Controllers
{
    using Capsule.Repository;
    using System.Web.Http.Cors;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TasksController : ApiController
    {
        private ITaskRepository repository;
        public TasksController(ITaskRepository _repository)
        {
            repository = _repository;
        }
       // TaskRepository  repository = new TaskRepository();
        // GET: api/Tasks
        public IQueryable<Task> GetTasks()
        {
            var list = repository.GetAllTasks().ToList();
            return repository.GetAllTasks();
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult GetTask(int id)
        {
            Task task = repository.GetTasksById(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        public IHttpActionResult PutTask(Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (task.TaskId != task.TaskId)
            {
                return BadRequest();
            }

            try
            {
               this.repository.UpdateTask(task);
            }
            catch (Exception e)
            {
                if (!repository.TaskExists(task.TaskId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Task))]
        public IHttpActionResult PostTask(Task task)
        {
            this.repository.CreateTask(task);

            return CreatedAtRoute("DefaultApi", new { id = task.TaskId }, task);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Task))]
        public IHttpActionResult DeleteTask(int id)
        {
            Task task = this.repository.GetTasksById(id);
            if (task == null)
            {
                return NotFound();
            }
            this.repository.DeleteTask(task);
            return Ok(task);
        }

       
    }
}