﻿using System.Runtime.InteropServices;
using SMath.Manager;

namespace coolprop_wrapper.Functions
{
  class CoolProp_get_parameter_information_string : IFunction
  {
    // long get_parameter_information_string(const char *key, char *Output, int n);
    [DllImport(
      "CoolProp.x86.dll", EntryPoint = "get_parameter_information_string",
      CharSet = CharSet.Ansi)]
    internal static extern long CoolPropDLLfunc_x86(
      string key,
      System.Text.StringBuilder Output,
      int n);
    [DllImport(
      "CoolProp.x64.dll", EntryPoint = "get_parameter_information_string",
      CharSet = CharSet.Ansi)]
    internal static extern long CoolPropDLLfunc_x64(
      string key,
      System.Text.StringBuilder Output,
      int n);
    internal static long CoolPropDLLfunc(
      string key,
      System.Text.StringBuilder Output,
      int n)
    {
      switch (System.IntPtr.Size)
      {
        case 4:
          return CoolPropDLLfunc_x86(key, Output, n);
        case 8:
          return CoolPropDLLfunc_x64(key, Output, n);
      }
      throw new EvaluationException(Errors.PluginCannotBeEnabled);
    }

    Term inf;
    public static int[] Arguments = new [] {2};

    public CoolProp_get_parameter_information_string(int childCount)
    {
      inf = new Term(this.GetType().Name, TermType.Function, childCount);
    }

    Term IFunction.Info { get { return inf; } }

    public TermInfo GetTermInfo(string lang)
    {
      string funcInfo = "(key, Output) Get a parameter information string\r\n" +
        "key A string\r\n" +
        "Output A string (one of \"IO\", \"short\", \"long\", \"units\")";

      var argsInfos = new [] {
        new ArgumentInfo(ArgumentSections.String),
        new ArgumentInfo(ArgumentSections.String)
      };

      return new TermInfo(inf.Text,
                          inf.Type,
                          funcInfo,
                          FunctionSections.Unknown,
                          true,
                          argsInfos);
    }

    bool IFunction.ExpressionEvaluation(Term root, Term[][] args, ref SMath.Math.Store context, ref Term[] result)
    {
      var key    = coolpropPlugin.GetStringParam(args[0], ref context);
      var Output = coolpropPlugin.GetStringParam(args[1], ref context);
      var output = new System.Text.StringBuilder(Output, 10000);
      var Result = CoolPropDLLfunc(key, output, output.Capacity);
      coolpropPlugin.LogInfo("[INFO ]",
        "key = {0} output(in) = {1} output(out) = {2} Result = {3}",
        key, Output, output.ToString(), Result);
      if (Result != 1)
        throw new EvaluationException(Errors.ArgumentDoesNotMatchToExpectedKind);
      result = coolpropPlugin.MakeStringResult(output.ToString());

      return true;
    }
  }
}
