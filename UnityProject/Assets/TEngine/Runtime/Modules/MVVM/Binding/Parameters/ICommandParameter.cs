﻿

using System;

namespace Framework.Binding.Parameters
{
    public interface ICommandParameter
    {
        object GetValue();

        Type GetValueType();
    }

    public interface ICommandParameter<T> : ICommandParameter
    {
        new T GetValue();
    }
}