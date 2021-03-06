﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Two10.TaskList.Model;
using Two10.TaskList.Web.Models;

namespace Two10.TaskList.Web
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