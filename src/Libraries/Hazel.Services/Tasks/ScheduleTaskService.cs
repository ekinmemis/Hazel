using Hazel.Core.Domain.Tasks;
using Hazel.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hazel.Services.Tasks
{
    /// <summary>
    /// Task service.
    /// </summary>
    public partial class ScheduleTaskService : IScheduleTaskService
    {
        /// <summary>
        /// Defines the _taskRepository.
        /// </summary>
        private readonly IRepository<ScheduleTask> _taskRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleTaskService"/> class.
        /// </summary>
        /// <param name="taskRepository">The taskRepository<see cref="IRepository{ScheduleTask}"/>.</param>
        public ScheduleTaskService(IRepository<ScheduleTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Deletes a task.
        /// </summary>
        /// <param name="task">Task.</param>
        public virtual void DeleteTask(ScheduleTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            _taskRepository.Delete(task);
        }

        /// <summary>
        /// Gets a task.
        /// </summary>
        /// <param name="taskId">Task identifier.</param>
        /// <returns>Task.</returns>
        public virtual ScheduleTask GetTaskById(int taskId)
        {
            if (taskId == 0)
                return null;

            return _taskRepository.GetById(taskId);
        }

        /// <summary>
        /// Gets a task by its type.
        /// </summary>
        /// <param name="type">Task type.</param>
        /// <returns>Task.</returns>
        public virtual ScheduleTask GetTaskByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return null;

            var query = _taskRepository.Table;
            query = query.Where(st => st.Type == type);
            query = query.OrderByDescending(t => t.Id);

            var task = query.FirstOrDefault();
            return task;
        }

        /// <summary>
        /// Gets all tasks.
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records.</param>
        /// <returns>Tasks.</returns>
        public virtual IList<ScheduleTask> GetAllTasks(bool showHidden = false)
        {
            var query = _taskRepository.Table;
            if (!showHidden)
            {
                query = query.Where(t => t.Enabled);
            }

            query = query.OrderByDescending(t => t.Seconds);

            var tasks = query.ToList();
            return tasks;
        }

        /// <summary>
        /// Inserts a task.
        /// </summary>
        /// <param name="task">Task.</param>
        public virtual void InsertTask(ScheduleTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            _taskRepository.Insert(task);
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="task">Task.</param>
        public virtual void UpdateTask(ScheduleTask task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            _taskRepository.Update(task);
        }
    }
}
