using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capsule.Tests
{
    using System.Web.Http.Results;

    using Capsule.Context;
    using Capsule.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TasksControllerTest
    {
        [TestMethod]
        public void GetAllTasks()
        {
            var controller = new TasksController(new TaskRepositoryTest());
            var allTasks = controller.GetTasks();
            Assert.AreEqual(2, allTasks.Count());
        }
        [TestMethod]
        public void GetTaskById()
        {
            var controller = new TasksController(new TaskRepositoryTest());
            var task = controller.GetTask(1);
            Assert.IsNotNull(task);

            task = controller.GetTask(3);
            Assert.IsInstanceOfType(task, typeof(NotFoundResult));
        }
        [TestMethod]
        public void PutTask()
        {
            var controller = new TasksController(new TaskRepositoryTest());
            var allTasks = controller.PutTask(new Task
                                                           {
                                                               TaskId = 2,
                                                               ParentId = null,
                                                               EndDate = null,
                                                               Priority = 1,
                                                               StartDate = DateTime.Now,
                                                               TaskName = "Task1"

                                                           });
            Assert.IsInstanceOfType(allTasks, typeof(StatusCodeResult));
        }
        [TestMethod]
        public void PutTaskFailed()
        {
            var controller = new TasksController(new TaskRepositoryTest());
            var allPoMasters = controller.PutTask(new Task
                                                           {
                                                               TaskId = 3,
                                                               ParentId = null,
                                                               EndDate = null,
                                                               Priority = 1,
                                                               StartDate = DateTime.Now,
                                                               TaskName = "Task1"

                                                           });
            Assert.IsInstanceOfType(allPoMasters, typeof(NotFoundResult));
        }
        [TestMethod]
        public void PostTask()
        {
            var controller = new TasksController(new TaskRepositoryTest());
            controller.PostTask(new Task
                                    {
                                        TaskId = 1,
                                        ParentId = null,
                                        EndDate = null,
                                        Priority = 1,
                                        StartDate = DateTime.Now,
                                        TaskName = "Task1"

                                    });
            // Assert.IsInstanceOfType(allPoMasters, typeof(BadRequestResult));
            var allTasks = controller.GetTasks();
            Assert.AreEqual(3, allTasks.Count());

        }

        [TestMethod]
        public void RemoveTask()
        {
            var controller = new TasksController(new TaskRepositoryTest());
            controller.DeleteTask(2);
            var allTasks = controller.GetTasks();
            Assert.AreEqual(1, allTasks.Count());
        }
    }
}
