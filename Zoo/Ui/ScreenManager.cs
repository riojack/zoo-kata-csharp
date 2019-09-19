using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.Ui
{
    public class ScreenManager
    {
        public ICollection<IScreen> Screens { get; set; }

        public TextWriter Out { get; set; }
        public TextReader In { get; set; }

        public async Task StartInputOutputLoop()
        {
            foreach (var screen in Screens)
            {
                Out.WriteLine(screen.Name);
            }
        }
    }
}