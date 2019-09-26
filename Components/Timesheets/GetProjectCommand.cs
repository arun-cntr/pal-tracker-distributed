using System;
using System.Threading.Tasks;
using Steeltoe.CircuitBreaker.Hystrix;

namespace Timesheets
{
    internal class GetProjectCommand: HystrixCommand<ProjectInfo>
    {
        private Func<long, Task<ProjectInfo>> doGet;
        private Func<long, Task<ProjectInfo>> doGetFromCache;
        private long projectId;

        public GetProjectCommand(Func<long, Task<ProjectInfo>> doGet, Func<long, Task<ProjectInfo>> doGetFromCache, long projectId): base(HystrixCommandGroupKeyDefault.AsKey("ProjectClientGroup"))
        {
            this.doGet = doGet;
            this.doGetFromCache = doGetFromCache;
            this.projectId = projectId;
        }
        protected override async Task<ProjectInfo> RunAsync() => await doGet(projectId);
        protected override async Task<ProjectInfo> RunFallbackAsync() => await doGetFromCache(projectId);
    }
}