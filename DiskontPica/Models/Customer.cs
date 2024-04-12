﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiskontPica.Models
{
	public class Customer
	{

		[Key]
		public int customerld { get; set; }
		public string name { get; set; }
		public string password { get; set; }
		public string email { get; set; }
		public string adress { get; set; }

		public string salt { get; set; }

		public Boolean customer {  get; set; }





		public Customer()
		{
			salt = GenerateDefaultSalt();
			customer = true;
		}

		private static string GenerateDefaultSalt()
		{
			// Generate a default salt (for example, a random base64 string)
			byte[] saltBytes = new byte[16];
			new Random().NextBytes(saltBytes);
			return Convert.ToBase64String(saltBytes);
		}

	}




}
