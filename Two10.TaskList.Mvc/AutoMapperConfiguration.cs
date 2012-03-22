using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Two10.TaskList.Model;
using Two10.TaskList.Mvc.Models;

namespace Two10.TaskList.Mvc
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<TaskItem, TaskItemViewModel>();
            Mapper.CreateMap<TaskItemViewModel, TaskItem>();
        }

    }
}