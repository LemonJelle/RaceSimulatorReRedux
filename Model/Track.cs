using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections) 
        {
            Name = name;
            Sections = ConvertSectionArraytoLinkedList(sections);
        }

        //Convert the array with sectiontypes to a linkedlist with sections
        private LinkedList<Section> ConvertSectionArraytoLinkedList(SectionTypes[] sectionTypes)
        {
            LinkedList<Section> sectionList = new LinkedList<Section>();
            foreach (SectionTypes sectionType in sectionTypes)
            {
                sectionList.AddLast(new Section(sectionType));
            }
            return sectionList;
        }
    }
}
