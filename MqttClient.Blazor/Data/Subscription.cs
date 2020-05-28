using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MqttClient.Blazor.Data
{
    class Subscription : System.ComponentModel.INotifyPropertyChanged
    {

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        MqttClient.Blazor.Data.MqttWrapper _wrapper;

        public Subscription(string topic, IJSRuntime runtime)
        {
            Topic = topic;
            _wrapper = new MqttClient.Blazor.Data.MqttWrapper(runtime);
            _wrapper.MessageReceived += (s, m) => Value = m;
        }

        public async Task InitializeAsync()
        {
            await _wrapper.InitializeAsync(Topic);
        }

        public string Topic { get; set; }

        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                if (value == _value)
                    return;

                _value = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }
    }
}
