using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capsule.Tests
{
    using System.Web.Razor.Parser.SyntaxTree;

    using Capsule.Context;
    using Capsule.Repository;

    public class TaskRepositoryTest: ITaskRepository
    {
        private List<Task> taskList = new List<Task>
                                                      {
                                                          new Task
                                                              {
                                                                  TaskId = 1,
                                                                  ParentId = null,
                                                                  EndDate = null,
                                                                  Priority = 1,
                                                                  StartDate = DateTime.Now,
                                                                  TaskName = "Task1"

                                                              },

                                                          new Task
                                                              {
                                                                  TaskId = 2,
                                                                  ParentId = 1,
                                                                  EndDate = null,
                                                                  Priority = 1,
                                                                  StartDate = DateTime.Now,
                                                                  TaskName = "Task2"
                                                              },
                                                      };
        public IQueryable<Task> GetAllTasks()
        {
            return taskList.AsQueryable();
        }

        public Task GetTasksById(int taskId)
        {
            return taskList.FirstOrDefault(x => x.TaskId == taskId);
        }

        public void UpdateTask(Task task)
        {

            var taskToUpdate = taskList.FirstOrDefault(x => x.TaskId.Equals(task.TaskId));
            taskToUpdate.TaskName = task.TaskName;
            taskToUpdate.StartDate = task.StartDate;
            taskToUpdate.EndDate = task.EndDate;
            taskToUpdate.Priority = task.Priority;
            taskToUpdate.ParentId = task.ParentId;
        }

        public void CreateTask(Task task)
        {
            taskList.Add(task);
        }

        public void DeleteTask(Task task)
        {
            taskList.Remove(this.taskList.FirstOrDefault(x => x.TaskId.Equals(task.TaskId)));
        }

        public bool TaskExists(int id)
        {
            return taskList.Count(e => e.TaskId == id) > 0;
        }
    }
}
