﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------
namespace RecNote.Entities.Projects
{
	using RecNote.Entities.Users;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	private class Actor : User
	{
		public virtual RoleType Role
		{
			get;
			set;
		}

		public Actor()
		{
		}

	}
}

