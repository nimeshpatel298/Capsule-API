using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using Capsule.Context;

namespace Capsule.Repository
{
    public class TaskRepository : ITaskRepository, IDisposable
    {
        private CapsuleEntities db = new CapsuleEntities();
        public System.Linq.IQueryable<Task> GetAllTasks()
        {
            return db.Tasks.Include(x => x.ParentTask);
        }

        public Task GetTasksById(int taskId)
        {
            return db.Tasks.FirstOrDefault(x => x.TaskId == taskId);
        }

        public void UpdateTask(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }

            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void CreateTask(Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void DeleteTask(Task task)
        {
            db.Tasks.Remove(task);
            db.SaveChanges();

        }
        
        public bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.TaskId == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}