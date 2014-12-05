using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JSA_Game.Screens
{
    [Serializable]
    [XmlInclude(typeof(SaveGameData))]
    public class ConsumableSaveData
    {
        public string itemName;

    }
}
