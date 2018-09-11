﻿using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMakerFreeWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace TestMakerFreeWebApp.Controllers

{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        //GET: api/quiz/latest
        [HttpGet("Latest/(num)")]
        public IActionResult Latest(int num = 10)
        {
            var sampleQuizzes = new List<QuizViewModel>();

            //add a first dimple quiz
            sampleQuizzes.Add(new QuizViewModel()
            {
                Id = 1,
                Title = "Wich Shingeki No Kyojin character are you?",
                Description = "Anime-related personality test",
                LastModifiedDate = DateTime.Now
            });

            //ass a bunch of other sample quizzes
            for (int i = 2; i <= num; i++)
            {
                sampleQuizzes.Add(new QuizViewModel()
                {
                    Id = i,
                    Title = String.Format($"Sample Quiz{0}", i),
                    Description = "This is a sample quiz",
                    CreatedTime = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            //output the result in JSON format
            return new JsonResult(
                sampleQuizzes,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        ///<sumary>
        ///GET: api/quiz/ByTitle
        ///Retrieves the {num} Quizzes sorted by Title (A to Z)
        ///</sumary>
        //<param name="num">the number of quizzes to retrieve</param>
        ///<returns>{num} Quizzes sorted by Title</returns>
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;
            return new JsonResult(
                sampleQuizzes.OrderBy(t => t.Title),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        ///<sumary>
        ///GET: api/quiz/mostViewed
        ///Retrieves the {num} random Quizzes
        ///</sumary>
        //<param name="num">the number of quizzes to retrieve</param>
        ///<returns>{num} random Quizzes</returns>
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var sampleQuizzes = ((JsonResult)Latest(num)).Value as List<QuizViewModel>;
            return new JsonResult(
                sampleQuizzes.OrderBy(t => Guid.NewGuid()),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
    }
}