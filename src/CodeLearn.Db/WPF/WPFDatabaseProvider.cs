﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace CodeLearn.Db.WPF
{
    public class WPFDatabaseProvider
    {
        public CodeLearnContext _context = new();
        private readonly UserManager<ApplicationUser> _userManager;


        #region Teacher windows' properties
        /// <summary>
        /// Implemented for CreateExerciseWindow's combobox.
        /// </summary>
        public List<ExerciseType> ExerciseTypes { get; set; }

        /// <summary>
        /// Implemented for CreateExerciseWindow's combobox.
        /// </summary>
        public List<DataType> MethodDataTypes { get; set; }
        #endregion

        #region Student windows' properties
        public List<Testing> Courses { get; set; }
        #endregion

        public WPFDatabaseProvider(UserManager<ApplicationUser> userManager)
        {
            // TODO: Class coding (only method coding allowed).
            ExerciseTypes = _context.ExerciseTypes.Where(e => e.Name != "Question" &&
                                                         e.Name != "Class coding").ToList();
            MethodDataTypes = _context.DataTypes.ToList();
            Courses = _context.Testings.ToList();

            // User Identity usage
            _userManager = userManager;
        }

        #region Initializing default data methods for CreateExerciseWindow
        public void InitializeMethodParameterDataTypes(ObservableCollection<DataType> dataTypes)
        {
            var items = _context.DataTypes.Where(x => x.ShortName != "void").ToArray();
            for (int i = 0; i < items.Length; i++)
                dataTypes.Add(items[i]);
        }

        public void InitializeTestMethodInfo(TestMethodInfo testMethod)
        {
            testMethod.Name = "GetNumber";
            testMethod.TestMethodParameters = new ObservableCollection<TestMethodParameter>();
            testMethod.ReturnType = _context.DataTypes.First(dt => dt.ShortName == "double");
            testMethod.TestCases = new ObservableCollection<TestCase>();
        }

        public void InitializeExercise(Exercise exercise, TestMethodInfo testMethod)
        {
            exercise.ExerciseType = _context.ExerciseTypes.First(et => et.Name == "Method coding");
            exercise.ClassName = "TestClass";
            exercise.Score = 1;
            exercise.TestMethodInfos.Add(testMethod);
        }

        public void InitializeTestingExercises(ObservableCollection<Exercise> exercises)
        {
            var items = _context.Exercises.ToArray();
            for (int i = 0; i < items.Length; i++)
            {
                exercises.Add(items[i]);
            }
        }
        #endregion

        #region Access methods
        public void SaveExercise(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            _context.SaveChanges(); 
        }

        public void SaveTesting(Testing testing)
        {
            _context.Testings.Add(testing);
            _context.SaveChanges();
        }

        public Exercise GetTestExercise()
        {
            return _context.Exercises.First();
        }

        public List<Testing> GetCourses()
        {
            return Courses;
        }

        public Student GetTestStudent()
        {
            return _context.Students.First();
        }

        public void SaveTestingResult(TestingResult result)
        {
            _context.Add(result);
            _context.SaveChanges();
        }

        public async Task SaveTestingResultAsync(TestingResult result)
        {
            _context.Attach(result);
            LeaveExercisesUnchanged(result.TestingAnswers);
            await _context.SaveChangesAsync();
        }

        private void LeaveExercisesUnchanged(ICollection<TestingAnswer> answers)
        {
            foreach (var answer in answers)
            {
                _context.Entry(answer.Exercise).State = EntityState.Unchanged;
            }
        }

        //public Teacher? SignInAsTeacher(string username, string password)
        //{
        //    var user = _context.Teachers.FirstOrDefault(t => t.Email == username);
        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    else if (user.Password == password)
        //    {
        //        return user;
        //    }
        //    return null;
        //}

        //public Student? SignInAsStudent(string username, string password)
        //{
        //    var user = _context.Students.FirstOrDefault(t => t.Username == username);
        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    else if (user.Password == password)
        //    {
        //        return user;
        //    }
        //    return null;
        //}

        public async Task<Teacher?> SignInAsTeacherAsync(string username, string password)
        {
            var user = _context.Teachers.FirstOrDefault(t => t.Email == username);
            if (user == null)
            {
                return null;
            }
            else if (await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }
            return null;
        }

        public async Task<Student?> SignInAsStudentAsync(string username, string password)
        {
            var user = _context.Students.FirstOrDefault(t => t.UserName == username);
            if (user == null)
            {
                return null;
            }
            else if (await _userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }
            return null;
        }

        public async Task<Student?> GetStudentByUserId(string userId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Id == userId);
        }

        public async Task<Teacher?> GetTeacherByUserId(string userId)
        {
            return await _context.Teachers.FirstOrDefaultAsync(t => t.Id == userId);
        }


        public ICollection<TestingResult>? GetTestingResults()
        {
            return _context.TestingResults.ToArray();
        }

        public ICollection<Exercise>? GetExercises()
        {
            return _context.Exercises.ToArray();
        }
        #endregion
    }
}