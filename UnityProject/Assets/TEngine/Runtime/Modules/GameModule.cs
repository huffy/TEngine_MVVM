﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace TEngine
{
    /// <summary>
    /// 游戏模块。
    /// </summary>
    public partial class GameModule : MonoBehaviour
    {
        private static readonly Dictionary<Type, Module> _moduleMaps =
            new Dictionary<Type, Module>(ModuleImpSystem.DesignModuleCount);

        #region 框架模块

        /// <summary>
        /// 获取游戏基础模块。
        /// </summary>
        public static RootModule Base => _base ??= Get<RootModule>();

        private static RootModule _base;

        /// <summary>
        /// 获取调试模块。
        /// </summary>
        public static DebuggerModule Debugger => _debugger ??= Get<DebuggerModule>();

        private static DebuggerModule _debugger;

        /// <summary>
        /// 获取有限状态机模块。
        /// </summary>
        public static FsmModule Fsm => _fsm ??= Get<FsmModule>();

        private static FsmModule _fsm;

        /// <summary>
        /// 流程管理模块。
        /// </summary>
        public static ProcedureModule Procedure => _procedure ??= Get<ProcedureModule>();

        private static ProcedureModule _procedure;

        /// <summary>
        /// 获取对象池模块。
        /// </summary>
        public static ObjectPoolModule ObjectPool => _objectPool ??= Get<ObjectPoolModule>();

        private static ObjectPoolModule _objectPool;

        /// <summary>
        /// 获取资源模块。
        /// </summary>
        public static ResourceModule Resource => _resource ??= Get<ResourceModule>();

        private static ResourceModule _resource;

        /// <summary>
        /// 获取音频模块。
        /// </summary>
        public static AudioModule Audio => _audio ??= Get<AudioModule>();

        private static AudioModule _audio;

        /// <summary>
        /// 获取配置模块。
        /// </summary>
        public static SettingModule Setting => _setting ??= Get<SettingModule>();

        private static SettingModule _setting;

        /// <summary>
        /// 获取UI模块。
        /// </summary>
        public static UIModule UI => _ui ??= Get<UIModule>();

        private static UIModule _ui;

        /// <summary>
        /// 获取多语言模块。
        /// </summary>
        public static LocalizationModule Localization => _localization ??= Get<LocalizationModule>();

        private static LocalizationModule _localization;

        #endregion

        /// <summary>
        /// 获取游戏框架模块类。
        /// </summary>
        /// <typeparam name="T">游戏框架模块类。</typeparam>
        /// <returns>游戏框架模块实例。</returns>
        public static T Get<T>() where T : Module
        {
            Type type = typeof(T);

            if (_moduleMaps.TryGetValue(type, out var ret))
            {
                return ret as T;
            }

            T module = ModuleSystem.GetModule<T>();

            Log.Assert(condition: module != null, $"{typeof(T)} is null");

            _moduleMaps.Add(type, module);

            return module;
        }

        private void Start()
        {
            Log.Info("GameModule Active");
            gameObject.name = $"[{nameof(GameModule)}]";
            DontDestroyOnLoad(gameObject);
        }
    }
}