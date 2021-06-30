using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirExplorer {
    class ListViewModel {
        public object Original {
            get;
            set;
        }

        public ListViewModel(string name, string length, string creationTime, string type) {
            Name = name;
            Length = length;
            CreationTime = creationTime;
            Type = type;
        }

        public string Name {
            set;
            get;
        }

        public string Length {
            set;
            get;
        }

        public string CreationTime {
            set;
            get;
        }

        public string Type {
            set;
            get;
        }
    }
}
