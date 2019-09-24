using System;
using System.Collections.Generic;
using System.IO;
using Zoo.Injector;
using Zoo.Ui;

namespace Zoo
{
    public static class ZooConsoleApp
    {
        public static void Main()
        {
            SimpleInjector injector = new SimpleInjector();

            injector.Store(Console.Out, typeof(TextWriter));
            injector.Store(Console.In, typeof(TextReader));

            injector.Configure<ListTicketsScreen>();
            injector.Configure<AddTicketScreen>();
            injector.Configure<EditTicketScreen>();
            injector.Configure<RemoveTicketScreen>();

            injector.Store(new List<IScreen>
            {
                injector.FindByType<ListTicketsScreen>(),
                injector.FindByType<AddTicketScreen>(),
                injector.FindByType<EditTicketScreen>(),
                injector.FindByType<RemoveTicketScreen>()
            }, typeof(ICollection<IScreen>));

            injector.Configure<ScreenManager>();

            var screenManager = injector.FindByType<ScreenManager>();
            screenManager.StartInputOutputLoop().Wait();
        }
    }
}