﻿using Microsoft.EntityFrameworkCore;
using Student_Dashboard_Api.Data;
using Student_Dashboard_Api.Data.Entities;
using Student_Dashboard_Api.Models;

namespace Student_Dashboard_Api.Services
{
    public class StudentService(AppDbContext context, FileService fileService)
    {
        private readonly AppDbContext _context = context;
        private readonly FileService _fileService = fileService;

        public async Task<Student> Add(StudentCreateDto studentCreateDto)
        {
            var student = new Student()
            {
                FullName = studentCreateDto.FullName,
                Age = studentCreateDto.Age,
                Gender = studentCreateDto.Gender,
                PhoneNumber = studentCreateDto.PhoneNumber,
                PhotoName = await _fileService.SaveFileAsync(studentCreateDto.Photo)
            };

            try
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        public async Task<Student> Update(StudentUpdateDto studentUpdateDto)
        {
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == studentUpdateDto.Id)
                                                     ?? throw new Exception("Not found");

                student.FullName = studentUpdateDto.FullName ?? student.FullName;
                student.Age = studentUpdateDto.Age ?? student.Age;
                student.PhoneNumber = studentUpdateDto.PhoneNumber ?? student.PhoneNumber;
                student.Gender = studentUpdateDto.Gender ?? student.Gender;
                if(studentUpdateDto.Photo != null)
                {
                    var photoName = await _fileService.SaveFileAsync(studentUpdateDto.Photo);
                    await _fileService.RemoveFileAsync(student.PhotoName);
                    student.PhotoName = photoName;
                }

                await _context.SaveChangesAsync();
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                throw;
            }
        }

        public async Task<ICollection<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> Get(int Id)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<bool> Remove(int Id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == Id);
            if (student == null)
            {
                return false;
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
