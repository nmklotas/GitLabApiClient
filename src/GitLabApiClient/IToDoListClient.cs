using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Models.ToDoList.Requests;
using GitLabApiClient.Models.ToDoList.Responses;

namespace GitLabApiClient
{
    public interface IToDoListClient
    {
        /// <summary>
        /// Get a list of ToDos for current user.
        /// </summary>
        /// <param name="options">Query options.</param>
        Task<IList<IToDo>> GetAsync(Action<ToDoListQueryOptions> options = null);

        /// <summary>
        /// Marks a specific ToDo, for the current user, as done.
        /// </summary>
        /// <param name="toDoId">The Id of the <see cref="IToDo"/> to mark as done.</param>
        Task MarkAsDoneAsync(int toDoId);

        /// <summary>
        /// Marks all ToDos, for the current user, as done.
        /// </summary>
        Task MarkAllAsDoneAsync();
    }
}
