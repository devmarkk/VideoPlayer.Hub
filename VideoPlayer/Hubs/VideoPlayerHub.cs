using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoPlayer.Hubs
{
    public class VideoPlayerHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<VideoPlayerHub> _loggger;

        public VideoPlayerHub(
            IMemoryCache memoryCache,
            ILogger<VideoPlayerHub> logger)
        {
            _memoryCache = memoryCache;
            _loggger = logger;
        }

        public void HandleVideoStateChanged(int evento)
        {
            if (evento == 1)
            {
                Clients.All.SendAsync("Play");


                _loggger.LogInformation("play");
            }
            else
            {
                Clients.All.SendAsync("Pause");
                _loggger.LogInformation("pause");
            }
        }

        public void LoadVideo(string videoId)
        {
            Clients.All.SendAsync("LoadVideo", videoId);
        }

        public override Task OnConnectedAsync()
        {
            _loggger.LogInformation("Connected:>: " + Context.ConnectionId);
            return base.OnConnectedAsync();
        }
    }
}
