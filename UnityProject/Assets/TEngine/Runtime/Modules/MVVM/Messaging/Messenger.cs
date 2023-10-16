﻿using System;
using System.Collections.Concurrent;

namespace Framework.Messaging
{
    /// <summary>
    /// The Messenger is a class allowing objects to exchange messages.
    /// </summary>
    public class Messenger : IMessenger
    {
        public static readonly Messenger Default = new Messenger();

        private readonly ConcurrentDictionary<Type, SubjectBase> notifiers = new ConcurrentDictionary<Type, SubjectBase>();
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<Type, SubjectBase>> channelNotifiers = new ConcurrentDictionary<string, ConcurrentDictionary<Type, SubjectBase>>();

        /// <summary>
        /// Subscribe a message.
        /// </summary>
        /// <param name="type">The type of message that the recipient subscribes for.</param>
        /// <param name="action">The action that will be executed when a message of type T is sent.</param>
        /// <returns>Disposable object that can be used to unsubscribe the message from the messenger.
        /// if the disposable object is disposed,the message is automatically unsubscribed.</returns>
        public virtual ISubscription<object> Subscribe(Type type, Action<object> action)
        {
            SubjectBase notifier;
            if (!notifiers.TryGetValue(type, out notifier))
            {
                notifier = new Subject<object>();
                if (!notifiers.TryAdd(type, notifier))
                    notifiers.TryGetValue(type, out notifier);
            }
            return (notifier as Subject<object>).Subscribe(action);
        }

        /// <summary>
        /// Subscribe a message.
        /// </summary>
        /// <typeparam name="T">The type of message that the recipient subscribes for.</typeparam>
        /// <param name="action">The action that will be executed when a message of type T is sent.</param>
        /// <returns>Disposable object that can be used to unsubscribe the message from the messenger.
        /// if the disposable object is disposed,the message is automatically unsubscribed.</returns>
        public virtual ISubscription<T> Subscribe<T>(Action<T> action)
        {
            Type type = typeof(T);
            SubjectBase notifier;
            if (!notifiers.TryGetValue(type, out notifier))
            {
                notifier = new Subject<T>();
                if (!notifiers.TryAdd(type, notifier))
                    notifiers.TryGetValue(type, out notifier);
            }
            return (notifier as Subject<T>).Subscribe(action);
        }

        /// <summary>
        /// Subscribe a message.
        /// </summary>
        /// <param name="channel">A name for a messaging channel.If a recipient subscribes
        /// using a channel, and a sender sends a message using the same channel, then this
        /// message will be delivered to the recipient. Other recipients who did not
        /// use a channel when subscribing (or who used a different channel) will not
        /// get the message. </param>
        /// <param name="type">The type of message that the recipient subscribes for.</param>
        /// <param name="action">The action that will be executed when a message of type T is sent.</param>
        /// <returns>Disposable object that can be used to unsubscribe the message from the messenger.
        /// if the disposable object is disposed,the message is automatically unsubscribed.</returns>
        public virtual ISubscription<object> Subscribe(string channel, Type type, Action<object> action)
        {
            SubjectBase notifier = null;
            ConcurrentDictionary<Type, SubjectBase> dict = null;
            if (!channelNotifiers.TryGetValue(channel, out dict))
            {
                dict = new ConcurrentDictionary<Type, SubjectBase>();
                if (!channelNotifiers.TryAdd(channel, dict))
                    channelNotifiers.TryGetValue(channel, out dict);
            }

            if (!dict.TryGetValue(type, out notifier))
            {
                notifier = new Subject<object>();
                if (!dict.TryAdd(type, notifier))
                    dict.TryGetValue(type, out notifier);
            }
            return (notifier as Subject<object>).Subscribe(action);
        }

        /// <summary>
        /// Subscribe a message.
        /// </summary>
        /// <typeparam name="T">The type of message that the recipient subscribes for.</typeparam>
        /// <param name="channel">A name for a messaging channel.If a recipient subscribes
        /// using a channel, and a sender sends a message using the same channel, then this
        /// message will be delivered to the recipient. Other recipients who did not
        /// use a channel when subscribing (or who used a different channel) will not
        /// get the message. </param>
        /// <param name="action">The action that will be executed when a message of type T is sent.</param>
        /// <returns>Disposable object that can be used to unsubscribe the message from the messenger.
        /// if the disposable object is disposed,the message is automatically unsubscribed.</returns>
        public virtual ISubscription<T> Subscribe<T>(string channel, Action<T> action)
        {
            SubjectBase notifier = null;
            ConcurrentDictionary<Type, SubjectBase> dict = null;
            if (!channelNotifiers.TryGetValue(channel, out dict))
            {
                dict = new ConcurrentDictionary<Type, SubjectBase>();
                if (!channelNotifiers.TryAdd(channel, dict))
                    channelNotifiers.TryGetValue(channel, out dict);
            }

            if (!dict.TryGetValue(typeof(T), out notifier))
            {
                notifier = new Subject<T>();
                if (!dict.TryAdd(typeof(T), notifier))
                    dict.TryGetValue(typeof(T), out notifier);
            }
            return (notifier as Subject<T>).Subscribe(action);
        }

        /// <summary>
        /// Publish a message to subscribed recipients. 
        /// </summary>
        /// <param name="message"></param>
        public virtual void Publish(object message)
        {
            this.Publish<object>(message);
        }

        /// <summary>
        /// Publish a message to subscribed recipients. 
        /// </summary>
        /// <typeparam name="T">The type of message that will be sent.</typeparam>
        /// <param name="message">The message to send to subscribed recipients.</param>
        public virtual void Publish<T>(T message)
        {
            if (message == null || notifiers.Count <= 0)
                return;

            Type messageType = message.GetType();
            foreach (var kv in notifiers)
            {
                if (kv.Key.IsAssignableFrom(messageType))
                    kv.Value.Publish(message);
            }
        }

        /// <summary>
        /// Publish a message to subscribed recipients. 
        /// </summary>
        /// <param name="channel">A name for a messaging channel.If a recipient subscribes
        /// using a channel, and a sender sends a message using the same channel, then this
        /// message will be delivered to the recipient. Other recipients who did not
        /// use a channel when subscribing (or who used a different channel) will not
        /// get the message. </param>
        /// <param name="message">The message to send to subscribed recipients.</param>
        public virtual void Publish(string channel, object message)
        {
            this.Publish<object>(channel, message);
        }

        /// <summary>
        /// Publish a message to subscribed recipients. 
        /// </summary>
        /// <typeparam name="T">The type of message that will be sent.</typeparam>
        /// <param name="channel">A name for a messaging channel.If a recipient subscribes
        /// using a channel, and a sender sends a message using the same channel, then this
        /// message will be delivered to the recipient. Other recipients who did not
        /// use a channel when subscribing (or who used a different channel) will not
        /// get the message. </param>
        /// <param name="message">The message to send to subscribed recipients.</param>
        public virtual void Publish<T>(string channel, T message)
        {
            if (string.IsNullOrEmpty(channel) || message == null)
                return;

            ConcurrentDictionary<Type, SubjectBase> dict = null;
            if (!channelNotifiers.TryGetValue(channel, out dict) || dict.Count <= 0)
                return;

            Type messageType = message.GetType();
            foreach (var kv in dict)
            {
                if (kv.Key.IsAssignableFrom(messageType))
                    kv.Value.Publish(message);
            }
        }
    }
}