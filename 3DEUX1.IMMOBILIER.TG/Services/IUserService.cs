﻿using _3DEUX1.IMMOBILIER.TG.Models;


namespace _3DEUX1.IMMOBILIER.TG.Services
{
    interface IUserService
    {
        public Task<User?> Login(string email, string password);
        public Task<User?> RefrechLogin(string email);
        public Task<bool> Registre(RegisterModelSend model);
        public Task<bool> UserVerifier();
        public void Logout();
        public Task<bool> UploadPost(Post post);
    }
}
