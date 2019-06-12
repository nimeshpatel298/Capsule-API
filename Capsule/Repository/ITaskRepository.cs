using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capsule.Repository
{
    using Capsule.Context;

    public interface ITaskRepository
    {
        IQueryable<Task> GetAllTasks();

        Task GetTasksById(int taskId);

        void UpdateTask(Task task);

        void CreateTask(Task task);

        void DeleteTask(Task task);

        bool TaskExists(int id);
    }
}
