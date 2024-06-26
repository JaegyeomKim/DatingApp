﻿using AutoMapper;
using DatingAppAPI.Data;
using DatingAppAPI.DTOs;
using DatingAppAPI.Entities;
using DatingAppAPI.Extenstions;
using DatingAppAPI.Interfaces;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DatingAppAPI.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository ,IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);
            
            return user;
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdatedDto memberUpdatedDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null) { return NotFound(); }

            _mapper.Map(memberUpdatedDto, user);

            if (await _userRepository.SaveAllAsync()) return NoContent(); //204: NoContent

            return BadRequest("Failed to updated");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null) { return NotFound(); }

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) { return BadRequest(result.Error.Message); }

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                AppUserId = user.Id, // Set the AppUserId manually
                AppUser = user,
            };

            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if (await _userRepository.SaveAllAsync())
            {
                return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
            }
            return BadRequest();
        }

    }
}
