﻿using System;
using System.Text;
using MSScriptControl;

namespace DAL
{
    /// <summary> 
    /// 脚本类型 
    /// </summary> 
    public enum ScriptLanguage
    {
        /// <summary> 
        /// JScript脚本语言 
        /// </summary> 
        JScript,

        /// <summary> 
        /// VBscript脚本语言 
        /// </summary> 
        VBScript,

        /// <summary> 
        /// JavaScript脚本语言 
        /// </summary> 
        JavaScript
    }

    /// <summary> 
    /// 脚本运行错误代理 
    /// </summary> 
    public delegate void RunErrorHandler();

    /// <summary> 
    /// 脚本运行超时代理 
    /// </summary> 
    public delegate void RunTimeoutHandler();

    /// <summary> 
    /// ScriptEngine类 
    /// </summary> 
    public class ScriptEngine
    {
        private ScriptControl msc;
        //定义脚本运行错误事件 
        public event RunErrorHandler RunError;
        //定义脚本运行超时事件 
        public event RunTimeoutHandler RunTimeout;

        /// <summary> 
        ///构造函数 
        /// </summary> 
        public ScriptEngine()
            : this(ScriptLanguage.VBScript)
        {
        }

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="language">脚本类型</param> 
        public ScriptEngine(ScriptLanguage language)
        {
            this.msc = new ScriptControlClass();
            this.msc.UseSafeSubset = true;
            this.msc.Language = language.ToString();
            ((DScriptControlSource_Event)this.msc).Error += new DScriptControlSource_ErrorEventHandler(ScriptEngine_Error);
            ((DScriptControlSource_Event)this.msc).Timeout += new DScriptControlSource_TimeoutEventHandler(ScriptEngine_Timeout);
        }

        /// <summary> 
        /// 运行Eval方法 
        /// </summary> 
        /// <param name="expression">表达式</param> 
        /// <param name="codeBody">函数体</param> 
        /// <returns>返回值object</returns> 
        public object Eval(string expression, string codeBody)
        {
            msc.AddCode(codeBody);
            return msc.Eval(expression);
        }

        /// <summary> 
        /// 运行Run方法 
        /// </summary> 
        /// <param name="mainFunctionName">入口函数名称</param> 
        /// <param name="parameters">参数</param> 
        /// <param name="codeBody">函数体</param> 
        /// <returns>返回值object</returns> 
        public object Run(string mainFunctionName, object[] parameters, string codeBody)
        {
            this.msc.AddCode(codeBody);
            return msc.Run(mainFunctionName, parameters);
        }

        /// <summary> 
        /// 获取或设置脚本语言 
        /// </summary> 
        public ScriptLanguage Language
        {
            get { return (ScriptLanguage)Enum.Parse(typeof(ScriptLanguage), this.msc.Language, false); }
            set { this.msc.Language = value.ToString(); }
        }

        /// <summary> 
        /// RunError事件激发 
        /// </summary> 
        private void OnError()
        {
            if (RunError != null)
                RunError();
        }

        /// <summary> 
        /// OnTimeout事件激发 
        /// </summary> 
        private void OnTimeout()
        {
            if (RunTimeout != null)
                RunTimeout();
        }

        private void ScriptEngine_Error()
        {
            OnError();
        }

        private void ScriptEngine_Timeout()
        {
            OnTimeout();
        }
    }
}
