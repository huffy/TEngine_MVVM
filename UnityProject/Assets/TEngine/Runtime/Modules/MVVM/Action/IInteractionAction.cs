﻿
namespace Framework.Interactivity
{
    public interface IInteractionAction
    {
        void OnRequest(object sender, InteractionEventArgs args);
    }
}
