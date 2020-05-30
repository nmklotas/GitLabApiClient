using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.ToDoList.Requests;
using GitLabApiClient.Models.ToDoList.Responses;

namespace GitLabApiClient
{
    /// <summary>
    /// Used to query GitLab API to retrieve and mark ToDos.
    /// <exception cref="GitLabException">Thrown if request to GitLab API fails</exception>
    /// <exception cref="HttpRequestException">Thrown if request to GitLab API fails</exception>
    /// </summary>
    public sealed class ToDoListClient : IToDoListClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly ToDoListQueryBuilder _queryBuilder;

        internal ToDoListClient(GitLabHttpFacade httpFacade, ToDoListQueryBuilder queryBuilder)
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Get a list of ToDos for current user.
        /// </summary>
        /// <param name="options">Query options.</param>
        public async Task<IList<IToDo>> GetAsync(Action<ToDoListQueryOptions> options = null)
        {
            var queryOptions = new ToDoListQueryOptions();
            options?.Invoke(queryOptions);

            string url = _queryBuilder.Build("todos", queryOptions);
            return await _httpFacade.GetPagedList<IToDo>(url);
        }

        /// <summary>
        /// Marks a specific ToDo, for the current user, as done.
        /// </summary>
        /// <param name="toDoId">The Id of the <see cref="IToDo"/> to mark as done.</param>
        public async Task MarkAsDoneAsync(int toDoId) =>
            await _httpFacade.Post($"todos/{toDoId}/mark_as_done");

        /// <summary>
        /// Marks all ToDos, for the current user, as done.
        /// </summary>
        public async Task MarkAllAsDoneAsync() =>
            await _httpFacade.Post($"todos/mark_as_done");

    }
}
