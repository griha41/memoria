﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------
namespace RecNote.Entities.Projects
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class ProjectItem
	{
		public virtual IEnumerable<ProjectItemComment> Comments
		{
			get;
			set;
		}

		public virtual string Data
		{
			get;
			set;
		}

		public virtual string EditorID
		{
			get;
			set;
		}

		public virtual bool IsPublic
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual ProjectItemStateType State
		{
			get;
			set;
		}

		public ProjectItem()
		{
		}

		public ProjectItem(string name)
		{
		}

	}
}

