using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLearn.WPF.Windows.Teacher.Pages
{
    internal class CreateTestingSettings
    {
        public int DurationOptionsCount { get; set; }

        public int ComboboxDefaultValue { get; set; }

        public CreateTestingSettings()
        {

        }

        public CreateTestingSettings(int durationOptionsCount, int comboboxDefaultValue)
        {
            DurationOptionsCount = durationOptionsCount;
            ComboboxDefaultValue = comboboxDefaultValue;
        }
    }
}
