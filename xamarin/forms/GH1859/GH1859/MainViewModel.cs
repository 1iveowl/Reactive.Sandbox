﻿using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace GH1859
{
    public class MainViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> CrashCommand { get; }

        public MainViewModel()
        {
            CrashCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var someArray = new int[2];
                var shouldBreak = someArray[3];
            });

            CrashCommand.IsExecuting.Subscribe(_ =>
            {
                var foo = "bar";
            });
        }
    }
}