﻿/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:   
 ******************************************/

using System.Collections.Generic;

namespace DncZeus.Api.Models.Menu
{
    public class MenuJsonModel
    {
        public MenuJsonModel()
        {
            Children = new List<Child>();
        }
        public string Path { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Component { get; set; }
        public List<Child> Children { get; set; }
    }

    public class Child
    {
        public Child()
        {
            Permission = new List<string>();
        }
        public string Path { get; set; }
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Component { get; set; }
        public List<string> Permission { get; set; }
    }
}
