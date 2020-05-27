using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MqttClient.Blazor.Data
{
    public class MqttWrapper
    {
        private IJSRuntime _js;
        private DotNetObjectReference<MqttWrapper> _objRef;

        public MqttWrapper(IJSRuntime js)
        {
            _js = js;
            _objRef = DotNetObjectReference.Create(this);
        }

        public async Task InitializeAsync(string topic)
        {
            await _js.InvokeVoidAsync("mqttwrapper.createClient", _objRef, topic);
        }

        public async Task<string> SayHello()
        {
            return await _js.InvokeAsync<string>("mqttwrapper.sayHello", null);
        }

        [JSInvokable]
        public void OnMessageArrived(string payloadString)
        {
            MessageReceived?.Invoke(this, payloadString);
        }

        public event EventHandler<string> MessageReceived;
    }
}
